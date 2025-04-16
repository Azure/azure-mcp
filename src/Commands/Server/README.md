<!-- Copyright (c) Microsoft Corporation.
<!-- Licensed under the MIT License. -->

STDIO
dotnet build && npx @modelcontextprotocol/inspector ./bin/Debug/net9.0/azmcp.exe server start


SSE
dotnet build && ./bin/Debug/net9.0/azmcp.exe server start --transport sse 
npx @modelcontextprotocol/inspector

Then attach to the azmcp process in debugger.  The default port for SSE is 5008.


To set timeout in mcp inspector
http://localhost:5173/?timeout=2000000000#resources