#!/usr/bin/env node

const os = require('os')

// Check if DEBUG environment variable is set
const isDebugMode = process.env.DEBUG && (
  process.env.DEBUG.toLowerCase() === 'true' ||
  process.env.DEBUG.includes('azure-mcp') ||
  process.env.DEBUG === '*'
)

// Helper function for debug logging
function debugLog(...args) {
  if (isDebugMode) {
    console.error(...args)
  }
}

debugLog('\nWrapper package starting')
debugLog('All args:')
process.argv.forEach((val, index) => {
  debugLog(`${index}: ${val}`)
})

const platform = os.platform()
const arch = os.arch()

const platformPackageName = `@azure/mcp-${platform}-${arch}`

// Try to load the platform package
let platformPackage
try {
  debugLog(`Attempting to require platform package: ${platformPackageName}`)
  platformPackage = require(platformPackageName)
} catch (err) {
  console.error(`\n❌ Failed to load platform specific package '${platformPackageName}'`)
  console.error(`\n🔍 Troubleshooting steps:`)
  console.error(`\n1. Clear npm cache and reinstall:`)
  console.error(`   npm cache clean --force`)
  console.error(`   npm uninstall -g @azure/mcp`)
  console.error(`   npm install -g @azure/mcp@latest`)
  console.error(`\n2. If using npx, clear the cache:`)
  console.error(`   npx clear-npx-cache`)
  console.error(`   npx -y @azure/mcp@latest server start`)
  console.error(`\n3. Manually install the platform package:`)
  console.error(`   npm install ${platformPackageName}@latest`)
  console.error(`\n4. Check your internet connection and try again`)
  console.error(`\n5. If the issue persists, please report it at:`)
  console.error(`   https://github.com/Azure/azure-mcp/issues`)
  console.error(`\nDetailed error: ${err.message}`)
  process.exit(1)
}

platformPackage.runExecutable(process.argv.slice(2))
  .then((code) => {
    debugLog(`Process exited with code: ${code}`)
    process.exit(code)
  })
  .catch((err) => {
    console.error(`Error: ${err.message}`)
    process.exit(1)
  })
