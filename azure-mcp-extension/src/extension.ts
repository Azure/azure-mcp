import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';
import * as os from 'os';
import { spawn, ChildProcess } from 'child_process';

let mcpServerProcess: ChildProcess | undefined;

function startMcpServerFromConfig(mcpConfigPath: string) {
    // Kill previous process if running
    if (mcpServerProcess) {
        mcpServerProcess.kill();
        mcpServerProcess = undefined;
    }
    try {
        const configRaw = fs.readFileSync(mcpConfigPath, { encoding: 'utf8' });
        const config = JSON.parse(configRaw);
        const serverConfig = config?.servers?.["azure-mcp-server"];
        if (serverConfig && serverConfig.command) {
            mcpServerProcess = spawn(serverConfig.command, serverConfig.args || [], { stdio: 'pipe' });
            mcpServerProcess.stdout?.on('data', (data) => {
                console.log(`[MCP Server]: ${data}`);
            });
            mcpServerProcess.stderr?.on('data', (data) => {
                console.error(`[MCP Server ERROR]: ${data}`);
            });
            mcpServerProcess.on('exit', (code) => {
                console.log(`MCP Server exited with code ${code}`);
            });
            vscode.window.showInformationMessage('Azure MCP server started automatically.');
        } else {
            vscode.window.showErrorMessage('azure-mcp-server config not found in mcp.json.');
        }
    } catch (err: any) {
        vscode.window.showErrorMessage('Failed to start Azure MCP server: ' + err.message);
    }
}

export function activate(context: vscode.ExtensionContext) {
    // Write .vscode/mcp.json config in the workspace
    const workspaceFolders = vscode.workspace.workspaceFolders;
    if (workspaceFolders && workspaceFolders.length > 0) {
        const workspacePath = workspaceFolders[0].uri.fsPath;
        const vscodeDir = path.join(workspacePath, '.vscode');
        if (!fs.existsSync(vscodeDir)) {
            fs.mkdirSync(vscodeDir);
        }
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
        const mcpConfig = {
            servers: {
                "azure-mcp-server": {
                    type: "stdio",
                    command: serverPath,
                    args: ["server", "start"],
                }
            }
        };
        vscode.commands.executeCommand('mcp.addServer', {
            name: 'azure-mcp-server',
            type: 'stdio',
            command: serverPath,
            args: ['server', 'start']
        });
        // const mcpConfigPath = path.join(vscodeDir, 'mcp.json');
        // fs.writeFileSync(mcpConfigPath, JSON.stringify(mcpConfig, null, 2), { encoding: 'utf8' });
        vscode.window.showInformationMessage('.vscode/mcp.json has been written for Azure MCP server.');



        // // Auto-start the MCP server from the config
        // startMcpServerFromConfig(mcpConfigPath);
    }
}

export function deactivate() {
    if (mcpServerProcess) {
        mcpServerProcess.kill();
        mcpServerProcess = undefined;
    }
}