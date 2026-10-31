# Android Remote Deployer

A Windows Forms (.NET 8.0) automation utility for building Android Studio projects, generating production APKs, synchronizing OTA update metadata (`app-version.json`), and deploying builds directly to local or web-staged server directories (e.g., `E:\html\Android\<AppName>`).

## Features
- **Project Detection & Gradle Parsing**: Automatically scans Android Studio project directories for `app/build.gradle` or `app/build.gradle.kts`.
- **Automatic Version Incrementing**:
  - Automatically reads current `versionName` and `versionCode`.
  - Increments patch versions (`X.Y` $\to$ `X.(Y+1)`) or major versions (`(X+1).0`) based on user selection.
  - Automatically syncs updated versions back to `build.gradle` before compilation.
- **Automated Gradle Build**: Executes `gradlew.bat assembleRelease` directly from the UI with real-time log streaming.
- **Deep Hidden Attribute Support**: Detects and handles deeply hidden directory structures using Win32 / standard I/O fallback mechanisms.
- **OTA JSON Metadata Generation**: Generates and updates `app-version.json` with SHA-256 hash checksums, release notes, timestamps, and download URLs.
- **Modern Dark Theme UI**: Built with responsive layouts, progress indicators, status cards, and clear feedback.

## Requirements
- .NET 8.0 SDK / Runtime
- Java Development Kit (JDK 17+)
- Android Studio / Gradle Wrapper (`gradlew.bat`)
