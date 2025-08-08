=== Additional requirements for Container Apps:
- Attach User-Assigned Managed Identity.
- MANDATORY: Add a {{RoleAssignmentResource}} resource to assign the AcrPull (7f951dda-4ed3-4680-a7ca-43fe172d538d) role to the user-assigned managed identity (Only one instance is required per-container registry. Define this BEFORE any container apps.).
- Use this identity (NOT system) to connect to the container registry. A registry connection needs to be created even if we are using a template base image.
- Container Apps MUST use base container image mcr.microsoft.com/azuredocs/containerapps-helloworld:latest. The property is set via {{ImageProperty}}.
{{CorsConfiguration}}
- Define all used secrets; Use Key Vault if possible.
{{LogAnalyticsConfiguration}}
===
