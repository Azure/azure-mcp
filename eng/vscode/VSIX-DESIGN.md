# Azure MCP Server VSIX Extension

## Overview

The Azure MCP Server VSIX extension provides Azure MCP server integration and tooling in Visual Studio Code. It enables cross-platform support for the Azure MCP server, allowing users on Windows, macOS, and Linux to interact with Azure services through a unified extension experience.

---

## User Experience

- Users install the extension from the Marketplace.
- The extension registers the Azure MCP server with VS Code on activation.
- No additional downloads or dependencies are required.
- (Optional) Users can configure enabled services in `.vscode/settings.json`:
  ```json
  "azureMcp.enabledServices": ["storage", "keyvault", "cosmosdb"]
  ```


### Getting Started

Follow these steps to get up and running with the Azure MCP extension:

1. **Install the extension**
	- Install from the VS Code Marketplace, or
	- Clone/download this repository and install from source.

2. **(Optional) Configure selected services**
	 - Open your workspace settings (`.vscode/settings.json`)
	 - Add or edit the following entry to enable specific Azure MCP services:

		 ```json
		 "azureMcp.enabledServices": [
			 "storage",
			 "keyvault"
		 ]
		 ```
	 - The extension in next step will start the MCP server with only the selected services enabled.

3. **Start the MCP Server**
	- Open the Command Palette (`Ctrl+Shift+P` or `Cmd+Shift+P`)
	- Type `MCP: List Servers` and select it
	- Choose `Azure MCP Server ext` from the list
	- Click `Start Server`

4. **Verify the server is running**
	- Open the `Output` tab in VS Code
	- Look for messages indicating the server started successfully

You're now ready to use Azure MCP features in VS Code!

---

## Architecture

### 1. Extension Structure

- **Main Entry Point:**
  `src/extension.ts` – Handles activation, server definition, and integration with VS Code APIs.


- **Server Binaries:**
  Platform binaries are located in a flat `server/` folder:
  - `server/azmcp.exe` (Windows)
  - `server/azmcp` (Linux/macOS)

- **Packaging Scripts:**
  - `ci-package.ps1` – Publishes .NET server for each platform and triggers VSIX packaging.
  - `package.json` – Defines build, test, and packaging scripts.

---

### 2. Build & Packaging Process

#### a. Server Build


- The .NET MCP server is built for each supported platform (x64 and arm64) using `dotnet publish --self-contained true`.
- Output binaries are placed in the flat `server/` folder as `azmcp.exe` (Windows) and `azmcp` (Linux/macOS).

#### b. VSIX Packaging

- The `npm run ci-package` script:
  1. Builds the extension and all server binaries.
  2. Runs `vsce package` to create the VSIX.
- By default, all files in the extension folder (including `server/<os>` binaries) are included in the VSIX unless excluded by `.vscodeignore`.

---

### 3. Extension Activation & Server Launch

- On activation, the extension:
  - Detects the user's platform (`process.platform`) and architecture (`process.arch`).
  - Targets the appropriate server binary from the flat `server/` folder.
  - Registers the server with VS Code using the MCP API.

#### Example (simplified):

```typescript
let binary = '';
if (process.platform === 'win32') {
    binary = 'azmcp.exe';
} else if (process.platform === 'darwin' || process.platform === 'linux') {
    binary = 'azmcp';
}
const serverPath = path.join(context.extensionPath, 'server', binary);
```

---


### 4. Cross-Platform Support

- The VSIX is shipped per platform (Windows, Linux, macOS), with binaries for each supported architecture included for that platform.
- Supported platforms and architectures:
  - **Windows**: x64, arm64
  - **Linux**: x64, arm64
  - **macOS**: x64, arm64
- No runtime download is required; users do not need Node.js, npx, or .NET installed.
- The extension is self-contained and works offline after installation.

---

### 5. Alternative Dynamic Download (Optimization)

- As an optimization, the extension could download the latest platform-specific server binary from a public URL at runtime.
- This reduces VSIX size but requires HTTP(S) download logic.
- Not currently implemented in the default design for maximum compatibility and offline support.
- Note: This can also be achieved by dynamically downloading the platform-specific package via npm, but this approach introduces a dependency on Node.js for the end user.
---


### 6. Exclusion & Inclusion

- `.vscodeignore` can be used to exclude files from the VSIX if needed.
- By default, all `server/` binaries are included.

---


## File/Folder Structure

```
ext/vscode/
├── src/
│   └── extension.ts
├── server/
│   ├── azmcp.exe
│   └── azmcp
├── package.json
├── ci-package.ps1
└── ...
```

---

## References

- [VS Code Extension Packaging](https://code.visualstudio.com/api/working-with-extensions/publishing-extension)
- [VSCE CLI](https://code.visualstudio.com/api/working-with-extensions/publishing-extension#vsce)
