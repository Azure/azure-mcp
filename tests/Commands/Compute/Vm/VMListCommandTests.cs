// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Commands.Compute.Vm;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using AzureMcp.Tests.Commands.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.CommandLine;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Commands.Compute.Vm
{
    public class VMListCommandTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IComputeService _computeService;
        private readonly ILogger<VmListCommand> _logger;

        public VMListCommandTests()
        {
            _computeService = Substitute.For<IComputeService>();
            _logger = Substitute.For<ILogger<VmListCommand>>();

            var collection = new ServiceCollection();
            collection.AddSingleton(_computeService);

            _serviceProvider = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsVms_WhenVmsExist()
        {
            // Arrange
            var expectedVms = new List<string> { "vm1", "vm2" };
            _computeService.GetVirtualMachines(Arg.Is("sub123"), Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
                .Returns(expectedVms);

            var command = new VmListCommand(_logger);
            var args = command.GetCommand().Parse(["--subscription", "sub123"]);
            var context = new CommandContext(_serviceProvider);

            // Act
            var response = await command.ExecuteAsync(context, args);


            var json = JsonSerializer.Serialize(response.Results);
            var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);


            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Results);
            Assert.NotNull(data);
            Assert.NotNull(data["vms"]);

            Assert.Equal(expectedVms, data["vms"]);
        }
    }
}
