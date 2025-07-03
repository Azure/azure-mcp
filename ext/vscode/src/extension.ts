import * as vscode from 'vscode';
import * as path from 'path';

export function activate(context: vscode.ExtensionContext) {
    const didChangeEmitter = new vscode.EventEmitter<void>();

    context.subscriptions.push(
        vscode.lm.registerMcpServerDefinitionProvider('azureMcpProvider', {
            onDidChangeMcpServerDefinitions: didChangeEmitter.event,
            provideMcpServerDefinitions: async () => {
                let folder = '';
                let binary = '';
                if (process.platform === 'win32') {
                    folder = 'win-x64';
                    binary = 'azmcp.exe';
                } else if (process.platform === 'darwin') {
                    folder = 'osx-x64';
                    binary = 'azmcp';
                } else if (process.platform === 'linux') {
                    folder = 'linux-x64';
                    binary = 'azmcp';
                }
                const serverPath = path.join(context.extensionPath, 'server', folder, binary);

                return [
                    new vscode.McpStdioServerDefinition(
                        'azure-mcp-server-vsix',
                        serverPath,
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
