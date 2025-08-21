Help the user to set up a CI/CD pipeline to deploy to Azure with the following steps IN ORDER. **RUN the commands directly and DO NOT just give instructions. DO NOT ask user to provide information.**

  1. First generate a Github Actions workflow file to deploy to Azure. {{EnvironmentNamePrompt}} The pipeline at least contains these steps in order:
    a. Azure login: login with a service principal using OIDC. DO NOT use secret.
    b. Docker build
    c. Deploy infrastructure: Use AZ CLI "az deployment sub/group create" command. Use "az deployment sub/group wait" to wait the deployment to finish. Refer to the infra files to set the correct parameters.
    d. Azure Container Registry login: login into the container registry created in the previous step. Use "az acr list" to get the correct registry name if you are not sure.
    e. Push app images to ACR
    f. Deploy to hosting service. Use the infra deployment output or AZ CLI to list hosting resources. Find the name or ID of the hosting resources from "az <resource> list" if you are not sure.

    Pay attention to the name of the branches to which the pipeline is triggered.

  2. Run '{{EnvironmentCreateCommand}}' to create the environment in the repository.

  3. - {{SubscriptionIdPrompt}}
     - Run "az ad sp create-for-rbac" command to create a service principal. Grant the service principal *Contributor* role of the subscription. Also grant the service principal *User Access Administrator*
    **Use Federated credentials in order to authenticate to Azure services from GitHub Actions workflows. The command is **az ad app federated-credential create --id <$service-principal-app-id> --parameters '{{JsonParameters}}'**. You MUST use ' and \"(DO NOT forget the slash \) in the command. Use the current Github org/repo to fill in the subject property.

  4. Run command "gh secret set <secret_name> --body <secret_value> {{EnvironmentArg}}" to configure the AZURE_CLIENT_ID, AZURE_TENANT_ID and AZURE_SUBSCRIPTION_ID of the service principal in Github secrets using Github CLI.

  ** DO NOT prompt user for any information. Find them on your own. **
