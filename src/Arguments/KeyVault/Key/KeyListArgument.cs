namespace AzureMcp.Arguments.KeyVault.Key
{
    public class KeyListArgument : BaseKeyVaultArguments
    {
        public bool IncludeManagedKeys { get; set; } = false;
    }
}
