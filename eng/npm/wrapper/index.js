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
    console.log(...args)
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
  console.error(`Failed to load platform specific package '${platformPackageName}': ${err.message}`)
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
