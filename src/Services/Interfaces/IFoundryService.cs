using AzureMcp.Arguments;

namespace AzureMcp.Services.Interfaces;

public interface IFoundryService
{
    Task<List<object>> ListModels(
        RetryPolicyArguments? retryPolicy = null
    );

    Task<string> GetModelGuidance(
        string inferenceModelName,
        RetryPolicyArguments? retryPolicy = null
    );
}