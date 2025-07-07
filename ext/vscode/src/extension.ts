import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import * as os from 'os';
import { execSync } from 'child_process';

export function activate(context: vscode.ExtensionContext) {
    const didChangeEmitter = new vscode.EventEmitter<void>();

    context.subscriptions.push(
        vscode.lm.registerMcpServerDefinitionProvider('azureMcpProvider', {
            onDidChangeMcpServerDefinitions: didChangeEmitter.event,
            provideMcpServerDefinitions: async () => {
                let npmPkg = '';
                let binary = '';
                if (process.platform === 'win32') {
                    npmPkg = '@azure-mcp/server-win-x64';
                    binary = 'azmcp.exe';
                } else if (process.platform === 'darwin') {
                    npmPkg = '@azure-mcp/server-osx-x64';
                    binary = 'azmcp';
                } else if (process.platform === 'linux') {
                    npmPkg = '@azure-mcp/server-linux-x64';
                    binary = 'azmcp';
                } else {
                    throw new Error('Unsupported platform: ' + process.platform);
                }

                // Download/extract the server binary if not present
                const storageDir = path.join(context.globalStorageUri.fsPath, 'server');
                const binPath = path.join(storageDir, binary);
                if (!fs.existsSync(binPath)) {
                    // Ensure storage dir exists
                    fs.mkdirSync(storageDir, { recursive: true });
                    // Use npx to download the latest server package to a temp dir
                    const tmpDir = fs.mkdtempSync(path.join(os.tmpdir(), 'azmcp-server-'));
                    execSync(`npx --yes ${npmPkg}@latest --extract --output-dir "${tmpDir}"`, { stdio: 'inherit' });
                    // Copy the binary to storageDir
                    const srcBin = path.join(tmpDir, binary);
                    fs.copyFileSync(srcBin, binPath);
                    fs.chmodSync(binPath, 0o755);
                }

                return [
                    new vscode.McpStdioServerDefinition(
                        'azure-mcp-server-npx',
                        binPath,
                        ['server', 'start']
                    )
                ];
            },
            resolveMcpServerDefinition: async (server: vscode.McpServerDefinition) => {
                // Optionally prompt for secrets or do other setup here
                return server;
            }
        })
    );
}

export function deactivate() {
    // No process management needed; VS Code will handle server lifecycle
}
