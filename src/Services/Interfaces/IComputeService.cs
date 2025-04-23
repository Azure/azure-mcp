using AzureMcp.Arguments;

namespace AzureMcp.Services.Interfaces
{
    public interface IComputeService
    {
        Task<List<string>> GetVirtualMachines(string subscriptionId, string? tenant = null, RetryPolicyArguments? retryPolicy = null);
    }
}