namespace AndroidRemoteDeployer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.grpProject = new System.Windows.Forms.GroupBox();
            this.lblDetectedApk = new System.Windows.Forms.Label();
            this.lblProjectStatus = new System.Windows.Forms.Label();
            this.txtAppId = new System.Windows.Forms.TextBox();
            this.lblAppId = new System.Windows.Forms.Label();
            this.btnQuickRms = new System.Windows.Forms.Button();
            this.btnBrowseProject = new System.Windows.Forms.Button();
            this.txtProjectRoot = new System.Windows.Forms.TextBox();
            this.lblProjectRoot = new System.Windows.Forms.Label();
            this.grpVersionBump = new System.Windows.Forms.GroupBox();
            this.btnRecalculate = new System.Windows.Forms.Button();
            this.lblBumpPreview = new System.Windows.Forms.Label();
            this.chkUpdateGradle = new System.Windows.Forms.CheckBox();
            this.rbMajorBump = new System.Windows.Forms.RadioButton();
            this.rbPatchBump = new System.Windows.Forms.RadioButton();
            this.chkAutoBump = new System.Windows.Forms.CheckBox();
            this.lblDetectedVersion = new System.Windows.Forms.Label();
            this.txtVersionName = new System.Windows.Forms.TextBox();
            this.lblVersionName = new System.Windows.Forms.Label();
            this.txtVersionCode = new System.Windows.Forms.TextBox();
            this.lblVersionCode = new System.Windows.Forms.Label();
            this.grpDestination = new System.Windows.Forms.GroupBox();
            this.btnToggleHidden = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lblJsonStatus = new System.Windows.Forms.Label();
            this.lblHiddenStatus = new System.Windows.Forms.Label();
            this.lblDestStatus = new System.Windows.Forms.Label();
            this.txtApkTargetName = new System.Windows.Forms.TextBox();
            this.lblApkTargetName = new System.Windows.Forms.Label();
            this.txtAppSubFolder = new System.Windows.Forms.TextBox();
            this.lblAppSubFolder = new System.Windows.Forms.Label();
            this.btnBrowseDestination = new System.Windows.Forms.Button();
            this.txtDestinationRoot = new System.Windows.Forms.TextBox();
            this.lblDestinationRoot = new System.Windows.Forms.Label();
            this.grpMetadata = new System.Windows.Forms.GroupBox();
            this.chkSyncRootLegacy = new System.Windows.Forms.CheckBox();
            this.btnCheckLiveServer = new System.Windows.Forms.Button();
            this.txtServerBaseUrl = new System.Windows.Forms.TextBox();
            this.lblServerBaseUrl = new System.Windows.Forms.Label();
            this.chkForceUpdate = new System.Windows.Forms.CheckBox();
            this.txtMinSupportedVersion = new System.Windows.Forms.TextBox();
            this.lblMinSupportedVersion = new System.Windows.Forms.Label();
            this.txtReleaseNotes = new System.Windows.Forms.TextBox();
            this.lblReleaseNotes = new System.Windows.Forms.Label();
            this.txtReleaseTitle = new System.Windows.Forms.TextBox();
            this.lblReleaseTitle = new System.Windows.Forms.Label();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.btnCancelBuild = new System.Windows.Forms.Button();
            this.rbReleaseBuild = new System.Windows.Forms.RadioButton();
            this.rbDebugBuild = new System.Windows.Forms.RadioButton();
            this.btnBuildAndDeploy = new System.Windows.Forms.Button();
            this.btnDeployExisting = new System.Windows.Forms.Button();
            this.grpConsole = new System.Windows.Forms.GroupBox();
            this.btnCopyLog = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.pnlHeader.SuspendLayout();
            this.grpProject.SuspendLayout();
            this.grpVersionBump.SuspendLayout();
            this.grpDestination.SuspendLayout();
            this.grpMetadata.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblHeaderSubtitle);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlHeader.Size = new System.Drawing.Size(1084, 76);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(22, 44);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(650, 15);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Autonomous build synchronization, JSON metadata generation & remote update distribution for Android Studio projects";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(534, 28);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "RMS Android Remote Deployer & Update Automation Hub";
            // 
            // grpProject
            // 
            this.grpProject.Controls.Add(this.lblDetectedApk);
            this.grpProject.Controls.Add(this.lblProjectStatus);
            this.grpProject.Controls.Add(this.txtAppId);
            this.grpProject.Controls.Add(this.lblAppId);
            this.grpProject.Controls.Add(this.btnQuickRms);
            this.grpProject.Controls.Add(this.btnBrowseProject);
            this.grpProject.Controls.Add(this.txtProjectRoot);
            this.grpProject.Controls.Add(this.lblProjectRoot);
            this.grpProject.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpProject.Location = new System.Drawing.Point(20, 86);
            this.grpProject.Name = "grpProject";
            this.grpProject.Size = new System.Drawing.Size(515, 140);
            this.grpProject.TabIndex = 1;
            this.grpProject.TabStop = false;
            this.grpProject.Text = "1. Android Studio Project Root";
            // 
            // lblDetectedApk
            // 
            this.lblDetectedApk.AutoSize = true;
            this.lblDetectedApk.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDetectedApk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDetectedApk.Location = new System.Drawing.Point(12, 114);
            this.lblDetectedApk.Name = "lblDetectedApk";
            this.lblDetectedApk.Size = new System.Drawing.Size(126, 15);
            this.lblDetectedApk.TabIndex = 11;
            this.lblDetectedApk.Text = "Found APK: Checking...";
            // 
            // lblProjectStatus
            // 
            this.lblProjectStatus.AutoSize = true;
            this.lblProjectStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProjectStatus.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblProjectStatus.Location = new System.Drawing.Point(12, 94);
            this.lblProjectStatus.Name = "lblProjectStatus";
            this.lblProjectStatus.Size = new System.Drawing.Size(107, 15);
            this.lblProjectStatus.TabIndex = 10;
            this.lblProjectStatus.Text = "Status: Initializing...";
            // 
            // txtAppId
            // 
            this.txtAppId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAppId.Location = new System.Drawing.Point(308, 91);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.ReadOnly = true;
            this.txtAppId.Size = new System.Drawing.Size(191, 23);
            this.txtAppId.TabIndex = 9;
            // 
            // lblAppId
            // 
            this.lblAppId.AutoSize = true;
            this.lblAppId.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAppId.Location = new System.Drawing.Point(220, 94);
            this.lblAppId.Name = "lblAppId";
            this.lblAppId.Size = new System.Drawing.Size(84, 15);
            this.lblAppId.TabIndex = 8;
            this.lblAppId.Text = "Package / App:";
            // 
            // btnQuickRms
            // 
            this.btnQuickRms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnQuickRms.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnQuickRms.Location = new System.Drawing.Point(413, 49);
            this.btnQuickRms.Name = "btnQuickRms";
            this.btnQuickRms.Size = new System.Drawing.Size(86, 26);
            this.btnQuickRms.TabIndex = 3;
            this.btnQuickRms.Text = "RMS Default";
            this.btnQuickRms.UseVisualStyleBackColor = false;
            this.btnQuickRms.Click += new System.EventHandler(this.btnQuickRms_Click);
            // 
            // btnBrowseProject
            // 
            this.btnBrowseProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnBrowseProject.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseProject.Location = new System.Drawing.Point(336, 49);
            this.btnBrowseProject.Name = "btnBrowseProject";
            this.btnBrowseProject.Size = new System.Drawing.Size(71, 26);
            this.btnBrowseProject.TabIndex = 2;
            this.btnBrowseProject.Text = "Browse...";
            this.btnBrowseProject.UseVisualStyleBackColor = false;
            this.btnBrowseProject.Click += new System.EventHandler(this.btnBrowseProject_Click);
            // 
            // txtProjectRoot
            // 
            this.txtProjectRoot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtProjectRoot.Location = new System.Drawing.Point(12, 50);
            this.txtProjectRoot.Name = "txtProjectRoot";
            this.txtProjectRoot.Size = new System.Drawing.Size(318, 23);
            this.txtProjectRoot.TabIndex = 1;
            // 
            // lblProjectRoot
            // 
            this.lblProjectRoot.AutoSize = true;
            this.lblProjectRoot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblProjectRoot.Location = new System.Drawing.Point(12, 27);
            this.lblProjectRoot.Name = "lblProjectRoot";
            this.lblProjectRoot.Size = new System.Drawing.Size(161, 15);
            this.lblProjectRoot.TabIndex = 0;
            this.lblProjectRoot.Text = "Android Project Root Folder:";
            // 
            // grpVersionBump
            // 
            this.grpVersionBump.Controls.Add(this.btnRecalculate);
            this.grpVersionBump.Controls.Add(this.lblBumpPreview);
            this.grpVersionBump.Controls.Add(this.chkUpdateGradle);
            this.grpVersionBump.Controls.Add(this.rbMajorBump);
            this.grpVersionBump.Controls.Add(this.rbPatchBump);
            this.grpVersionBump.Controls.Add(this.chkAutoBump);
            this.grpVersionBump.Controls.Add(this.lblDetectedVersion);
            this.grpVersionBump.Controls.Add(this.txtVersionName);
            this.grpVersionBump.Controls.Add(this.lblVersionName);
            this.grpVersionBump.Controls.Add(this.txtVersionCode);
            this.grpVersionBump.Controls.Add(this.lblVersionCode);
            this.grpVersionBump.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpVersionBump.Location = new System.Drawing.Point(20, 232);
            this.grpVersionBump.Name = "grpVersionBump";
            this.grpVersionBump.Size = new System.Drawing.Size(515, 142);
            this.grpVersionBump.TabIndex = 2;
            this.grpVersionBump.TabStop = false;
            this.grpVersionBump.Text = "2. Automated Version Code & Semantic Bumper";
            // 
            // btnRecalculate
            // 
            this.btnRecalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRecalculate.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRecalculate.Location = new System.Drawing.Point(409, 21);
            this.btnRecalculate.Name = "btnRecalculate";
            this.btnRecalculate.Size = new System.Drawing.Size(90, 23);
            this.btnRecalculate.TabIndex = 10;
            this.btnRecalculate.Text = "Recalculate";
            this.btnRecalculate.UseVisualStyleBackColor = false;
            this.btnRecalculate.Click += new System.EventHandler(this.btnRecalculate_Click);
            // 
            // lblBumpPreview
            // 
            this.lblBumpPreview.AutoSize = true;
            this.lblBumpPreview.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBumpPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(107)))), ((int)(((byte)(168)))));
            this.lblBumpPreview.Location = new System.Drawing.Point(12, 119);
            this.lblBumpPreview.Name = "lblBumpPreview";
            this.lblBumpPreview.Size = new System.Drawing.Size(201, 15);
            this.lblBumpPreview.TabIndex = 9;
            this.lblBumpPreview.Text = "Deploying: Calculating next bump...";
            // 
            // chkUpdateGradle
            // 
            this.chkUpdateGradle.AutoSize = true;
            this.chkUpdateGradle.Checked = true;
            this.chkUpdateGradle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateGradle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkUpdateGradle.Location = new System.Drawing.Point(12, 97);
            this.chkUpdateGradle.Name = "chkUpdateGradle";
            this.chkUpdateGradle.Size = new System.Drawing.Size(356, 19);
            this.chkUpdateGradle.TabIndex = 8;
            this.chkUpdateGradle.Text = "Auto-update build.gradle.kts with new versionCode && versionName";
            this.chkUpdateGradle.UseVisualStyleBackColor = true;
            // 
            // rbMajorBump
            // 
            this.rbMajorBump.AutoSize = true;
            this.rbMajorBump.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rbMajorBump.ForeColor = System.Drawing.Color.DarkMagenta;
            this.rbMajorBump.Location = new System.Drawing.Point(268, 71);
            this.rbMajorBump.Name = "rbMajorBump";
            this.rbMajorBump.Size = new System.Drawing.Size(231, 19);
            this.rbMajorBump.TabIndex = 7;
            this.rbMajorBump.Text = "Main / Major changes (+1.0) [5.1 ➔ 6.0]";
            this.rbMajorBump.UseVisualStyleBackColor = true;
            this.rbMajorBump.CheckedChanged += new System.EventHandler(this.rbBumpType_CheckedChanged);
            // 
            // rbPatchBump
            // 
            this.rbPatchBump.AutoSize = true;
            this.rbPatchBump.Checked = true;
            this.rbPatchBump.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbPatchBump.Location = new System.Drawing.Point(12, 71);
            this.rbPatchBump.Name = "rbPatchBump";
            this.rbPatchBump.Size = new System.Drawing.Size(240, 19);
            this.rbPatchBump.TabIndex = 6;
            this.rbPatchBump.TabStop = true;
            this.rbPatchBump.Text = "Patch / Minor update (+0.1) [5.1 ➔ 5.2]";
            this.rbPatchBump.UseVisualStyleBackColor = true;
            this.rbPatchBump.CheckedChanged += new System.EventHandler(this.rbBumpType_CheckedChanged);
            // 
            // chkAutoBump
            // 
            this.chkAutoBump.AutoSize = true;
            this.chkAutoBump.Checked = true;
            this.chkAutoBump.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBump.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkAutoBump.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(107)))), ((int)(((byte)(168)))));
            this.chkAutoBump.Location = new System.Drawing.Point(12, 47);
            this.chkAutoBump.Name = "chkAutoBump";
            this.chkAutoBump.Size = new System.Drawing.Size(188, 19);
            this.chkAutoBump.TabIndex = 5;
            this.chkAutoBump.Text = "Auto-Bump Version on Deploy";
            this.chkAutoBump.UseVisualStyleBackColor = true;
            this.chkAutoBump.CheckedChanged += new System.EventHandler(this.chkAutoBump_CheckedChanged);
            // 
            // lblDetectedVersion
            // 
            this.lblDetectedVersion.AutoSize = true;
            this.lblDetectedVersion.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDetectedVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDetectedVersion.Location = new System.Drawing.Point(12, 25);
            this.lblDetectedVersion.Name = "lblDetectedVersion";
            this.lblDetectedVersion.Size = new System.Drawing.Size(164, 13);
            this.lblDetectedVersion.TabIndex = 4;
            this.lblDetectedVersion.Text = "Detected Base: Code 1 (v1.0)";
            // 
            // txtVersionName
            // 
            this.txtVersionName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtVersionName.Location = new System.Drawing.Point(409, 45);
            this.txtVersionName.Name = "txtVersionName";
            this.txtVersionName.Size = new System.Drawing.Size(90, 23);
            this.txtVersionName.TabIndex = 3;
            // 
            // lblVersionName
            // 
            this.lblVersionName.AutoSize = true;
            this.lblVersionName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVersionName.Location = new System.Drawing.Point(344, 48);
            this.lblVersionName.Name = "lblVersionName";
            this.lblVersionName.Size = new System.Drawing.Size(61, 15);
            this.lblVersionName.TabIndex = 2;
            this.lblVersionName.Text = "Ver Name:";
            // 
            // txtVersionCode
            // 
            this.txtVersionCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtVersionCode.Location = new System.Drawing.Point(268, 45);
            this.txtVersionCode.Name = "txtVersionCode";
            this.txtVersionCode.Size = new System.Drawing.Size(66, 23);
            this.txtVersionCode.TabIndex = 1;
            // 
            // lblVersionCode
            // 
            this.lblVersionCode.AutoSize = true;
            this.lblVersionCode.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVersionCode.Location = new System.Drawing.Point(208, 48);
            this.lblVersionCode.Name = "lblVersionCode";
            this.lblVersionCode.Size = new System.Drawing.Size(58, 15);
            this.lblVersionCode.TabIndex = 0;
            this.lblVersionCode.Text = "Ver Code:";
            // 
            // grpDestination
            // 
            this.grpDestination.Controls.Add(this.btnToggleHidden);
            this.grpDestination.Controls.Add(this.btnOpenFolder);
            this.grpDestination.Controls.Add(this.lblJsonStatus);
            this.grpDestination.Controls.Add(this.lblHiddenStatus);
            this.grpDestination.Controls.Add(this.lblDestStatus);
            this.grpDestination.Controls.Add(this.txtApkTargetName);
            this.grpDestination.Controls.Add(this.lblApkTargetName);
            this.grpDestination.Controls.Add(this.txtAppSubFolder);
            this.grpDestination.Controls.Add(this.lblAppSubFolder);
            this.grpDestination.Controls.Add(this.btnBrowseDestination);
            this.grpDestination.Controls.Add(this.txtDestinationRoot);
            this.grpDestination.Controls.Add(this.lblDestinationRoot);
            this.grpDestination.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpDestination.Location = new System.Drawing.Point(549, 86);
            this.grpDestination.Name = "grpDestination";
            this.grpDestination.Size = new System.Drawing.Size(515, 288);
            this.grpDestination.TabIndex = 3;
            this.grpDestination.TabStop = false;
            this.grpDestination.Text = "3. Deployment Destination (D:\\html\\Android)";
            // 
            // btnToggleHidden
            // 
            this.btnToggleHidden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnToggleHidden.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToggleHidden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnToggleHidden.Location = new System.Drawing.Point(344, 248);
            this.btnToggleHidden.Name = "btnToggleHidden";
            this.btnToggleHidden.Size = new System.Drawing.Size(155, 28);
            this.btnToggleHidden.TabIndex = 11;
            this.btnToggleHidden.Text = "Toggle SuperHidden";
            this.btnToggleHidden.UseVisualStyleBackColor = false;
            this.btnToggleHidden.Click += new System.EventHandler(this.btnToggleHidden_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnOpenFolder.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOpenFolder.Location = new System.Drawing.Point(232, 248);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(106, 28);
            this.btnOpenFolder.TabIndex = 10;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = false;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // lblJsonStatus
            // 
            this.lblJsonStatus.AutoSize = true;
            this.lblJsonStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblJsonStatus.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblJsonStatus.Location = new System.Drawing.Point(12, 198);
            this.lblJsonStatus.Name = "lblJsonStatus";
            this.lblJsonStatus.Size = new System.Drawing.Size(157, 15);
            this.lblJsonStatus.TabIndex = 9;
            this.lblJsonStatus.Text = "app-version.json: Checking...";
            // 
            // lblHiddenStatus
            // 
            this.lblHiddenStatus.AutoSize = true;
            this.lblHiddenStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHiddenStatus.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblHiddenStatus.Location = new System.Drawing.Point(12, 172);
            this.lblHiddenStatus.Name = "lblHiddenStatus";
            this.lblHiddenStatus.Size = new System.Drawing.Size(108, 15);
            this.lblHiddenStatus.TabIndex = 8;
            this.lblHiddenStatus.Text = "Attributes: Normal";
            // 
            // lblDestStatus
            // 
            this.lblDestStatus.AutoSize = true;
            this.lblDestStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDestStatus.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblDestStatus.Location = new System.Drawing.Point(12, 146);
            this.lblDestStatus.Name = "lblDestStatus";
            this.lblDestStatus.Size = new System.Drawing.Size(107, 15);
            this.lblDestStatus.TabIndex = 7;
            this.lblDestStatus.Text = "Status: Initializing...";
            // 
            // txtApkTargetName
            // 
            this.txtApkTargetName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtApkTargetName.Location = new System.Drawing.Point(232, 103);
            this.txtApkTargetName.Name = "txtApkTargetName";
            this.txtApkTargetName.Size = new System.Drawing.Size(267, 23);
            this.txtApkTargetName.TabIndex = 6;
            this.txtApkTargetName.Text = "rms.apk";
            // 
            // lblApkTargetName
            // 
            this.lblApkTargetName.AutoSize = true;
            this.lblApkTargetName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblApkTargetName.Location = new System.Drawing.Point(232, 85);
            this.lblApkTargetName.Name = "lblApkTargetName";
            this.lblApkTargetName.Size = new System.Drawing.Size(117, 15);
            this.lblApkTargetName.TabIndex = 5;
            this.lblApkTargetName.Text = "Target APK Filename:";
            // 
            // txtAppSubFolder
            // 
            this.txtAppSubFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAppSubFolder.Location = new System.Drawing.Point(12, 103);
            this.txtAppSubFolder.Name = "txtAppSubFolder";
            this.txtAppSubFolder.Size = new System.Drawing.Size(201, 23);
            this.txtAppSubFolder.TabIndex = 4;
            this.txtAppSubFolder.Text = "RMS";
            // 
            // lblAppSubFolder
            // 
            this.lblAppSubFolder.AutoSize = true;
            this.lblAppSubFolder.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAppSubFolder.Location = new System.Drawing.Point(12, 85);
            this.lblAppSubFolder.Name = "lblAppSubFolder";
            this.lblAppSubFolder.Size = new System.Drawing.Size(130, 15);
            this.lblAppSubFolder.TabIndex = 3;
            this.lblAppSubFolder.Text = "App Subfolder (Folder):";
            // 
            // btnBrowseDestination
            // 
            this.btnBrowseDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnBrowseDestination.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseDestination.Location = new System.Drawing.Point(428, 46);
            this.btnBrowseDestination.Name = "btnBrowseDestination";
            this.btnBrowseDestination.Size = new System.Drawing.Size(71, 26);
            this.btnBrowseDestination.TabIndex = 2;
            this.btnBrowseDestination.Text = "Browse...";
            this.btnBrowseDestination.UseVisualStyleBackColor = false;
            this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);
            // 
            // txtDestinationRoot
            // 
            this.txtDestinationRoot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDestinationRoot.Location = new System.Drawing.Point(12, 47);
            this.txtDestinationRoot.Name = "txtDestinationRoot";
            this.txtDestinationRoot.Size = new System.Drawing.Size(410, 23);
            this.txtDestinationRoot.TabIndex = 1;
            this.txtDestinationRoot.Text = "D:\\html\\Android";
            // 
            // lblDestinationRoot
            // 
            this.lblDestinationRoot.AutoSize = true;
            this.lblDestinationRoot.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDestinationRoot.Location = new System.Drawing.Point(12, 27);
            this.lblDestinationRoot.Name = "lblDestinationRoot";
            this.lblDestinationRoot.Size = new System.Drawing.Size(176, 15);
            this.lblDestinationRoot.TabIndex = 0;
            this.lblDestinationRoot.Text = "Base Android Destination Root:";
            // 
            // grpMetadata
            // 
            this.grpMetadata.Controls.Add(this.chkSyncRootLegacy);
            this.grpMetadata.Controls.Add(this.btnCheckLiveServer);
            this.grpMetadata.Controls.Add(this.txtServerBaseUrl);
            this.grpMetadata.Controls.Add(this.lblServerBaseUrl);
            this.grpMetadata.Controls.Add(this.chkForceUpdate);
            this.grpMetadata.Controls.Add(this.txtMinSupportedVersion);
            this.grpMetadata.Controls.Add(this.lblMinSupportedVersion);
            this.grpMetadata.Controls.Add(this.txtReleaseNotes);
            this.grpMetadata.Controls.Add(this.lblReleaseNotes);
            this.grpMetadata.Controls.Add(this.txtReleaseTitle);
            this.grpMetadata.Controls.Add(this.lblReleaseTitle);
            this.grpMetadata.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMetadata.Location = new System.Drawing.Point(20, 380);
            this.grpMetadata.Name = "grpMetadata";
            this.grpMetadata.Size = new System.Drawing.Size(652, 186);
            this.grpMetadata.TabIndex = 4;
            this.grpMetadata.TabStop = false;
            this.grpMetadata.Text = "4. Automated Remote Update Metadata (app-version.json)";
            // 
            // chkSyncRootLegacy
            // 
            this.chkSyncRootLegacy.AutoSize = true;
            this.chkSyncRootLegacy.Checked = true;
            this.chkSyncRootLegacy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSyncRootLegacy.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkSyncRootLegacy.Location = new System.Drawing.Point(286, 160);
            this.chkSyncRootLegacy.Name = "chkSyncRootLegacy";
            this.chkSyncRootLegacy.Size = new System.Drawing.Size(353, 19);
            this.chkSyncRootLegacy.TabIndex = 10;
            this.chkSyncRootLegacy.Text = "Also sync legacy root (D:\\html\\app-version.json && rms.apk)";
            this.chkSyncRootLegacy.UseVisualStyleBackColor = true;
            // 
            // btnCheckLiveServer
            // 
            this.btnCheckLiveServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCheckLiveServer.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCheckLiveServer.Location = new System.Drawing.Point(495, 128);
            this.btnCheckLiveServer.Name = "btnCheckLiveServer";
            this.btnCheckLiveServer.Size = new System.Drawing.Size(144, 26);
            this.btnCheckLiveServer.TabIndex = 9;
            this.btnCheckLiveServer.Text = "Ping Live Server";
            this.btnCheckLiveServer.UseVisualStyleBackColor = false;
            this.btnCheckLiveServer.Click += new System.EventHandler(this.btnCheckLiveServer_Click);
            // 
            // txtServerBaseUrl
            // 
            this.txtServerBaseUrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtServerBaseUrl.Location = new System.Drawing.Point(286, 130);
            this.txtServerBaseUrl.Name = "txtServerBaseUrl";
            this.txtServerBaseUrl.Size = new System.Drawing.Size(203, 23);
            this.txtServerBaseUrl.TabIndex = 8;
            this.txtServerBaseUrl.Text = "http://192.168.32.43";
            // 
            // lblServerBaseUrl
            // 
            this.lblServerBaseUrl.AutoSize = true;
            this.lblServerBaseUrl.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblServerBaseUrl.Location = new System.Drawing.Point(286, 112);
            this.lblServerBaseUrl.Name = "lblServerBaseUrl";
            this.lblServerBaseUrl.Size = new System.Drawing.Size(117, 15);
            this.lblServerBaseUrl.TabIndex = 7;
            this.lblServerBaseUrl.Text = "Server Base Web URL:";
            // 
            // chkForceUpdate
            // 
            this.chkForceUpdate.AutoSize = true;
            this.chkForceUpdate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkForceUpdate.Location = new System.Drawing.Point(12, 160);
            this.chkForceUpdate.Name = "chkForceUpdate";
            this.chkForceUpdate.Size = new System.Drawing.Size(161, 19);
            this.chkForceUpdate.TabIndex = 6;
            this.chkForceUpdate.Text = "Enforce Mandatory Update";
            this.chkForceUpdate.UseVisualStyleBackColor = true;
            // 
            // txtMinSupportedVersion
            // 
            this.txtMinSupportedVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMinSupportedVersion.Location = new System.Drawing.Point(12, 130);
            this.txtMinSupportedVersion.Name = "txtMinSupportedVersion";
            this.txtMinSupportedVersion.Size = new System.Drawing.Size(175, 23);
            this.txtMinSupportedVersion.TabIndex = 5;
            this.txtMinSupportedVersion.Text = "1";
            // 
            // lblMinSupportedVersion
            // 
            this.lblMinSupportedVersion.AutoSize = true;
            this.lblMinSupportedVersion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMinSupportedVersion.Location = new System.Drawing.Point(12, 112);
            this.lblMinSupportedVersion.Name = "lblMinSupportedVersion";
            this.lblMinSupportedVersion.Size = new System.Drawing.Size(130, 15);
            this.lblMinSupportedVersion.TabIndex = 4;
            this.lblMinSupportedVersion.Text = "Min Supported Version:";
            // 
            // txtReleaseNotes
            // 
            this.txtReleaseNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtReleaseNotes.Location = new System.Drawing.Point(286, 40);
            this.txtReleaseNotes.Multiline = true;
            this.txtReleaseNotes.Name = "txtReleaseNotes";
            this.txtReleaseNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReleaseNotes.Size = new System.Drawing.Size(353, 67);
            this.txtReleaseNotes.TabIndex = 3;
            // 
            // lblReleaseNotes
            // 
            this.lblReleaseNotes.AutoSize = true;
            this.lblReleaseNotes.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblReleaseNotes.Location = new System.Drawing.Point(286, 22);
            this.lblReleaseNotes.Name = "lblReleaseNotes";
            this.lblReleaseNotes.Size = new System.Drawing.Size(83, 15);
            this.lblReleaseNotes.TabIndex = 2;
            this.lblReleaseNotes.Text = "Release Notes:";
            // 
            // txtReleaseTitle
            // 
            this.txtReleaseTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtReleaseTitle.Location = new System.Drawing.Point(12, 40);
            this.txtReleaseTitle.Name = "txtReleaseTitle";
            this.txtReleaseTitle.Size = new System.Drawing.Size(252, 23);
            this.txtReleaseTitle.TabIndex = 1;
            this.txtReleaseTitle.Text = "New RMS Update Available";
            // 
            // lblReleaseTitle
            // 
            this.lblReleaseTitle.AutoSize = true;
            this.lblReleaseTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblReleaseTitle.Location = new System.Drawing.Point(12, 22);
            this.lblReleaseTitle.Name = "lblReleaseTitle";
            this.lblReleaseTitle.Size = new System.Drawing.Size(121, 15);
            this.lblReleaseTitle.TabIndex = 0;
            this.lblReleaseTitle.Text = "Notification Title:";
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.btnCancelBuild);
            this.grpActions.Controls.Add(this.rbReleaseBuild);
            this.grpActions.Controls.Add(this.rbDebugBuild);
            this.grpActions.Controls.Add(this.btnBuildAndDeploy);
            this.grpActions.Controls.Add(this.btnDeployExisting);
            this.grpActions.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpActions.Location = new System.Drawing.Point(685, 380);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(379, 186);
            this.grpActions.TabIndex = 5;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "5. One-Click Build & Remote Deploy";
            // 
            // btnCancelBuild
            // 
            this.btnCancelBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnCancelBuild.Enabled = false;
            this.btnCancelBuild.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancelBuild.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnCancelBuild.Location = new System.Drawing.Point(235, 145);
            this.btnCancelBuild.Name = "btnCancelBuild";
            this.btnCancelBuild.Size = new System.Drawing.Size(128, 30);
            this.btnCancelBuild.TabIndex = 4;
            this.btnCancelBuild.Text = "Stop Build";
            this.btnCancelBuild.UseVisualStyleBackColor = false;
            this.btnCancelBuild.Click += new System.EventHandler(this.btnCancelBuild_Click);
            // 
            // rbReleaseBuild
            // 
            this.rbReleaseBuild.AutoSize = true;
            this.rbReleaseBuild.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbReleaseBuild.Location = new System.Drawing.Point(125, 151);
            this.rbReleaseBuild.Name = "rbReleaseBuild";
            this.rbReleaseBuild.Size = new System.Drawing.Size(95, 19);
            this.rbReleaseBuild.TabIndex = 3;
            this.rbReleaseBuild.Text = "Release Build";
            this.rbReleaseBuild.UseVisualStyleBackColor = true;
            // 
            // rbDebugBuild
            // 
            this.rbDebugBuild.AutoSize = true;
            this.rbDebugBuild.Checked = true;
            this.rbDebugBuild.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbDebugBuild.Location = new System.Drawing.Point(19, 151);
            this.rbDebugBuild.Name = "rbDebugBuild";
            this.rbDebugBuild.Size = new System.Drawing.Size(89, 19);
            this.rbDebugBuild.TabIndex = 2;
            this.rbDebugBuild.TabStop = true;
            this.rbDebugBuild.Text = "Debug Build";
            this.rbDebugBuild.UseVisualStyleBackColor = true;
            // 
            // btnBuildAndDeploy
            // 
            this.btnBuildAndDeploy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(107)))), ((int)(((byte)(168)))));
            this.btnBuildAndDeploy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuildAndDeploy.FlatAppearance.BorderSize = 0;
            this.btnBuildAndDeploy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuildAndDeploy.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBuildAndDeploy.ForeColor = System.Drawing.Color.White;
            this.btnBuildAndDeploy.Location = new System.Drawing.Point(19, 28);
            this.btnBuildAndDeploy.Name = "btnBuildAndDeploy";
            this.btnBuildAndDeploy.Size = new System.Drawing.Size(344, 46);
            this.btnBuildAndDeploy.TabIndex = 0;
            this.btnBuildAndDeploy.Text = "⚡ Build APK & Remote Deploy";
            this.btnBuildAndDeploy.UseVisualStyleBackColor = false;
            this.btnBuildAndDeploy.Click += new System.EventHandler(this.btnBuildAndDeploy_Click);
            // 
            // btnDeployExisting
            // 
            this.btnDeployExisting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnDeployExisting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeployExisting.FlatAppearance.BorderSize = 0;
            this.btnDeployExisting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeployExisting.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDeployExisting.ForeColor = System.Drawing.Color.White;
            this.btnDeployExisting.Location = new System.Drawing.Point(19, 82);
            this.btnDeployExisting.Name = "btnDeployExisting";
            this.btnDeployExisting.Size = new System.Drawing.Size(344, 42);
            this.btnDeployExisting.TabIndex = 1;
            this.btnDeployExisting.Text = "🚀 Fast Deploy (Existing Built APK)";
            this.btnDeployExisting.UseVisualStyleBackColor = false;
            this.btnDeployExisting.Click += new System.EventHandler(this.btnDeployExisting_Click);
            // 
            // grpConsole
            // 
            this.grpConsole.Controls.Add(this.btnCopyLog);
            this.grpConsole.Controls.Add(this.btnClearLog);
            this.grpConsole.Controls.Add(this.progressBar);
            this.grpConsole.Controls.Add(this.rtbLog);
            this.grpConsole.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpConsole.Location = new System.Drawing.Point(20, 572);
            this.grpConsole.Name = "grpConsole";
            this.grpConsole.Size = new System.Drawing.Size(1044, 185);
            this.grpConsole.TabIndex = 6;
            this.grpConsole.TabStop = false;
            this.grpConsole.Text = "6. Live Automation & Deployment Console";
            // 
            // btnCopyLog
            // 
            this.btnCopyLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCopyLog.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCopyLog.Location = new System.Drawing.Point(863, 16);
            this.btnCopyLog.Name = "btnCopyLog";
            this.btnCopyLog.Size = new System.Drawing.Size(81, 23);
            this.btnCopyLog.TabIndex = 3;
            this.btnCopyLog.Text = "Copy Log";
            this.btnCopyLog.UseVisualStyleBackColor = false;
            this.btnCopyLog.Click += new System.EventHandler(this.btnCopyLog_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClearLog.Location = new System.Drawing.Point(950, 16);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(81, 23);
            this.btnClearLog.TabIndex = 2;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = false;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(3, 174);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1038, 8);
            this.progressBar.TabIndex = 1;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.rtbLog.Location = new System.Drawing.Point(12, 43);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(1019, 125);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1084, 765);
            this.Controls.Add(this.grpConsole);
            this.Controls.Add(this.grpActions);
            this.Controls.Add(this.grpMetadata);
            this.Controls.Add(this.grpDestination);
            this.Controls.Add(this.grpVersionBump);
            this.Controls.Add(this.grpProject);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RMS Android Remote Deployer & Update Automation Hub";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpProject.ResumeLayout(false);
            this.grpProject.PerformLayout();
            this.grpVersionBump.ResumeLayout(false);
            this.grpVersionBump.PerformLayout();
            this.grpDestination.ResumeLayout(false);
            this.grpDestination.PerformLayout();
            this.grpMetadata.ResumeLayout(false);
            this.grpMetadata.PerformLayout();
            this.grpActions.ResumeLayout(false);
            this.grpActions.PerformLayout();
            this.grpConsole.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.GroupBox grpProject;
        private System.Windows.Forms.Label lblProjectRoot;
        private System.Windows.Forms.TextBox txtProjectRoot;
        private System.Windows.Forms.Button btnBrowseProject;
        private System.Windows.Forms.Button btnQuickRms;
        private System.Windows.Forms.Label lblAppId;
        private System.Windows.Forms.TextBox txtAppId;
        private System.Windows.Forms.Label lblProjectStatus;
        private System.Windows.Forms.Label lblDetectedApk;
        private System.Windows.Forms.GroupBox grpVersionBump;
        private System.Windows.Forms.Label lblDetectedVersion;
        private System.Windows.Forms.CheckBox chkAutoBump;
        private System.Windows.Forms.RadioButton rbPatchBump;
        private System.Windows.Forms.RadioButton rbMajorBump;
        private System.Windows.Forms.CheckBox chkUpdateGradle;
        private System.Windows.Forms.Label lblBumpPreview;
        private System.Windows.Forms.Button btnRecalculate;
        private System.Windows.Forms.Label lblVersionCode;
        private System.Windows.Forms.TextBox txtVersionCode;
        private System.Windows.Forms.Label lblVersionName;
        private System.Windows.Forms.TextBox txtVersionName;
        private System.Windows.Forms.GroupBox grpDestination;
        private System.Windows.Forms.Label lblDestinationRoot;
        private System.Windows.Forms.TextBox txtDestinationRoot;
        private System.Windows.Forms.Button btnBrowseDestination;
        private System.Windows.Forms.Label lblAppSubFolder;
        private System.Windows.Forms.TextBox txtAppSubFolder;
        private System.Windows.Forms.Label lblApkTargetName;
        private System.Windows.Forms.TextBox txtApkTargetName;
        private System.Windows.Forms.Label lblDestStatus;
        private System.Windows.Forms.Label lblHiddenStatus;
        private System.Windows.Forms.Label lblJsonStatus;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnToggleHidden;
        private System.Windows.Forms.GroupBox grpMetadata;
        private System.Windows.Forms.Label lblReleaseTitle;
        private System.Windows.Forms.TextBox txtReleaseTitle;
        private System.Windows.Forms.Label lblReleaseNotes;
        private System.Windows.Forms.TextBox txtReleaseNotes;
        private System.Windows.Forms.Label lblMinSupportedVersion;
        private System.Windows.Forms.TextBox txtMinSupportedVersion;
        private System.Windows.Forms.CheckBox chkForceUpdate;
        private System.Windows.Forms.Label lblServerBaseUrl;
        private System.Windows.Forms.TextBox txtServerBaseUrl;
        private System.Windows.Forms.Button btnCheckLiveServer;
        private System.Windows.Forms.CheckBox chkSyncRootLegacy;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button btnBuildAndDeploy;
        private System.Windows.Forms.Button btnDeployExisting;
        private System.Windows.Forms.RadioButton rbDebugBuild;
        private System.Windows.Forms.RadioButton rbReleaseBuild;
        private System.Windows.Forms.Button btnCancelBuild;
        private System.Windows.Forms.GroupBox grpConsole;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCopyLog;
        private System.Windows.Forms.Button btnClearLog;
    }
}
