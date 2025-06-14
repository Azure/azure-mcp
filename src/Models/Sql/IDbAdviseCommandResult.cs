// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Models.Sql;

internal interface IDbAdviseCommandResult
{
    List<SqlRecommendation> Recommendations { get; }
    SqlAnalysisResult Analysis { get; }
}
