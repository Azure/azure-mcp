2. Build and Deploy the Application
    1. Build and Push Docker Image: {Agent should check if Dockerfile exists, if not add the step: "generate a Dockerfile for the application deployment", if does, list the Dockerfile path}.
    2. Prepare Kubernetes Manifests: {Agent should check if Kubernetes YAML files exists, if not add the step: "generate for the application deployment", if does, list the yaml files path}.
    3. Deploy to AKS: Use `kubectl apply` to deploy manifests to the AKS cluster
3. Validation:
    1. Verify pods are running and services are exposed
