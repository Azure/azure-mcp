# Wrapper package troubleshooting

To see debug text from the wrapper and platform packages, set the `DEBUG` environment variable to `true`:

```powershell
> $env:DEBUG='true'   #`export DEBUG=true` in bash
> npx --yes @azure/mcp@latest --version
```
```
Attempting to require platform package: @azure/mcp-linux-x64
All args:
0: /usr/bin/node
1: /home/user/.npm/_npx/541454632b79112e/node_modules/.bin/azmcp
2: --version
All args:
0: --version
Found executable in package.json: azmcp
Executable path: /home/user/.npm/_npx/541454632b79112e/node_modules/@azure/mcp-linux-x64/azmcp
Starting /home/user/.npm/_npx/541454632b79112e/node_modules/@azure/mcp-linux-x64/azmcp
0.0.6+90c3def5f15860420244db365c04eb302494da5b
Process exited with code: 0
```

This is useful if you want to see where the package is installed and how it's resolving the platform specific dependency.

# NPX Debugging

To debug javascript invoked during the npx call, you can start the run with the node option `--inspect-brk` enabled:
```
npx --node-options="--inspect-brk" @azure/mcp@latest 
```

This will start npx, but will wait for a debugger to attach before continuing.  
See [VSCode's node debugging documentation](https://code.visualstudio.com/docs/nodejs/nodejs-debugging#_attaching-to-nodejs) for details.
