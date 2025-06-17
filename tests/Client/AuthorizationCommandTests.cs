// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;


public class AuthorizationCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_role_assignments()
    {
        var scope = $"/subscriptions/{Settings.SubscriptionId}/resourceGroups/{Settings.ResourceGroupName}";
        var result = await CallToolAsync(
            "azmcp-role-assignment-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "scope", scope }
            });

        var roleAssignmentsArray = result.AssertProperty("Assignments");
        Assert.Equal(JsonValueKind.Array, roleAssignmentsArray.ValueKind);

        var enumerator = roleAssignmentsArray.EnumerateArray();
        Assert.NotEmpty(enumerator);

        var testRoleAssignmentFound = false;
        var expectedDescription = "Role assignment for azmcp test"; // Defined in ./infra/services/authorization.bicep
        while (enumerator.MoveNext() && !testRoleAssignmentFound)
        {
            var roleAssignment = enumerator.Current;
            var description = roleAssignment.AssertProperty("Description").GetString();
            testRoleAssignmentFound = expectedDescription.Equals(description, StringComparison.Ordinal);
        }
        Assert.True(testRoleAssignmentFound, "Test role assignment not found in the list of role assignments.");
    }
}
