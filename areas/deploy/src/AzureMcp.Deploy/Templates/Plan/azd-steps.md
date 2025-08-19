1. Provision Azure Infrastructure{{DeployTitle}}:
    1. Based on following required Azure resources in plan, get the IaC rules from the tool `iac-rules-get`
    2. Generate IaC ({{IacType}} files) for required azure resources based on the plan.
    3. Pre-check: use `get_errors` tool to check generated Bicep grammar errors. Fix the errors if exist.
    4. Run the AZD command `azd up` to provision the resources and confirm each resource is created or already exists.
    5. Check the deployment output to ensure the resources are provisioned successfully.
    {{CheckLog}}
