// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Services.Azure.Sql.Exceptions;

public class DatabaseNotFoundException : Exception
{
    public DatabaseNotFoundException(string message) : base(message)
    {
    }

    public DatabaseNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
