// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Authorization.Commands;
using AzureMcp.Authorization.Models;
using AzureMcp.Authorization.Services;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Authorization.UnitTests;

public class RoleAssignmentListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<RoleAssignmentListCommand> _logger;
    private readonly RoleAssignmentListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";
    private readonly string _knownScope = "/subscriptions/00000000-0000-0000-0000-000000000001/resourceGroups/rg1";

    public RoleAssignmentListCommandTests()
    {
        _authorizationService = Substitute.For<IAuthorizationService>();
        _logger = Substitute.For<ILogger<RoleAssignmentListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_authorizationService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsRoleAssignments_WhenRoleAssignmentsExist()
    {
        // Arrange
        var id1 = "00000000-0000-0000-0000-000000000001";
        var id2 = "00000000-0000-0000-0000-000000000002";
        var expectedRoleAssignments = new List<RoleAssignment>
        {
            new RoleAssignment
            {
                Id = $"/subscriptions/{_knownSubscriptionId}/resourcegroups/azure-mcp/providers/Microsoft.Authorization/roleAssignments/{id1}",
                Name = "Test role definition 1",
                PrincipalId = new Guid(id1),
                PrincipalType = "User",
                RoleDefinitionId = $"/subscriptions/{_knownSubscriptionId}/providers/Microsoft.Authorization/roleDefinitions/{id1}",
                Scope = _knownScope,
                Description = "Role assignment for azmcp test 1",
                DelegatedManagedIdentityResourceId = string.Empty,
                Condition = string.Empty
            },
            new RoleAssignment
            {
                Id = $"/subscriptions/{_knownSubscriptionId}/resourcegroups/azure-mcp/providers/Microsoft.Authorization/roleAssignments/{id2}",
                Name = "Test role definition 2",
                PrincipalId = new Guid(id2),
                PrincipalType = "User",
                RoleDefinitionId = $"/subscriptions/{_knownSubscriptionId}/providers/Microsoft.Authorization/roleDefinitions/{id2}",
                Scope = _knownScope,
                Description = "Role assignment for azmcp test 2",
                DelegatedManagedIdentityResourceId = string.Empty,
                Condition = "ActionMatches{'Microsoft.Authorization/roleAssignments/write'}"
            }
        };
        _authorizationService.ListRoleAssignments(
                Arg.Is(_knownScope),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
            .Returns(expectedRoleAssignments);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--scope", _knownScope,
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<RoleAssignmentListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedRoleAssignments, result.Assignments);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoRoleAssignments()
    {
        // Arrange
        _authorizationService.ListRoleAssignments(Arg.Is(_knownScope), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--scope", _knownScope
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<RoleAssignmentListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Assignments);
        Assert.Empty(result.Assignments);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";

        _authorizationService.ListRoleAssignments(Arg.Is(_knownScope), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--scope", _knownScope
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class RoleAssignmentListResult
    {
        [JsonPropertyName("Assignments")]
        public List<RoleAssignment> Assignments { get; set; } = [];
    }
}
