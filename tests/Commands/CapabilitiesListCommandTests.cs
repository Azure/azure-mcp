﻿using System.CommandLine.Parsing;
using AzureMCP.Commands;
using AzureMCP.Commands.Capabilities;
using AzureMCP.Models.Capabilities;
using McpDotNet.Server;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace AzureMCP.Tests.Commands
{
    public class CapabilitiesListCommandTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMcpServer _mcpServer;

        public CapabilitiesListCommandTests()
        {
            _mcpServer = Substitute.For<IMcpServer>();

            var collection = new ServiceCollection();
            collection.AddSingleton(_mcpServer);

            _serviceProvider = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task GetsExpectedFullNames()
        {
            var expected = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            expected.Add("azmcp cosmos databases containers items query");
            expected.Add("azmcp cosmos databases containers list");
            expected.Add("azmcp cosmos databases list");
            expected.Add("azmcp cosmos accounts list");
            expected.Add("azmcp storage accounts list");
            expected.Add("azmcp storage tables list");
            expected.Add("azmcp storage blobs list");
            expected.Add("azmcp storage blobs containers list");
            expected.Add("azmcp storage blobs containers details");
            expected.Add("azmcp monitor logs query");
            expected.Add("azmcp monitor workspaces list");
            expected.Add("azmcp monitor tables list");
            expected.Add("azmcp subscriptions list");

            var commandFactory = new CommandFactory(_serviceProvider);
            var provider = new ServiceCollection()
                .AddSingleton(commandFactory)
                .BuildServiceProvider();
            var capabilities = new CapabilitiesListCommand();
            var context = new Models.CommandContext(provider);
            var actual = await capabilities.ExecuteAsync(context, default(ParseResult));

            Assert.NotNull(actual);
            Assert.IsType<List<CommandCapability>>(actual?.Results);

            var collection = (List<CommandCapability>)actual.Results;
            var actualNames = collection.Select(x => x.FullPath).ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var ex in expected)
            {
                var withoutHyphens = ex.Replace('-', ' ');
                Assert.True(actualNames.Remove(withoutHyphens), "Expected but was not in hash set.  Expected: " + withoutHyphens);
            }

            Assert.Empty(actualNames);
        }
    }
}
