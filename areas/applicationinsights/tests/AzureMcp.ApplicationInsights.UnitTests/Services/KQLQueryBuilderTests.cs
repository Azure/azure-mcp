// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Services;
using Xunit;

namespace AzureMcp.ApplicationInsights.UnitTests.Services;

[Trait("Area", "ApplicationInsights")]
public class KQLQueryBuilderTests
{
    [Fact]
    public void GetDistributedTrace_ReturnsWithValidTraceId()
    {
        var result = KQLQueryBuilder.GetDistributedTrace("1234");

        Assert.NotNull(result);
        Assert.Equal($"""
            union requests, dependencies, exceptions, (availabilityResults | extend success=iff(success=='1', "True", "False"))
            | where operation_Id == "1234"
            | project-away customMeasurements, _ResourceId, itemCount, client_Type, client_Model, client_OS, client_IP, client_City, client_StateOrProvince, client_CountryOrRegion, client_Browser, appId, appName, iKey, sdkVersion
            """, result);
    }

    [Fact]
    public void ListTraces_Requests_WithNoFilters()
    {
        var result = KQLQueryBuilder.ListTraces("requests", Array.Empty<string>());

        Assert.Equal("""

            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            requests
            | project operation_Name, resultCode, operation_Id, itemType, id, itemCount
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, operation_Name, resultCode
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_Requests_WithFilters()
    {
        var result = KQLQueryBuilder.ListTraces("requests", new string[]
        {
            "resultCode='200'",
            "operation_Name='GET /redis'"
        });

        Assert.Equal("""

            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            requests| where resultCode contains "200"
            | where operation_Name contains "GET /redis"
            | project operation_Name, resultCode, operation_Id, itemType, id, itemCount
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, operation_Name, resultCode
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_Requests_WithPercentileFilters()
    {
        var result = KQLQueryBuilder.ListTraces("requests", new string[]
        {
            "resultCode='200'",
            "duration='95p'"
        });

        Assert.Equal("""
            let percentile95 = toscalar(requests | where resultCode contains "200"
            | summarize percentile(duration, 95));
            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            requests| where resultCode contains "200"
            | where duration > percentile95
            | project operation_Name, resultCode, operation_Id, itemType, id, itemCount
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, operation_Name, resultCode
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_Dependencies_WithNoFilters()
    {
        var result = KQLQueryBuilder.ListTraces("dependencies", Array.Empty<string>());

        Assert.Equal("""

            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType, id, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, target, type, resultCode
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_Dependencies_WithFilters()
    {
        var result = KQLQueryBuilder.ListTraces("dependencies", new string[]
        {
            "resultCode='200'",
            "type='redis'"
        });

        Assert.Equal("""

            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            dependencies| where resultCode contains "200"
            | where type contains "redis"
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType, id, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, target, type, resultCode
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_Dependencies_WithPercentileFilters()
    {
        var result = KQLQueryBuilder.ListTraces("dependencies", new string[]
        {
            "resultCode='200'",
            "duration='95p'"
        });

        Assert.Equal("""
            let percentile95 = toscalar(dependencies | where resultCode contains "200"
            | summarize percentile(duration, 95));
            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            dependencies| where resultCode contains "200"
            | where duration > percentile95
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType, id, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, target, type, resultCode
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_Exceptions_WithNoFilters()
    {
        var result = KQLQueryBuilder.ListTraces("exceptions", Array.Empty<string>());

        Assert.Equal("""
            
            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            exceptions
            | project problemId, type, operation_Id, itemType, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, problemId, type
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_Exceptions_WithFilters()
    {
        var result = KQLQueryBuilder.ListTraces("exceptions", new string[]
        {
            "type='System.NullReferenceException'",
            "message='Object reference not set to an instance of an object.'"
        });

        Assert.Equal("""
            
            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            exceptions| where type contains "System.NullReferenceException"
            | where message contains "Object reference not set to an instance of an object."
            | project problemId, type, operation_Id, itemType, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, problemId, type
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_AvailabilityResults_WithNoFilters()
    {
        var result = KQLQueryBuilder.ListTraces("availabilityResults", Array.Empty<string>());

        Assert.Equal("""
            
            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            availabilityResults
            | extend success=iff(success == '1', "True", "False")
            | project name, location, operation_Id, itemType, id, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, name, location
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_AvailabilityResults_WithFilters()
    {
        var result = KQLQueryBuilder.ListTraces("availabilityResults", new string[]
        {
            "name='AvailabilityTest'",
            "location='West Europe'"
        });

        Assert.Equal("""
            
            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            availabilityResults
            | extend success=iff(success == '1', "True", "False")| where name contains "AvailabilityTest"
            | where location contains "West Europe"
            | project name, location, operation_Id, itemType, id, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, name, location
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ListTraces_AvailabilityResults_WithPercentileFilters()
    {
        var result = KQLQueryBuilder.ListTraces("availabilityResults", new string[]
        {
            "name='AvailabilityTest'",
            "duration='95p'"
        });

        Assert.Equal("""
            let percentile95 = toscalar(availabilityResults | where name contains "AvailabilityTest"
            | summarize percentile(duration, 95));
            let min_length_8 = (s: string) {let len = strlen(s);case(len == 1, strcat(s, s, s, s, s, s, s, s),    len == 2 or len == 3, strcat(s, s, s, s),    len == 4 or len == 5 or len == 6 or len == 7, strcat(s, s),    s)};
            let ai_hash = (s: string) {
                abs(toint(__hash_djb2(min_length_8(s))))
            };
            availabilityResults
            | extend success=iff(success == '1', "True", "False")| where name contains "AvailabilityTest"
            | where duration > percentile95
            | project name, location, operation_Id, itemType, id, itemCount
            | join kind=leftouter (requests
            | project operation_Name, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (dependencies
            | where type != "InProc"
            | project target, type, resultCode, operation_Id, itemType) on operation_Id
            | join kind=leftouter (exceptions
            | project problemId, type, operation_Id, itemType) on operation_Id
            | summarize sum(itemCount), arg_min(ai_hash(operation_Id), operation_Id, column_ifexists("id", '')) by itemType, operation_Name, resultCode, problemId, target, type, resultCode1, name, location
            | summarize traces=make_list(bag_pack('traceId', operation_Id, 'spanId', column_ifexists("id", '')), 3), sum(sum_itemCount) by itemType, name, location
            | top 10 by sum_sum_itemCount desc
            """, result);
    }

    [Fact]
    public void ParseFilters_HandlesEmptyFilters()
    {
        var result = KQLQueryBuilder.ParseFilters(Array.Empty<string>());

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Theory]
    [InlineData("column='value'", "column", "value")]
    [InlineData("column = 'value'", "column", "value")]
    [InlineData("column = 'value with spaces'", "column", "value with spaces")]
    [InlineData("column = \"value with spaces\"", "column", "value with spaces")]
    [InlineData("column = 'value with \"quotes\"'", "column", "value with \"quotes\"")]
    [InlineData("column = \"value with 'quotes'\"", "column", "value with 'quotes'")]
    [InlineData("column = 'value with \\\"escaped quotes\\\"'", "column", "value with \\\"escaped quotes\\\"")]
    public void ParseFilters_HandlesSingleFilterWithVariousQuotationFormats(string filters, string expectedKey, string expectedValue)
    {
        var result = KQLQueryBuilder.ParseFilters(new string[] { filters });
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(expectedKey, result[0].Key);
        Assert.Equal(expectedValue, result[0].Value);
    }

    [Fact]
    public void ParseFilters_HandlesMultipleFiltersWithVariousQuotationFormats()
    {
        string[] filters = new string[]
        {
            "column='value'",
            "column = 'value'",
            "column = 'value with spaces'",
            "column = \"value with spaces\"",
            "column = 'value with \"quotes\"'",
            "column = \"value with 'quotes'\"",
            "column = 'value with \\\"escaped quotes\\\"'"
        };

        var result = KQLQueryBuilder.ParseFilters(filters);
        Assert.NotNull(result);
        Assert.Equal(7, result.Length);
        Assert.All(result, filter => Assert.Equal("column", filter.Key));
        Assert.Equal("value", result[0].Value);
        Assert.Equal("value", result[1].Value);
        Assert.Equal("value with spaces", result[2].Value);
        Assert.Equal("value with spaces", result[3].Value);
        Assert.Equal("value with \"quotes\"", result[4].Value);
        Assert.Equal("value with 'quotes'", result[5].Value);
        Assert.Equal("value with \\\"escaped quotes\\\"", result[6].Value);
    }

    [Theory]
    [InlineData("column = value")]
    [InlineData("column = 'value with no closing quote")]
    [InlineData("column = \"value with no closing quote")]
    public void ParseFilters_HandlesInvalidFilters(string filters)
    {
        ArgumentException result = Assert.Throws<ArgumentException>(() => KQLQueryBuilder.ParseFilters(new string[] { filters }));

        Assert.NotNull(result);
        Assert.Equal($"Invalid filter format: '{filters}'. Expected format is 'key=\"value\"'.", result.Message);
    }

    [Theory]
    [InlineData("2025-07-29T00:00:00Z", "2025-07-29T00:00:01Z", "2m")] // 1 second
    [InlineData("2025-07-29T00:00:00Z", "2025-07-29T00:01:00Z", "2m")] // 1 minute
    [InlineData("2025-07-29T00:00:00Z", "2025-07-29T01:00:00Z", "2m")] // 1 hour
    [InlineData("2025-07-29T00:00:00Z", "2025-07-29T04:00:00Z", "10m")] // 4 hours
    [InlineData("2025-07-29T00:00:00Z", "2025-07-29T04:00:01Z", "30m")] // 4 hours and 1 minute
    [InlineData("2025-07-29T00:00:00Z", "2025-07-29T12:00:00Z", "30m")] // 12 hours
    [InlineData("2025-07-29T00:00:00Z", "2025-07-29T12:00:01Z", "1h")] // 12 hours and 1 minute
    [InlineData("2025-07-29T00:00:00Z", "2025-07-30T00:00:00Z", "1h")] // 24 hours
    [InlineData("2025-07-29T00:00:00Z", "2025-07-30T00:00:01Z", "2h")] // 24 hours and 1 minute
    [InlineData("2025-07-29T00:00:00Z", "2025-08-01T00:00:00Z", "2h")] // 3 days
    [InlineData("2025-07-29T00:00:00Z", "2025-08-01T00:00:01Z", "6h")] // 3 days and 1 minute
    [InlineData("2025-07-29T00:00:00Z", "2025-08-05T00:00:00Z", "6h")] // 7 days
    [InlineData("2025-07-29T00:00:00Z", "2025-08-05T00:00:01Z", "12h")] // 7 days and 1 minute
    [InlineData("2025-07-29T00:00:00Z", "2025-08-12T00:00:00Z", "12h")] // 14 days
    [InlineData("2025-07-29T00:00:00Z", "2025-08-12T00:00:01Z", "1d")] // 14 days and 1 minute
    [InlineData("2025-07-29T00:00:00Z", "2025-08-28T00:00:00Z", "1d")] // 30 days
    [InlineData("2025-07-29T00:00:00Z", "2025-08-28T00:00:01Z", "2d")] // 30 days and 1 minute
    
    public void GetKqlInterval(string start, string end, string expected)
    {
        var result = KQLQueryBuilder.GetKqlInterval(DateTime.Parse(start), DateTime.Parse(end));
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetKqlFilters_HandlesEmptyFilters()
    {
        var parsedFilters = Array.Empty<KeyValuePair<string, string>>();

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetKqlFilters_ConvertsSingleFilterToKqlWhere()
    {
        var parsedFilters = new KeyValuePair<string, string>[]
        {
            new("resultCode", "500")
        };

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("| where resultCode contains \"500\"", result[0]);
    }

    [Fact]
    public void GetKqlFilters_ConvertsMultipleFiltersToKqlWhere()
    {
        var parsedFilters = new KeyValuePair<string, string>[]
        {
            new("resultCode", "500"),
            new("operation_Name", "GET /api/users"),
            new("success", "false")
        };

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
        Assert.Equal("| where resultCode contains \"500\"", result[0]);
        Assert.Equal("| where operation_Name contains \"GET /api/users\"", result[1]);
        Assert.Equal("| where success contains \"false\"", result[2]);
    }

    [Fact]
    public void GetKqlFilters_ExcludesDurationFiltersWithPercentile()
    {
        var parsedFilters = new KeyValuePair<string, string>[]
        {
            new("resultCode", "500"),
            new("duration", "95p"),
            new("Duration", "99p"), // Test case-insensitive
            new("operation_Name", "GET /api/users")
        };

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        Assert.Equal("| where resultCode contains \"500\"", result[0]);
        Assert.Equal("| where operation_Name contains \"GET /api/users\"", result[1]);
    }

    [Fact]
    public void GetKqlFilters_IncludesDurationFiltersWithoutPercentile()
    {
        var parsedFilters = new KeyValuePair<string, string>[]
        {
            new("resultCode", "500"),
            new("duration", "1000"),
            new("operation_Name", "GET /api/users")
        };

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
        Assert.Equal("| where resultCode contains \"500\"", result[0]);
        Assert.Equal("| where duration contains \"1000\"", result[1]);
        Assert.Equal("| where operation_Name contains \"GET /api/users\"", result[2]);
    }

    [Fact]
    public void GetKqlFilters_HandlesSpecialCharactersInValues()
    {
        var parsedFilters = new KeyValuePair<string, string>[]
        {
            new("operation_Name", "GET /api/users?name=\"John Doe\""),
            new("customDimension", "value with spaces"),
            new("errorMessage", "Internal server error: connection failed")
        };

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
        Assert.Equal("| where operation_Name contains \"GET /api/users?name=\"John Doe\"\"", result[0]);
        Assert.Equal("| where customDimension contains \"value with spaces\"", result[1]);
        Assert.Equal("| where errorMessage contains \"Internal server error: connection failed\"", result[2]);
    }

    [Theory]
    [InlineData("duration", "95p")]
    [InlineData("Duration", "99p")]
    [InlineData("DURATION", "50p")]
    [InlineData("duration", "100p")]
    public void GetKqlFilters_ExcludesDurationPercentileFilters_CaseInsensitive(string key, string value)
    {
        var parsedFilters = new KeyValuePair<string, string>[]
        {
            new("resultCode", "500"),
            new(key, value)
        };

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("| where resultCode contains \"500\"", result[0]);
    }

    [Theory]
    [InlineData("duration", "1000")]
    [InlineData("duration", "p95")]
    [InlineData("duration", "95")]
    [InlineData("other", "95p")]
    public void GetKqlFilters_IncludesNonPercentileDurationFilters(string key, string value)
    {
        var parsedFilters = new KeyValuePair<string, string>[]
        {
            new(key, value)
        };

        var result = KQLQueryBuilder.GetKqlFilters(parsedFilters);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal($"| where {key} contains \"{value}\"", result[0]);
    }

    [Fact]
    public void GetImpact_Test()
    {
        var result = KQLQueryBuilder.GetImpact("requests", new string[]
        {
            "operation_Name=\"GET /rescuepet\"",
            "success=\"false\""
        });
    }

    [Fact]
    public void GetImpact_Requests_WithNoFilters()
    {
        var result = KQLQueryBuilder.GetImpact("requests", Array.Empty<string>());

        Assert.Equal("""
            let total=requests
            | summarize TotalInstances=dcount(cloud_RoleInstance), TotalRequests=sum(itemCount) by cloud_RoleName;
            requests 
            | summarize ImpactedInstances=dcount(cloud_RoleInstance), ImpactedRequests=sum(itemCount) by cloud_RoleName
            | join kind=rightouter (total) on cloud_RoleName
            | extend ImpactedInstances = iff(isempty(ImpactedInstances), 0, ImpactedInstances)
            | extend ImpactedRequests = iff(isempty(ImpactedRequests), 0, ImpactedRequests)
            | project
                cloud_RoleName=cloud_RoleName1,
                ImpactedInstances,
                TotalInstances,
                TotalRequests,
                ImpactedRequests
            | extend
                ImpactedRequestsPercent = round((todouble(ImpactedRequests) / TotalRequests) * 100, 3),
                ImpactedInstancePercent = round((todouble(ImpactedInstances) / TotalInstances) * 100, 3)
                | order by ImpactedRequestsPercent desc
            """, result);
    }

    [Fact]
    public void GetImpact_Requests_WithFilters()
    {
        var result = KQLQueryBuilder.GetImpact("requests", new string[]
        {
            "resultCode='200'",
            "operation_Name='GET /api/users'"
        });

        Assert.Equal("""
            let total=requests
            | summarize TotalInstances=dcount(cloud_RoleInstance), TotalRequests=sum(itemCount) by cloud_RoleName;
            requests | where resultCode contains "200"
            | where operation_Name contains "GET /api/users"
            | summarize ImpactedInstances=dcount(cloud_RoleInstance), ImpactedRequests=sum(itemCount) by cloud_RoleName
            | join kind=rightouter (total) on cloud_RoleName
            | extend ImpactedInstances = iff(isempty(ImpactedInstances), 0, ImpactedInstances)
            | extend ImpactedRequests = iff(isempty(ImpactedRequests), 0, ImpactedRequests)
            | project
                cloud_RoleName=cloud_RoleName1,
                ImpactedInstances,
                TotalInstances,
                TotalRequests,
                ImpactedRequests
            | extend
                ImpactedRequestsPercent = round((todouble(ImpactedRequests) / TotalRequests) * 100, 3),
                ImpactedInstancePercent = round((todouble(ImpactedInstances) / TotalInstances) * 100, 3)
                | order by ImpactedRequestsPercent desc
            """, result);
    }

    [Fact]
    public void GetImpact_Exceptions_WithFilters()
    {
        var result = KQLQueryBuilder.GetImpact("exceptions", new string[]
        {
            "type='System.NullReferenceException'",
            "message='Object reference not set to an instance of an object.'"
        });
        Assert.Equal("""
            let total=exceptions
            | summarize TotalInstances=dcount(cloud_RoleInstance), TotalRequests=sum(itemCount) by cloud_RoleName;
            exceptions | where type contains "System.NullReferenceException"
            | where message contains "Object reference not set to an instance of an object."
            | summarize ImpactedInstances=dcount(cloud_RoleInstance), ImpactedRequests=sum(itemCount) by cloud_RoleName
            | join kind=rightouter (total) on cloud_RoleName
            | extend ImpactedInstances = iff(isempty(ImpactedInstances), 0, ImpactedInstances)
            | extend ImpactedRequests = iff(isempty(ImpactedRequests), 0, ImpactedRequests)
            | project
                cloud_RoleName=cloud_RoleName1,
                ImpactedInstances,
                TotalInstances,
                TotalRequests,
                ImpactedRequests
            | extend
                ImpactedRequestsPercent = round((todouble(ImpactedRequests) / TotalRequests) * 100, 3),
                ImpactedInstancePercent = round((todouble(ImpactedInstances) / TotalInstances) * 100, 3)
                | order by ImpactedRequestsPercent desc
            """, result);
    }

    [Fact]
    public void GetImpact_Dependencies_WithNoFilters()
    {
        var result = KQLQueryBuilder.GetImpact("dependencies", Array.Empty<string>());
        Assert.Equal("""
            let total=dependencies
            | summarize TotalInstances=dcount(cloud_RoleInstance), TotalRequests=sum(itemCount) by cloud_RoleName;
            dependencies 
            | summarize ImpactedInstances=dcount(cloud_RoleInstance), ImpactedRequests=sum(itemCount) by cloud_RoleName
            | join kind=rightouter (total) on cloud_RoleName
            | extend ImpactedInstances = iff(isempty(ImpactedInstances), 0, ImpactedInstances)
            | extend ImpactedRequests = iff(isempty(ImpactedRequests), 0, ImpactedRequests)
            | project
                cloud_RoleName=cloud_RoleName1,
                ImpactedInstances,
                TotalInstances,
                TotalRequests,
                ImpactedRequests
            | extend
                ImpactedRequestsPercent = round((todouble(ImpactedRequests) / TotalRequests) * 100, 3),
                ImpactedInstancePercent = round((todouble(ImpactedInstances) / TotalInstances) * 100, 3)
                | order by ImpactedRequestsPercent desc
            """, result);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_WithDefaultParameters()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
        }, interval, start, end);

        Assert.Equal("Count of requests ", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            requests
            | extend split = 'Overall requests Count'
            | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
            | project Value, split
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_AvailabilityResults_WithDefaultParameters()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "availabilityResults",
        }, interval, start, end);

        Assert.Equal("Count of availabilityResults ", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            availabilityResults
            | extend split = 'Overall availabilityResults Count'
            | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
            | project Value, split
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_WithSplitBy()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
            SplitBy = "client_Type"
        }, interval, start, end);

        Assert.Equal("Count of requests  split by client_Type", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            let top10 = requests
            | where timestamp > start and timestamp < end
            | summarize sum(itemCount) by client_Type
            | top 10 by sum_itemCount desc
            | project split=client_Type;
            requests
            | extend split=client_Type
            | where split in (top10)
            | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
            | project Value, split=strcat('client_Type=', split)
            | union (requests
                | extend split = 'Overall requests Count'
                | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
                | project Value, split)
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_WithFiltersAndSplitBy()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
            SplitBy = "client_Type",
            Filters = new string[] { "resultCode='200'", "operation_Name='GET /api/users'" }
        }, interval, start, end);

        Assert.Equal("Count of requests where resultCode=\"200\" and operation_Name=\"GET /api/users\" split by client_Type", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            let top10 = requests| where resultCode contains "200"
            | where operation_Name contains "GET /api/users"
            | where timestamp > start and timestamp < end
            | summarize sum(itemCount) by client_Type
            | top 10 by sum_itemCount desc
            | project split=client_Type;
            requests| where resultCode contains "200"
            | where operation_Name contains "GET /api/users"
            | extend split=client_Type
            | where split in (top10)
            | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
            | project Value, split=strcat('client_Type=', split)
            | union (requests| where resultCode contains "200"
            | where operation_Name contains "GET /api/users"
                | extend split = 'Overall requests Count'
                | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
                | project Value, split)
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_WithAverageAndSplitBy()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
            SplitBy = "client_Type",
            Aggregation = "Average"
        }, interval, start, end);

        Assert.Equal("Average duration of requests  split by client_Type", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            let top10 = requests
            | where timestamp > start and timestamp < end
            | summarize sum(itemCount) by client_Type
            | top 10 by sum_itemCount desc
            | project split=client_Type;
            requests
            | extend split=client_Type
            | where split in (top10)
            | make-series Value=avg(duration) default=0 on timestamp from start to end step interval by split
            | project Value, split=strcat('client_Type=', split)
            | union (requests
                | extend split = 'Overall requests Average duration'
                | make-series Value=avg(duration) default=0 on timestamp from start to end step interval by split
                | project Value, split)
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_WithAverageAndNoSplitBy()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
            Aggregation = "Average"
        }, interval, start, end);

        Assert.Equal("Average duration of requests ", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            requests
            | extend split = 'Overall requests Average duration'
            | make-series Value=avg(duration) default=0 on timestamp from start to end step interval by split
            | project Value, split
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_With95thPercentileAndSplitBy()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
            SplitBy = "client_Type",
            Aggregation = "95thPercentile"
        }, interval, start, end);

        Assert.Equal("95th Percentile duration of requests  split by client_Type", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            let top10 = requests
            | where timestamp > start and timestamp < end
            | summarize sum(itemCount) by client_Type
            | top 10 by sum_itemCount desc
            | project split=client_Type;
            requests
            | extend split=client_Type
            | where split in (top10)
            | make-series Value=percentile(duration, 95) default=0 on timestamp from start to end step interval by split
            | project Value, split=strcat('client_Type=', split)
            | union (requests
                | extend split = 'Overall requests 95th Percentile duration'
                | make-series Value=percentile(duration, 95) default=0 on timestamp from start to end step interval by split
                | project Value, split)
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_With9thPercentileAndNoSplitBy()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
            Aggregation = "95thPercentile"
        }, interval, start, end);

        Assert.Equal("95th Percentile duration of requests ", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            requests
            | extend split = 'Overall requests 95th Percentile duration'
            | make-series Value=percentile(duration, 95) default=0 on timestamp from start to end step interval by split
            | project Value, split
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }

    [Fact]
    public void BuildTimeSeriesQuery_Requests_WithFiltersAndNoSplitBy()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "requests",
            Filters = new string[] { "resultCode='200'", "operation_Name='GET /api/users'" }
        }, interval, start, end);

        Assert.Equal("Count of requests where resultCode=\"200\" and operation_Name=\"GET /api/users\"", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            requests| where resultCode contains "200"
            | where operation_Name contains "GET /api/users"
            | extend split = 'Overall requests Count'
            | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
            | project Value, split
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }


    [Fact]
    public void BuildTimeSeriesQuery_Exceptions_WithDefaultParameters()
    {
        DateTime start = DateTime.UtcNow.AddDays(-1);
        DateTime end = DateTime.UtcNow;
        string interval = "1h";
        var result = KQLQueryBuilder.BuildTimeSeriesQuery(new AppCorrelateDataSet
        {
            Table = "exceptions",
        }, interval, start, end);

        Assert.Equal("Count of exceptions ", result.description);

        Assert.Equal($"""
            let start = datetime({start:O});
            let end = datetime({end:O});
            let interval = 1h;
            exceptions
            | extend split = 'Overall exceptions Count'
            | make-series Value=sum(itemCount) default=0 on timestamp from start to end step interval by split
            | project Value, split
            | extend startTime=start, endTime = end
            | project startTime, endTime, split, Value
            """, result.query);
    }
}
