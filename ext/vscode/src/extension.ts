// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';

export function activate(context: vscode.ExtensionContext) {
    const didChangeEmitter = new vscode.EventEmitter<void>();

    // Determine platform and binary path once at activation
    let binary = '';
    let platformFolder = '';
    if (process.platform === 'win32') {
        binary = 'azmcp.exe';
        platformFolder = 'win32';
    } else if (process.platform === 'darwin') {
        binary = 'azmcp';
        platformFolder = 'darwin';
    } else if (process.platform === 'linux') {
        binary = 'azmcp';
        platformFolder = 'linux';
    } else {
        throw new Error('Unsupported platform: ' + process.platform);
    }

    // Use the binary from the extension's server/os folder
    const binPath = path.join(context.extensionPath, 'server', platformFolder, binary);
    if (!fs.existsSync(binPath)) {
        throw new Error(`azmcp binary not found at ${binPath}. Please ensure the server binary is present.`);
    }

    // Ensure executable permission on macOS and Linux (once at activation)
    if ((process.platform === 'linux' || process.platform === 'darwin') && fs.existsSync(binPath)) {
        try {
            fs.chmodSync(binPath, 0o755);
        } catch (e) {
            console.warn(`Failed to set executable permission on ${binPath}: ${e}`);
        }
    }

    context.subscriptions.push(
        vscode.lm.registerMcpServerDefinitionProvider('azureMcpProvider', {
            onDidChangeMcpServerDefinitions: didChangeEmitter.event,
            provideMcpServerDefinitions: async () => {
                // Read enabled MCP services from user/workspace settings
                const config = vscode.workspace.getConfiguration('azureMcp');
                // Example: ["storage", "keyvault", ...]
                const enabledServices: string[] | undefined = config.get('enabledServices');
                const args = ['server', 'start'];
                if (enabledServices && Array.isArray(enabledServices) && enabledServices.length > 0) {
                    for (const svc of enabledServices) {
                        args.push('--namespace', svc);
                    }
                }

                return [
                    new vscode.McpStdioServerDefinition(
                        'azure-mcp-server-ext',
                        binPath,
                        args
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