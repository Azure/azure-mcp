## What does this PR do?

## GitHub issue number?

## Pre-merge checklist
- [ ] **I have read the [contribution guidelines](https://github.com/Azure/azure-mcp/blob/main/CONTRIBUTING.md) covering pull request process, code style, and testing**
- [ ] PR title is clear and informative
- [ ] Commit history is clean with informative messages (no previously merged commits appear in PR history). [See cleanup guide](https://github.com/Azure/azure-powershell/blob/master/documentation/development-docs/cleaning-up-commits.md)
- [ ] Added comprehensive tests for core features
- [ ] Added `CHANGELOG.md` entry for user-impacting changes (bug fixes, new features, UI/UX changes)
- [ ] Spelling check passes with `.\eng\common\spelling\Invoke-Cspell.ps1`
- [ ] For MCP tool changes, updated:
  - [ ] Documentation in `README.md`
  - [ ] Command list in `/docs/azmcp-commands.md` 
  - [ ] End-to-end test prompts in `/e2eTests/e2eTestPrompts.md`
- [ ] **Team member live testing:**
  - [ ] **Security review:** Review PR for security vulnerabilities and malicious code before running tests (e.g., cryptocurrency mining, email spam, data exfiltration, or other harmful activities)
  - [ ] **Test execution:** Add comment `/azp run azure - mcp` to trigger pipeline
