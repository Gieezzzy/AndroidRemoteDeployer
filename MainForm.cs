using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidRemoteDeployer
{
    public partial class MainForm : Form
    {
        // Real tablets/phones only need ARM; skipping x86/x86_64 roughly halves the OTA download.
        private const string OTA_ABI_FILTER = "arm64-v8a,armeabi-v7a";
        private const string DEFAULT_PROJECT_ROOT = @"c:\Users\AUDIT STAFF\AndroidStudioProjects\RMS";
        private const string DEFAULT_HTML_ANDROID_ROOT = @"E:\html\Android";
        private const string DEFAULT_RMS_SUBFOLDER = "RMS";
        private const string DEFAULT_SERVER_BASE_URL = "http://192.168.32.43";

        private CancellationTokenSource? _buildCts;
        private readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

        private int _baseVersionCode = 1;
        private string _baseVersionName = "1.0";
        private bool _isUpdatingUi = false;

        private System.Windows.Forms.Timer? _usbPollingTimer;
        private List<AdbDeviceInfo> _connectedUsbDevices = new List<AdbDeviceInfo>();
        private bool _isScanningUsb = false;

        public MainForm()
        {
            InitializeComponent();
            ApplyModernTheme();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtProjectRoot.Text = Directory.Exists(DEFAULT_PROJECT_ROOT) ? DEFAULT_PROJECT_ROOT : @"C:\";
            txtDestinationRoot.Text = DEFAULT_HTML_ANDROID_ROOT;
            txtAppSubFolder.Text = DEFAULT_RMS_SUBFOLDER;
            txtServerBaseUrl.Text = DEFAULT_SERVER_BASE_URL;

            txtReleaseTitle.Text = "New RMS Update Available";
            txtMinSupportedVersion.Text = "1";
            txtReleaseNotes.Text = "• Added RDMS Color Trial QC Approval with live eyedropper & camera.\r\n• High-speed autonomous notifications & badging.\r\n• Stability & offline sync improvements.";

            RefreshProjectInfo();
            RefreshDestinationStatus();

            // Initialize USB Device Monitor
            _usbPollingTimer = new System.Windows.Forms.Timer { Interval = 4000 };
            _usbPollingTimer.Tick += async (s, e) => await RefreshUsbDevicesUiAsync(silent: true);
            _usbPollingTimer.Start();
            _ = RefreshUsbDevicesUiAsync(silent: false);

            Log("Ready. System initialized with default paths: " + GetFullDestinationFolder(), LogType.Success);
        }

        #region Path & Folder Helpers

        private string GetFullDestinationFolder()
        {
            string root = string.IsNullOrWhiteSpace(txtDestinationRoot.Text) ? DEFAULT_HTML_ANDROID_ROOT : txtDestinationRoot.Text.Trim();
            string sub = string.IsNullOrWhiteSpace(txtAppSubFolder.Text) ? DEFAULT_RMS_SUBFOLDER : txtAppSubFolder.Text.Trim();
            return Path.Combine(root, sub);
        }

        private string GetDestinationJsonPath()
        {
            return Path.Combine(GetFullDestinationFolder(), "app-version.json");
        }

        private string GetDestinationApkPath()
        {
            string apkName = string.IsNullOrWhiteSpace(txtApkTargetName.Text) ? "rms.apk" : txtApkTargetName.Text.Trim();
            if (!apkName.EndsWith(".apk", StringComparison.OrdinalIgnoreCase)) apkName += ".apk";
            return Path.Combine(GetFullDestinationFolder(), apkName);
        }

        private string GetApkDownloadUrl()
        {
            string baseUrl = txtServerBaseUrl.Text.Trim().TrimEnd('/');
            string sub = string.IsNullOrWhiteSpace(txtAppSubFolder.Text) ? DEFAULT_RMS_SUBFOLDER : txtAppSubFolder.Text.Trim();
            string apkName = string.IsNullOrWhiteSpace(txtApkTargetName.Text) ? "rms.apk" : txtApkTargetName.Text.Trim();
            if (!apkName.EndsWith(".apk", StringComparison.OrdinalIgnoreCase)) apkName += ".apk";

            return $"{baseUrl}/Android/{sub}/{apkName}";
        }

        #endregion

        #region Project Analysis & Auto-Bump Engine

        private void RefreshProjectInfo()
        {
            try
            {
                string projectDir = txtProjectRoot.Text.Trim();
                if (!Directory.Exists(projectDir))
                {
                    lblProjectStatus.Text = "Status: Directory not found";
                    lblProjectStatus.ForeColor = Color.Crimson;
                    return;
                }

                string gradlewPath = Path.Combine(projectDir, "gradlew.bat");
                bool hasGradlew = File.Exists(gradlewPath);

                string appBuildGradleKts = Path.Combine(projectDir, "app", "build.gradle.kts");
                string appBuildGradle = Path.Combine(projectDir, "app", "build.gradle");
                string gradleFile = File.Exists(appBuildGradleKts) ? appBuildGradleKts : (File.Exists(appBuildGradle) ? appBuildGradle : "");

                int localCode = 1;
                string localName = "1.0";
                string appId = "com.example.rms";

                if (!string.IsNullOrEmpty(gradleFile))
                {
                    string content = File.ReadAllText(gradleFile);
                    var matchCode = Regex.Match(content, @"versionCode\s*=?\s*(\d+)");
                    if (matchCode.Success && int.TryParse(matchCode.Groups[1].Value, out int code))
                    {
                        localCode = code;
                    }

                    var matchName = Regex.Match(content, @"versionName\s*=?\s*[""']([^""']+)[""']");
                    if (matchName.Success)
                    {
                        localName = matchName.Groups[1].Value;
                    }

                    var matchAppId = Regex.Match(content, @"(?:applicationId|namespace)\s*=?\s*[""']([^""']+)[""']");
                    if (matchAppId.Success)
                    {
                        appId = matchAppId.Groups[1].Value;
                    }
                }

                txtAppId.Text = appId;

                // Inspect server app-version.json if already deployed
                int serverCode = 0;
                string serverName = "";
                string jsonPath = GetDestinationJsonPath();
                if (File.Exists(jsonPath))
                {
                    try
                    {
                        string jsonStr = File.ReadAllText(jsonPath);
                        var jsonNode = JsonNode.Parse(jsonStr);
                        if (jsonNode != null)
                        {
                            serverCode = jsonNode["versionCode"]?.GetValue<int>() ?? 0;
                            serverName = jsonNode["versionName"]?.GetValue<string>() ?? "";
                        }
                    }
                    catch { }
                }

                // Determine base version (take highest version between local and server)
                if (serverCode > localCode || (serverCode == localCode && CompareSemanticVersion(serverName, localName) > 0))
                {
                    _baseVersionCode = serverCode;
                    _baseVersionName = string.IsNullOrEmpty(serverName) ? localName : serverName;
                }
                else
                {
                    _baseVersionCode = localCode;
                    _baseVersionName = localName;
                }

                lblDetectedVersion.Text = $"Detected Base: Code {_baseVersionCode} (v{_baseVersionName}) [Local: {localCode} (v{localName}) | Server: {(serverCode > 0 ? $"{serverCode} (v{serverName})" : "None")}]";

                // Look for latest built APK
                string debugApk = Path.Combine(projectDir, "app", "build", "outputs", "apk", "debug", "app-debug.apk");
                string releaseApk = Path.Combine(projectDir, "app", "build", "outputs", "apk", "release", "app-release.apk");

                string foundApk = "";
                if (File.Exists(releaseApk)) foundApk = releaseApk;
                else if (File.Exists(debugApk)) foundApk = debugApk;

                if (!string.IsNullOrEmpty(foundApk))
                {
                    FileInfo fi = new FileInfo(foundApk);
                    lblDetectedApk.Text = $"Found APK: {Path.GetFileName(foundApk)} ({fi.Length / (1024 * 1024):N1} MB - {fi.LastWriteTime:yyyy-MM-dd HH:mm})";
                    lblDetectedApk.ForeColor = Color.SeaGreen;
                }
                else
                {
                    lblDetectedApk.Text = "No built APK found in app/build/outputs/apk/. Click 'Build & Deploy' to compile.";
                    lblDetectedApk.ForeColor = Color.DarkOrange;
                }

                lblProjectStatus.Text = $"Status: Valid Android Project (Gradlew: {(hasGradlew ? "Yes" : "Missing")})";
                lblProjectStatus.ForeColor = Color.SeaGreen;

                ApplyVersionCalculation();
            }
            catch (Exception ex)
            {
                lblProjectStatus.Text = "Status: Parse Error - " + ex.Message;
                lblProjectStatus.ForeColor = Color.Crimson;
            }
        }

        private void ApplyVersionCalculation()
        {
            if (_isUpdatingUi) return;
            _isUpdatingUi = true;

            try
            {
                if (chkAutoBump.Checked)
                {
                    bool isMajor = rbMajorBump.Checked;
                    var (nextCode, nextName) = CalculateNextVersion(_baseVersionCode, _baseVersionName, isMajor);

                    txtVersionCode.Text = nextCode.ToString();
                    txtVersionName.Text = nextName;
                    lblBumpPreview.Text = $"Deploying: v{nextName} (Code: {nextCode}) ➔ {(isMajor ? "Major Bump (+1.0)" : "Patch Bump (+0.1)")}";
                    lblBumpPreview.ForeColor = isMajor ? Color.Purple : Color.FromArgb(14, 107, 168);
                }
                else
                {
                    txtVersionCode.Text = _baseVersionCode.ToString();
                    txtVersionName.Text = _baseVersionName;
                    lblBumpPreview.Text = "Auto-Bump is OFF. Deploying exact version specified.";
                    lblBumpPreview.ForeColor = Color.DimGray;
                }
            }
            finally
            {
                _isUpdatingUi = false;
            }
        }

        public static (int nextCode, string nextName) CalculateNextVersion(int currentCode, string currentName, bool isMajorChange)
        {
            int nextCode = currentCode + 1;
            string nextName;

            if (string.IsNullOrWhiteSpace(currentName))
            {
                return (nextCode, isMajorChange ? "2.0" : "1.1");
            }

            currentName = currentName.Trim();
            string[] parts = currentName.Split('.');

            if (isMajorChange)
            {
                if (int.TryParse(parts[0], out int major))
                {
                    nextName = $"{major + 1}.0";
                }
                else
                {
                    nextName = "2.0";
                }
            }
            else
            {
                // Patch / Minor increment after the dot: 5.1 -> 5.2, 1.0 -> 1.1, 1.0.3 -> 1.0.4
                if (parts.Length >= 2)
                {
                    int lastIdx = parts.Length - 1;
                    if (int.TryParse(parts[lastIdx], out int lastNum))
                    {
                        parts[lastIdx] = (lastNum + 1).ToString();
                        nextName = string.Join(".", parts);
                    }
                    else
                    {
                        nextName = currentName + ".1";
                    }
                }
                else if (parts.Length == 1)
                {
                    if (int.TryParse(parts[0], out int single))
                    {
                        nextName = $"{single}.1";
                    }
                    else
                    {
                        nextName = currentName + ".1";
                    }
                }
                else
                {
                    nextName = "1.1";
                }
            }

            return (nextCode, nextName);
        }

        private static int CompareSemanticVersion(string v1, string v2)
        {
            if (string.IsNullOrEmpty(v1)) return -1;
            if (string.IsNullOrEmpty(v2)) return 1;

            string[] p1 = v1.Split('.');
            string[] p2 = v2.Split('.');
            int len = Math.Max(p1.Length, p2.Length);

            for (int i = 0; i < len; i++)
            {
                int n1 = i < p1.Length && int.TryParse(p1[i], out int a) ? a : 0;
                int n2 = i < p2.Length && int.TryParse(p2[i], out int b) ? b : 0;
                if (n1 != n2) return n1.CompareTo(n2);
            }

            return 0;
        }

        /// <summary>
        /// Updates the Android Studio build.gradle.kts or build.gradle file with the new versionCode and versionName.
        /// </summary>
        private void UpdateProjectGradleVersion(string projectDir, int newVersionCode, string newVersionName)
        {
            string ktsPath = Path.Combine(projectDir, "app", "build.gradle.kts");
            string groovyPath = Path.Combine(projectDir, "app", "build.gradle");
            string targetPath = File.Exists(ktsPath) ? ktsPath : (File.Exists(groovyPath) ? groovyPath : "");

            if (string.IsNullOrEmpty(targetPath))
            {
                Log("Warning: No build.gradle or build.gradle.kts found to update.", LogType.Warn);
                return;
            }

            try
            {
                string content = File.ReadAllText(targetPath);

                // Replace versionCode (Kotlin DSL and Groovy DSL)
                string updated = Regex.Replace(content, @"(versionCode\s*=?\s*)\d+", $"${{1}}{newVersionCode}");

                // Replace versionName (Kotlin DSL and Groovy DSL)
                updated = Regex.Replace(updated, @"(versionName\s*=?\s*[""'])[^""']+([""'])", $"${{1}}{newVersionName}${{2}}");

                if (content != updated)
                {
                    File.WriteAllText(targetPath, updated, Encoding.UTF8);
                    Log($"Updated {Path.GetFileName(targetPath)}: versionCode = {newVersionCode}, versionName = \"{newVersionName}\"", LogType.Success);
                }
                else
                {
                    Log($"Version in {Path.GetFileName(targetPath)} already matches target v{newVersionName}.", LogType.Info);
                }
            }
            catch (Exception ex)
            {
                Log($"Failed to update Gradle version file: {ex.Message}", LogType.Warn);
            }
        }

        #endregion

        #region Destination & Hidden Handling

        private void RefreshDestinationStatus()
        {
            try
            {
                string targetFolder = GetFullDestinationFolder();
                bool exists = Directory.Exists(targetFolder);

                if (!exists)
                {
                    lblDestStatus.Text = "Status: Not yet created. Will be auto-created on deploy.";
                    lblDestStatus.ForeColor = Color.DarkOrange;
                    lblHiddenStatus.Text = "Attributes: None";
                    return;
                }

                DirectoryInfo di = new DirectoryInfo(targetFolder);
                bool isHidden = (di.Attributes & FileAttributes.Hidden) != 0;
                bool isSystem = (di.Attributes & FileAttributes.System) != 0;

                string attrStr = "Normal";
                if (isHidden && isSystem) attrStr = "SuperHidden (Hidden + System)";
                else if (isHidden) attrStr = "Hidden";
                else if (isSystem) attrStr = "System";

                lblDestStatus.Text = $"Status: Directory Ready ({targetFolder})";
                lblDestStatus.ForeColor = Color.SeaGreen;

                lblHiddenStatus.Text = $"Folder Attributes: {attrStr}";
                lblHiddenStatus.ForeColor = (isHidden || isSystem) ? Color.MediumVioletRed : Color.DarkSlateGray;

                // Check app-version.json status
                string jsonPath = GetDestinationJsonPath();
                if (File.Exists(jsonPath))
                {
                    lblJsonStatus.Text = $"app-version.json: Exists ({new FileInfo(jsonPath).Length} bytes)";
                    lblJsonStatus.ForeColor = Color.SeaGreen;
                }
                else
                {
                    lblJsonStatus.Text = "app-version.json: Not found (Will be auto-generated on deploy)";
                    lblJsonStatus.ForeColor = Color.DarkOrange;
                }
            }
            catch (Exception ex)
            {
                lblDestStatus.Text = "Error: " + ex.Message;
                lblDestStatus.ForeColor = Color.Crimson;
            }
        }

        private void EnsureDirectoryAccessible(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Log($"Created destination directory: {folderPath}", LogType.Info);
            }
        }

        private void SafeWriteAllText(string filePath, string content)
        {
            string? dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir)) EnsureDirectoryAccessible(dir);

            if (File.Exists(filePath))
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(filePath);
                    if ((attr & (FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System)) != 0)
                    {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                    }
                }
                catch { }
            }

            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        private void SafeCopyFile(string sourcePath, string destPath)
        {
            string? dir = Path.GetDirectoryName(destPath);
            if (!string.IsNullOrEmpty(dir)) EnsureDirectoryAccessible(dir);

            if (File.Exists(destPath))
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(destPath);
                    if ((attr & (FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System)) != 0)
                    {
                        File.SetAttributes(destPath, FileAttributes.Normal);
                    }
                }
                catch { }
            }

            File.Copy(sourcePath, destPath, overwrite: true);
        }

        private void ToggleSuperHiddenAttributes()
        {
            try
            {
                string targetFolder = GetFullDestinationFolder();
                EnsureDirectoryAccessible(targetFolder);

                DirectoryInfo di = new DirectoryInfo(targetFolder);
                bool isCurrentlyHidden = (di.Attributes & FileAttributes.Hidden) != 0;

                if (isCurrentlyHidden)
                {
                    di.Attributes &= ~FileAttributes.Hidden;
                    di.Attributes &= ~FileAttributes.System;
                    Log($"Folder visibility restored to Normal: {targetFolder}", LogType.Success);
                }
                else
                {
                    di.Attributes |= FileAttributes.Hidden | FileAttributes.System;
                    Log($"Folder visibility set to SuperHidden (Hidden + System) matching Server HTML Handler: {targetFolder}", LogType.Warn);
                }

                RefreshDestinationStatus();
            }
            catch (Exception ex)
            {
                Log($"Failed to toggle folder attributes: {ex.Message}", LogType.Error);
                MessageBox.Show($"Failed to toggle attributes: {ex.Message}", "Attribute Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region JSON Metadata Generation

        private string GenerateOrUpdateVersionJson(long fileSizeBytes, int versionCode, string versionName)
        {
            string jsonPath = GetDestinationJsonPath();
            JsonObject root;

            if (File.Exists(jsonPath))
            {
                try
                {
                    string existing = File.ReadAllText(jsonPath);
                    root = JsonNode.Parse(existing)?.AsObject() ?? new JsonObject();
                    Log("Existing app-version.json found. Updating metadata fields...", LogType.Info);
                }
                catch (Exception ex)
                {
                    Log($"Warning reading existing JSON ({ex.Message}), creating new template.", LogType.Warn);
                    root = new JsonObject();
                }
            }
            else
            {
                root = new JsonObject();
                Log("No app-version.json found in target. Generating brand new JSON file...", LogType.Info);
            }

            // Populate / Override required fields
            root["versionCode"] = versionCode;
            root["versionName"] = versionName;
            
            if (!int.TryParse(txtMinSupportedVersion.Text.Trim(), out int minVer)) minVer = 1;
            root["minSupportedVersion"] = minVer;

            root["apkUrl"] = GetApkDownloadUrl();
            root["fileSizeBytes"] = fileSizeBytes;
            root["releaseDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            root["isForceUpdate"] = chkForceUpdate.Checked;
            root["title"] = string.IsNullOrWhiteSpace(txtReleaseTitle.Text) ? "New RMS Update Available" : txtReleaseTitle.Text.Trim();
            root["releaseNotes"] = txtReleaseNotes.Text.Trim();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string formattedJson = root.ToJsonString(options);

            SafeWriteAllText(jsonPath, formattedJson);
            Log($"Successfully updated and saved: {jsonPath}", LogType.Success);

            // Optional: Also sync to root E:\html\app-version.json for legacy clients
            if (chkSyncRootLegacy.Checked)
            {
                try
                {
                    string legacyJsonPath = @"E:\html\app-version.json";
                    SafeWriteAllText(legacyJsonPath, formattedJson);
                    Log($"Legacy Root Synced: {legacyJsonPath}", LogType.Info);
                }
                catch (Exception ex)
                {
                    Log($"Legacy root sync failed: {ex.Message}", LogType.Warn);
                }
            }

            return formattedJson;
        }

        #endregion

        #region Deploy & Build Execution

        private async void btnDeployExisting_Click(object sender, EventArgs e)
        {
            await ExecuteDeployAsync(runBuild: false);
        }

        private async void btnBuildAndDeploy_Click(object sender, EventArgs e)
        {
            await ExecuteDeployAsync(runBuild: true);
        }

        private async Task ExecuteDeployAsync(bool runBuild)
        {
            SetUiBusy(true);
            progressBar.Value = 10;

            try
            {
                string projectDir = txtProjectRoot.Text.Trim();
                if (!Directory.Exists(projectDir))
                {
                    MessageBox.Show("Please select a valid Android Studio project folder.", "Invalid Project", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Determine Target Version Code & Name
                if (!int.TryParse(txtVersionCode.Text.Trim(), out int targetVersionCode)) targetVersionCode = _baseVersionCode + 1;
                string targetVersionName = txtVersionName.Text.Trim();
                if (string.IsNullOrWhiteSpace(targetVersionName)) targetVersionName = "1.1";

                // 2. Automatically update Android Studio build.gradle.kts / build.gradle if enabled
                if (chkUpdateGradle.Checked)
                {
                    UpdateProjectGradleVersion(projectDir, targetVersionCode, targetVersionName);
                }

                // 3. Optional Gradle Build
                if (runBuild)
                {
                    Log("==================================================", LogType.Info);
                    Log($"Starting Gradle Build: .\\gradlew.bat {(rbReleaseBuild.Checked ? "assembleRelease" : "assembleDebug")} for v{targetVersionName} (Code {targetVersionCode})", LogType.Build);
                    Log("==================================================", LogType.Info);

                    progressBar.Value = 25;
                    bool buildSuccess = await RunGradleBuildAsync(projectDir, rbReleaseBuild.Checked ? "assembleRelease" : "assembleDebug");
                    if (!buildSuccess)
                    {
                        Log("Gradle build failed or was cancelled. Deployment aborted.", LogType.Error);
                        MessageBox.Show("Gradle build encountered errors. Please check the build log below.", "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Log("Gradle build completed successfully!", LogType.Success);
                }

                progressBar.Value = 60;

                // 4. Locate built APK
                string apkOutputDir = Path.Combine(projectDir, "app", "build", "outputs", "apk");
                
                string? sourceApk = null;
                if (Directory.Exists(apkOutputDir))
                {
                    var apks = Directory.GetFiles(apkOutputDir, "*.apk", SearchOption.AllDirectories);
                    Array.Sort(apks, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));
                    // Prioritize signed APKs over unsigned ones
                    string? signedApk = apks.FirstOrDefault(a => !Path.GetFileName(a).Contains("-unsigned", StringComparison.OrdinalIgnoreCase));
                    sourceApk = signedApk ?? (apks.Length > 0 ? apks[0] : null);
                }

                if (string.IsNullOrEmpty(sourceApk) || !File.Exists(sourceApk))
                {
                    Log($"Could not find compiled APK in {apkOutputDir}", LogType.Error);
                    MessageBox.Show("Could not find any compiled APK to deploy. Run a build first.", "Missing APK", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileInfo sourceInfo = new FileInfo(sourceApk);
                Log($"Selected Source APK: {sourceApk} ({sourceInfo.Length / (1024 * 1024):N1} MB)", LogType.Info);

                // 5. Ensure Target Directory & Copy APK
                string destFolder = GetFullDestinationFolder();
                EnsureDirectoryAccessible(destFolder);

                string destApk = GetDestinationApkPath();
                Log($"Copying APK to: {destApk} ...", LogType.Info);
                SafeCopyFile(sourceApk, destApk);

                FileInfo destInfo = new FileInfo(destApk);
                Log($"APK copied successfully ({destInfo.Length:N0} bytes).", LogType.Success);

                // Optional: sync to legacy root E:\html\rms.apk
                if (chkSyncRootLegacy.Checked)
                {
                    try
                    {
                        string legacyApk = @"E:\html\rms.apk";
                        SafeCopyFile(sourceApk, legacyApk);
                        Log($"Legacy Root APK Synced: {legacyApk}", LogType.Info);
                    }
                    catch (Exception ex)
                    {
                        Log($"Legacy APK sync failed: {ex.Message}", LogType.Warn);
                    }
                }

                progressBar.Value = 85;

                // 6. Generate / Update JSON Metadata with Bumped Version
                string jsonContent = GenerateOrUpdateVersionJson(destInfo.Length, targetVersionCode, targetVersionName);

                progressBar.Value = 100;

                // Refresh info so next bump is queued from new version!
                RefreshProjectInfo();
                RefreshDestinationStatus();

                // 7. Optional Direct USB Installation
                if (chkUsbAutoInstallOnDeploy.Checked)
                {
                    var readyDevs = _connectedUsbDevices.Where(d => d.IsReady).ToList();
                    if (readyDevs.Count > 0)
                    {
                        Log("==================================================", LogType.Info);
                        Log($"[USB DEBUGGING] Found {readyDevs.Count} ready USB device(s). Deploying directly via USB...", LogType.Success);
                        string chosenSerial = GetSelectedAdbSerial();
                        await InstallApkToUsbDeviceAsync(chosenSerial, destApk);
                        Log("==================================================", LogType.Success);
                    }
                    else
                    {
                        Log("[INFO] Auto-install via USB is active, but no ready USB devices were detected. Server deployment completed successfully.", LogType.Info);
                    }
                }

                Log("==================================================", LogType.Success);
                Log($"DEPLOYMENT COMPLETED FOR v{targetVersionName} (Code {targetVersionCode})!", LogType.Success);
                Log($"Download URL: {GetApkDownloadUrl()}", LogType.Success);
                Log($"Version JSON: {GetDestinationJsonPath()}", LogType.Success);
                Log("==================================================", LogType.Success);

                MessageBox.Show($"Application deployed successfully to {destFolder}!\r\n\r\nVersion: v{targetVersionName} (Code {targetVersionCode})\r\nSize: {destInfo.Length / (1024 * 1024):N1} MB\r\nURL: {GetApkDownloadUrl()}", 
                    "Deploy Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log($"Deployment Exception: {ex.Message}", LogType.Error);
                MessageBox.Show($"Deployment encountered an error:\r\n{ex.Message}", "Deployment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetUiBusy(false);
            }
        }

        private async Task<bool> RunGradleBuildAsync(string projectDir, string gradleTask)
        {
            _buildCts = new CancellationTokenSource();
            string gradlew = Path.Combine(projectDir, "gradlew.bat");
            if (!File.Exists(gradlew))
            {
                Log($"gradlew.bat not found in {projectDir}", LogType.Error);
                return false;
            }

            return await Task.Run(() =>
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c gradlew.bat {gradleTask} \"-PabiFilter={OTA_ABI_FILTER}\"",
                        WorkingDirectory = projectDir,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var proc = new Process { StartInfo = psi };
                    proc.OutputDataReceived += (s, ev) =>
                    {
                        if (!string.IsNullOrEmpty(ev.Data))
                        {
                            Invoke(new Action(() => Log(ev.Data, LogType.Build)));
                        }
                    };
                    proc.ErrorDataReceived += (s, ev) =>
                    {
                        if (!string.IsNullOrEmpty(ev.Data))
                        {
                            Invoke(new Action(() => Log(ev.Data, LogType.Error)));
                        }
                    };

                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();

                    while (!proc.HasExited)
                    {
                        if (_buildCts.Token.IsCancellationRequested)
                        {
                            proc.Kill();
                            return false;
                        }
                        Thread.Sleep(200);
                    }

                    return proc.ExitCode == 0;
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() => Log($"Process execution failed: {ex.Message}", LogType.Error)));
                    return false;
                }
            });
        }

        private async void btnCheckLiveServer_Click(object sender, EventArgs e)
        {
            string url = $"{txtServerBaseUrl.Text.Trim().TrimEnd('/')}/Android/{txtAppSubFolder.Text.Trim()}/app-version.json";
            Log($"Pinging Live Server Update URL: {url} ...", LogType.Info);

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Log($"[HTTP 200 OK] Live Server Response:\r\n{content}", LogType.Success);
                    MessageBox.Show($"Server returned HTTP 200 OK!\r\n\r\n{content}", "Live Server Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Log($"[HTTP {(int)response.StatusCode}] Server responded with error: {response.ReasonPhrase}", LogType.Warn);
                    MessageBox.Show($"Server returned HTTP {(int)response.StatusCode}: {response.ReasonPhrase}", "Server Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Log($"Failed to reach server: {ex.Message}", LogType.Error);
                MessageBox.Show($"Cannot reach web server at {url}.\r\nCheck if Apache is running.\r\n\r\nDetails: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region UI Event Handlers

        private void btnBrowseProject_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Select Android Studio Project Root Folder",
                SelectedPath = txtProjectRoot.Text
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtProjectRoot.Text = fbd.SelectedPath;
                string dirName = Path.GetFileName(fbd.SelectedPath);
                if (!string.IsNullOrEmpty(dirName) && string.IsNullOrWhiteSpace(txtAppSubFolder.Text))
                {
                    txtAppSubFolder.Text = dirName;
                }
                RefreshProjectInfo();
                RefreshDestinationStatus();
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Select Target Root Folder (e.g. E:\\html\\Android)",
                SelectedPath = txtDestinationRoot.Text
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtDestinationRoot.Text = fbd.SelectedPath;
                RefreshDestinationStatus();
            }
        }

        private void btnQuickRms_Click(object sender, EventArgs e)
        {
            txtProjectRoot.Text = DEFAULT_PROJECT_ROOT;
            txtDestinationRoot.Text = DEFAULT_HTML_ANDROID_ROOT;
            txtAppSubFolder.Text = DEFAULT_RMS_SUBFOLDER;
            RefreshProjectInfo();
            RefreshDestinationStatus();
            Log("Loaded default RMS project settings.", LogType.Info);
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string path = GetFullDestinationFolder();
                EnsureDirectoryAccessible(path);
                Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open Explorer: " + ex.Message, "Explorer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnToggleHidden_Click(object sender, EventArgs e)
        {
            ToggleSuperHiddenAttributes();
        }

        private void btnCancelBuild_Click(object sender, EventArgs e)
        {
            _buildCts?.Cancel();
            Log("Build cancellation requested...", LogType.Warn);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }

        private void btnCopyLog_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbLog.Text))
            {
                Clipboard.SetText(rtbLog.Text);
                Log("Log copied to clipboard.", LogType.Info);
            }
        }

        private void chkAutoBump_CheckedChanged(object sender, EventArgs e)
        {
            ApplyVersionCalculation();
        }

        private void rbBumpType_CheckedChanged(object sender, EventArgs e)
        {
            ApplyVersionCalculation();
        }

        private void btnRecalculate_Click(object sender, EventArgs e)
        {
            RefreshProjectInfo();
            Log("Recalculated version bump preview.", LogType.Info);
        }

        private void SetUiBusy(bool isBusy)
        {
            btnBuildAndDeploy.Enabled = !isBusy;
            btnDeployExisting.Enabled = !isBusy;
            btnCancelBuild.Enabled = isBusy;
            btnBrowseProject.Enabled = !isBusy;
            btnBrowseDestination.Enabled = !isBusy;
            btnQuickRms.Enabled = !isBusy;
            btnToggleHidden.Enabled = !isBusy;
            btnRecalculate.Enabled = !isBusy;
            btnRefreshUsb.Enabled = !isBusy;
            btnAdbRestart.Enabled = !isBusy;
            btnUsbInstallNow.Enabled = !isBusy && _connectedUsbDevices.Any(d => d.IsReady);
            btnLaunchApp.Enabled = !isBusy && _connectedUsbDevices.Any(d => d.IsReady);
            btnUsbUninstall.Enabled = !isBusy && _connectedUsbDevices.Any(d => d.IsReady);
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

        #endregion

        #region Logging Engine

        private enum LogType { Info, Success, Warn, Error, Build }

        private void Log(string message, LogType type)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Log(message, type)));
                return;
            }

            Color tagColor;
            string prefix;

            switch (type)
            {
                case LogType.Success:
                    tagColor = Color.FromArgb(16, 185, 129); // Emerald
                    prefix = "[SUCCESS] ";
                    break;
                case LogType.Warn:
                    tagColor = Color.FromArgb(245, 158, 11); // Amber
                    prefix = "[WARN]    ";
                    break;
                case LogType.Error:
                    tagColor = Color.FromArgb(239, 68, 68); // Red
                    prefix = "[ERROR]   ";
                    break;
                case LogType.Build:
                    tagColor = Color.FromArgb(96, 165, 250); // Light blue
                    prefix = "[BUILD]   ";
                    break;
                case LogType.Info:
                default:
                    tagColor = Color.FromArgb(148, 163, 184); // Slate
                    prefix = "[INFO]    ";
                    break;
            }

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;

            rtbLog.SelectionColor = Color.FromArgb(100, 116, 139);
            rtbLog.AppendText($"{DateTime.Now:HH:mm:ss} ");

            rtbLog.SelectionColor = tagColor;
            rtbLog.AppendText(prefix);

            rtbLog.SelectionColor = Color.FromArgb(226, 232, 240);
            rtbLog.AppendText(message + "\r\n");

            rtbLog.ScrollToCaret();
        }

        private void ApplyModernTheme()
        {
            rtbLog.BackColor = Color.FromArgb(15, 23, 42); // Navy 900
            rtbLog.ForeColor = Color.FromArgb(241, 245, 249);
            rtbLog.Font = new Font("Consolas", 9.5f, FontStyle.Regular);
        }

        #endregion

        #region ADB & USB Debugging Sideload Engine

        private string GetAdbExecutablePath()
        {
            // 1. Android SDK from local.properties
            try
            {
                string projectDir = txtProjectRoot.Text.Trim();
                string localProp = Path.Combine(projectDir, "local.properties");
                if (File.Exists(localProp))
                {
                    foreach (var line in File.ReadAllLines(localProp))
                    {
                        if (line.Trim().StartsWith("sdk.dir"))
                        {
                            string rawPath = line.Substring(line.IndexOf('=') + 1).Trim().Replace(@"\:", ":").Replace(@"\\", @"\");
                            string adbCand = Path.Combine(rawPath, "platform-tools", "adb.exe");
                            if (File.Exists(adbCand)) return adbCand;
                        }
                    }
                }
            }
            catch { }

            // 2. Standard known SDK locations
            string[] candidates = new[]
            {
                @"E:\Android\Sdk\platform-tools\adb.exe",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Android\Sdk\platform-tools\adb.exe"),
                @"C:\Android\Sdk\platform-tools\adb.exe",
                @"C:\Program Files\Android\platform-tools\adb.exe"
            };

            foreach (var path in candidates)
            {
                if (File.Exists(path)) return path;
            }

            return "adb.exe";
        }

        private async Task<(int ExitCode, string Output, string Error)> RunAdbCommandAsync(string arguments, Action<string>? onOutput = null)
        {
            string adbPath = GetAdbExecutablePath();
            return await Task.Run(() =>
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = adbPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    var outSb = new StringBuilder();
                    var errSb = new StringBuilder();

                    using var proc = new Process { StartInfo = psi };
                    proc.OutputDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            outSb.AppendLine(e.Data);
                            onOutput?.Invoke(e.Data);
                        }
                    };
                    proc.ErrorDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            errSb.AppendLine(e.Data);
                            onOutput?.Invoke(e.Data);
                        }
                    };

                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();

                    return (proc.ExitCode, outSb.ToString(), errSb.ToString());
                }
                catch (Exception ex)
                {
                    return (-1, "", ex.Message);
                }
            });
        }

        private async Task<List<AdbDeviceInfo>> ScanAdbDevicesAsync()
        {
            var devices = new List<AdbDeviceInfo>();
            var res = await RunAdbCommandAsync("devices -l");
            if (res.ExitCode != 0) return devices;

            var lines = res.Output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rawLine in lines)
            {
                string line = rawLine.Trim();
                if (line.StartsWith("List of devices") || line.StartsWith("*")) continue;

                var parts = Regex.Split(line, @"\s+");
                if (parts.Length >= 2)
                {
                    var dev = new AdbDeviceInfo
                    {
                        Serial = parts[0],
                        State = parts[1]
                    };

                    foreach (var part in parts)
                    {
                        if (part.StartsWith("model:", StringComparison.OrdinalIgnoreCase))
                            dev.Model = part.Substring(6).Replace("_", " ");
                        else if (part.StartsWith("product:", StringComparison.OrdinalIgnoreCase))
                            dev.Product = part.Substring(8);
                        else if (part.StartsWith("device:", StringComparison.OrdinalIgnoreCase))
                            dev.Device = part.Substring(7);
                    }

                    devices.Add(dev);
                }
            }

            return devices;
        }

        private async Task RefreshUsbDevicesUiAsync(bool silent = false)
        {
            if (_isScanningUsb) return;
            _isScanningUsb = true;

            try
            {
                var devices = await ScanAdbDevicesAsync();
                _connectedUsbDevices = devices;

                string previousSelectedSerial = GetSelectedAdbSerial();

                cmbUsbDevices.Items.Clear();

                int readyCount = devices.Count(d => d.IsReady);

                if (devices.Count == 0)
                {
                    cmbUsbDevices.Items.Add("No USB Devices Detected");
                    cmbUsbDevices.SelectedIndex = 0;
                    lblUsbDeviceStatus.Text = "⚠️ No USB Devices Detected";
                    lblUsbDeviceStatus.ForeColor = Color.DarkOrange;
                    btnUsbInstallNow.Enabled = false;
                    btnLaunchApp.Enabled = false;
                    btnUsbUninstall.Enabled = false;
                }
                else
                {
                    if (readyCount > 1)
                    {
                        cmbUsbDevices.Items.Add($"[All Ready Devices ({readyCount})]");
                    }

                    int selectIndex = 0;
                    for (int i = 0; i < devices.Count; i++)
                    {
                        cmbUsbDevices.Items.Add(devices[i]);
                        if (!string.IsNullOrEmpty(previousSelectedSerial) && devices[i].Serial == previousSelectedSerial)
                        {
                            selectIndex = cmbUsbDevices.Items.Count - 1;
                        }
                    }

                    cmbUsbDevices.SelectedIndex = selectIndex >= 0 && selectIndex < cmbUsbDevices.Items.Count ? selectIndex : 0;

                    if (readyCount > 0)
                    {
                        lblUsbDeviceStatus.Text = $"🟢 {readyCount} Device(s) Ready (USB Debugging Active)";
                        lblUsbDeviceStatus.ForeColor = Color.SeaGreen;
                        btnUsbInstallNow.Enabled = true;
                        btnLaunchApp.Enabled = true;
                        btnUsbUninstall.Enabled = true;
                    }
                    else
                    {
                        bool hasUnauthorized = devices.Any(d => d.State == "unauthorized");
                        if (hasUnauthorized)
                        {
                            lblUsbDeviceStatus.Text = "🔑 Device Unauthorized (Unlock phone & allow USB debugging)";
                            lblUsbDeviceStatus.ForeColor = Color.Crimson;
                        }
                        else
                        {
                            lblUsbDeviceStatus.Text = $"⚠️ {devices.Count} Device(s) connected ({devices[0].State})";
                            lblUsbDeviceStatus.ForeColor = Color.DarkOrange;
                        }
                        btnUsbInstallNow.Enabled = false;
                        btnLaunchApp.Enabled = false;
                        btnUsbUninstall.Enabled = false;
                    }

                    if (!silent)
                    {
                        Log($"ADB scan found {devices.Count} connected USB device(s) ({readyCount} ready).", LogType.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!silent) Log($"USB scan error: {ex.Message}", LogType.Warn);
            }
            finally
            {
                _isScanningUsb = false;
            }
        }

        private string GetSelectedAdbSerial()
        {
            if (cmbUsbDevices.SelectedItem is AdbDeviceInfo dev)
            {
                return dev.Serial;
            }
            return "";
        }

        private string? GetTargetApkForUsb()
        {
            // 1. Check compiled APK in project outputs
            string projectDir = txtProjectRoot.Text.Trim();
            string apkOutputDir = Path.Combine(projectDir, "app", "build", "outputs", "apk");
            if (Directory.Exists(apkOutputDir))
            {
                var apks = Directory.GetFiles(apkOutputDir, "*.apk", SearchOption.AllDirectories);
                Array.Sort(apks, (a, b) => File.GetLastWriteTime(b).CompareTo(File.GetLastWriteTime(a)));
                string? signed = apks.FirstOrDefault(a => !Path.GetFileName(a).Contains("-unsigned", StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(signed) && File.Exists(signed)) return signed;
                if (apks.Length > 0 && File.Exists(apks[0])) return apks[0];
            }

            // 2. Check deployed APK in destination folder
            string destApk = GetDestinationApkPath();
            if (File.Exists(destApk)) return destApk;

            return null;
        }

        private async Task<bool> InstallApkToUsbDeviceAsync(string? specificSerial = null, string? customApkPath = null)
        {
            string? apkToInstall = customApkPath;
            if (string.IsNullOrEmpty(apkToInstall) || !File.Exists(apkToInstall))
            {
                apkToInstall = GetTargetApkForUsb();
            }

            if (string.IsNullOrEmpty(apkToInstall) || !File.Exists(apkToInstall))
            {
                Log("Cannot install via USB: No APK file found. Please build or deploy the APK first.", LogType.Error);
                MessageBox.Show("No APK file found to install. Please run 'Build APK & Remote Deploy' first.", "Missing APK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var targetDevices = new List<AdbDeviceInfo>();
            if (!string.IsNullOrEmpty(specificSerial))
            {
                var dev = _connectedUsbDevices.FirstOrDefault(d => d.Serial == specificSerial);
                if (dev != null) targetDevices.Add(dev);
            }

            if (targetDevices.Count == 0)
            {
                targetDevices = _connectedUsbDevices.Where(d => d.IsReady).ToList();
            }

            if (targetDevices.Count == 0)
            {
                Log("USB Install Aborted: No authorized Android devices connected via USB debugging.", LogType.Warn);
                MessageBox.Show("No authorized Android devices detected.\r\n\r\n1. Ensure phone is connected via USB.\r\n2. Enable Developer Options -> USB Debugging.\r\n3. Tap 'Allow USB Debugging' on your phone screen.", "No Device", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Build install flags
            var flagSb = new StringBuilder();
            if (chkUsbKeepData.Checked) flagSb.Append(" -r");
            flagSb.Append(" -d"); // Allow version downgrade
            if (chkUsbGrantPermissions.Checked) flagSb.Append(" -g"); // Auto-grant runtime permissions

            string flags = flagSb.ToString().Trim();
            bool overallSuccess = true;

            foreach (var target in targetDevices)
            {
                Log($"[USB DEBUGGING] Installing {Path.GetFileName(apkToInstall)} on {target.Model} ({target.Serial})...", LogType.Build);

                string installArgs = $"-s {target.Serial} install {flags} \"{apkToInstall}\"";
                var installRes = await RunAdbCommandAsync(installArgs, line =>
                {
                    Invoke(new Action(() => Log($"[ADB] {line}", LogType.Build)));
                });

                if (installRes.Output.Contains("Success") || installRes.ExitCode == 0)
                {
                    Log($"[SUCCESS] APK successfully installed on {target.Model} ({target.Serial})!", LogType.Success);

                    if (chkUsbLaunchAfterInstall.Checked)
                    {
                        string appId = string.IsNullOrWhiteSpace(txtAppId.Text) ? "com.example.rms" : txtAppId.Text.Trim();
                        Log($"[ADB] Auto-launching application ({appId}) on {target.Serial}...", LogType.Info);
                        string launchArgs = $"-s {target.Serial} shell monkey -p {appId} -c android.intent.category.LAUNCHER 1";
                        await RunAdbCommandAsync(launchArgs);
                    }
                }
                else
                {
                    overallSuccess = false;
                    Log($"[ERROR] Installation failed on {target.Serial}: {installRes.Error} {installRes.Output}", LogType.Error);

                    if (installRes.Output.Contains("INSTALL_FAILED_UPDATE_INCOMPATIBLE"))
                    {
                        Log("[HINT] Existing installed APK has a different signing certificate. Click 'Clean Reinstall' to uninstall first.", LogType.Warn);
                    }
                    else if (installRes.Output.Contains("INSTALL_FAILED_USER_RESTRICTED"))
                    {
                        Log("[HINT] USB installation blocked by phone security policy. Enable 'Install via USB' in your phone's Developer Options.", LogType.Warn);
                    }
                }
            }

            return overallSuccess;
        }

        private async void btnRefreshUsb_Click(object sender, EventArgs e)
        {
            await RefreshUsbDevicesUiAsync(silent: false);
        }

        private void cmbUsbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Selected USB device changed
        }

        private async void btnUsbInstallNow_Click(object sender, EventArgs e)
        {
            SetUiBusy(true);
            try
            {
                string chosenSerial = GetSelectedAdbSerial();
                bool ok = await InstallApkToUsbDeviceAsync(chosenSerial);
                if (ok)
                {
                    MessageBox.Show("APK installed successfully via USB Debugging!", "Direct Install Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                SetUiBusy(false);
            }
        }

        private async void btnLaunchApp_Click(object sender, EventArgs e)
        {
            string chosenSerial = GetSelectedAdbSerial();
            string appId = string.IsNullOrWhiteSpace(txtAppId.Text) ? "com.example.rms" : txtAppId.Text.Trim();
            string args = string.IsNullOrEmpty(chosenSerial)
                ? $"shell monkey -p {appId} -c android.intent.category.LAUNCHER 1"
                : $"-s {chosenSerial} shell monkey -p {appId} -c android.intent.category.LAUNCHER 1";

            Log($"Launching {appId} via ADB...", LogType.Info);
            var res = await RunAdbCommandAsync(args);
            if (res.ExitCode == 0)
            {
                Log($"[SUCCESS] App {appId} launched.", LogType.Success);
            }
            else
            {
                Log($"Failed to launch app: {res.Error}", LogType.Warn);
            }
        }

        private async void btnAdbRestart_Click(object sender, EventArgs e)
        {
            SetUiBusy(true);
            Log("Restarting ADB server...", LogType.Info);
            try
            {
                await RunAdbCommandAsync("kill-server");
                await Task.Delay(500);
                await RunAdbCommandAsync("start-server");
                await RefreshUsbDevicesUiAsync(silent: false);
                Log("[SUCCESS] ADB server restarted successfully.", LogType.Success);
            }
            finally
            {
                SetUiBusy(false);
            }
        }

        private async void btnUsbUninstall_Click(object sender, EventArgs e)
        {
            string appId = string.IsNullOrWhiteSpace(txtAppId.Text) ? "com.example.rms" : txtAppId.Text.Trim();
            string chosenSerial = GetSelectedAdbSerial();

            var confirm = MessageBox.Show($"Uninstall '{appId}' from selected device and perform clean reinstall?", "Confirm Clean Reinstall", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            SetUiBusy(true);
            try
            {
                Log($"[ADB] Uninstalling {appId}...", LogType.Info);
                string uninstArgs = string.IsNullOrEmpty(chosenSerial) ? $"uninstall {appId}" : $"-s {chosenSerial} uninstall {appId}";
                await RunAdbCommandAsync(uninstArgs);
                Log("[ADB] Uninstall finished. Reinstalling fresh APK...", LogType.Info);

                bool ok = await InstallApkToUsbDeviceAsync(chosenSerial);
                if (ok)
                {
                    MessageBox.Show($"Clean reinstall of {appId} complete!", "Reinstall Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                SetUiBusy(false);
            }
        }

        #endregion
    }

    public class AdbDeviceInfo
    {
        public string Serial { get; set; } = "";
        public string State { get; set; } = "unknown";
        public string Model { get; set; } = "";
        public string Product { get; set; } = "";
        public string Device { get; set; } = "";

        public bool IsReady => string.Equals(State, "device", StringComparison.OrdinalIgnoreCase);

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrEmpty(Serial)) return "None";
                string modelStr = !string.IsNullOrEmpty(Model) ? Model : (!string.IsNullOrEmpty(Device) ? Device : "Android Device");
                string statusIcon = IsReady ? "🟢 Ready" : (State == "unauthorized" ? "🔑 Unauthorized" : $"⚠️ {State}");
                return $"{modelStr} ({Serial}) [{statusIcon}]";
            }
        }

        public override string ToString() => DisplayText;
    }
}
