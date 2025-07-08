## What does this PR do?

## GitHub issue number?

## Checklist before merging
- [ ] **I have read the [contribution guidelines](https://github.com/Azure/azure-mcp/blob/main/CONTRIBUTING.md) on pull request process, code style, and testing.**
- [ ] Title of the pull request is clear and informative.
- [ ] There are a small number of commits, each of which have an informative message. This means that previously merged commits do not appear in the history of the PR. For more information on cleaning up the commits in your PR,  [see this page](https://github.com/Azure/azure-powershell/blob/master/documentation/development-docs/cleaning-up-commits.md).
- [ ] For core features, I have added thorough tests.
- [ ] For user-impacting changes (bug fixes, new features, UI/UX changes), I have added a `CHANGELOG.md` entry linking to this PR.
- [ ] For MCP tool additions/updates, I have updated the documentation in `README.md`, the command list in `azmcp-commands.md`, and end-to-end test prompts in `/e2eTests/e2eTestPrompts.md`.
- [ ] Have a team member run [live tests](https://github.com/Azure/azure-mcp/blob/main/CONTRIBUTING.md#live-tests):
   - [ ] Team Member: Inspect PR for security issues before queueing a test run
   - [ ] Team Member: Add this comment to the pr `/azp run azure - mcp` to start the run
