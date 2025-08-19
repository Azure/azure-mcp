2. Build and Deploy the Application:
    1. Build and Push Docker Image: Agent should check if Dockerfile exists, if not add the step: 'generate a Dockerfile for the application deployment', if it does, list the Dockerfile path
    2. Deploy to {{AzureComputeHost}}: Use Azure CLI command to deploy the application
3. Validation:
    1. Verify command output to ensure the application is deployed successfully
