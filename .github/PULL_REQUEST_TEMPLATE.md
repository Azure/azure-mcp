## What does this PR do?
`[Provide a clear, concise description of the changes]`

`[Any additional context, screenshots, or information that helps reviewers]`

## GitHub issue number?
`[Link to the GitHub issue this PR addresses]`

## Pre-merge Checklist

- [ ] Required for All PRs
    - [ ] **Read [contribution guidelines](https://github.com/Azure/azure-mcp/blob/main/CONTRIBUTING.md)**
    - [ ] PR title clearly describes the change
    - [ ] Commit history is clean with descriptive messages ([cleanup guide](https://github.com/Azure/azure-powershell/blob/master/documentation/development-docs/cleaning-up-commits.md))
    - [ ] Added comprehensive tests for new/modified functionality
    - [ ] Updated `CHANGELOG.md` for product changes (`features, bug fixes, UI/UX, updated dependencies`)
    - [ ] Spelling check passes: `.\eng\common\spelling\Invoke-Cspell.ps1`
- [ ] For MCP tool changes, updated:
    - [ ] Updated `README.md` documentation
    - [ ] Updated command list in `/docs/azmcp-commands.md`
    - [ ] Updated test prompts in `/e2eTests/e2eTestPrompts.md`
- [ ] 👉 For Community (non-Azure team member) PRs:
    - [ ] **Security review**: Reviewed code for security vulnerabilities, malicious code, or suspicious activities before running tests (`crypto mining, spam, data exfiltration, etc.`)
    - [ ] **Manual tests run**: added comment `/azp run azure - mcp` to run *Live Test Pipeline*
