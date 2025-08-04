// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Services;
using Xunit;

namespace AzureMcp.ApplicationInsights.UnitTests.Models
{
    [Trait("Area", "ApplicationInsights")]
    public class DistributedTraceResultTests
    {
        private static readonly List<SpanSummary> SingleRequestSpan = new List<SpanSummary>
        {
            new SpanSummary
            {
                ItemId = "span1ItemId",
                ItemType = "request",
                Name = "GET /api/values",
                IsSuccessful = true,
                ResponseCode = "200",
                StartTime = DateTime.Parse("2025-05-20T00:00:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:01:00Z"),
                Duration = TimeSpan.FromMinutes(1),
                SpanId = "span1",
                ParentId = null,
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("http.method", "GET"),
                    new KeyValuePair<string, string>("http.url", "/api/values")
                }
            }
        };

        private static readonly List<SpanSummary> FullChainToStackTraceSpan = new List<SpanSummary>
        {
            new SpanSummary
            {
                ItemId = "span3ItemId",
                ItemType = "exception",
                Name = "NullReferenceException",
                IsSuccessful = false,
                ResponseCode = "500",
                StartTime = DateTime.Parse("2025-05-20T00:01:30Z"),
                EndTime = DateTime.Parse("2025-05-20T00:02:00Z"),
                Duration = TimeSpan.FromSeconds(30),
                SpanId = "span3",
                ParentId = "span2",
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("exception.message", "Object reference not set to an instance of an object")
                }
            },
            new SpanSummary
            {
                ItemId = "span2ItemId",
                ItemType = "dependency",
                Name = "SQL Database Query",
                IsSuccessful = false,
                ResponseCode = "500",
                StartTime = DateTime.Parse("2025-05-20T00:01:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:01:30Z"),
                Duration = TimeSpan.FromSeconds(30),
                SpanId = "span2",
                ParentId = "span1",
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("db.statement", "SELECT * FROM Users")
                }
            },
            new SpanSummary
            {
                ItemId = "span1ItemId",
                ItemType = "request",
                Name = "GET /api/values",
                IsSuccessful = false,
                ResponseCode = "500",
                StartTime = DateTime.Parse("2025-05-20T00:00:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:02:00Z"),
                Duration = TimeSpan.FromMinutes(1),
                SpanId = "span1",
                ParentId = null,
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("http.method", "GET"),
                    new KeyValuePair<string, string>("http.url", "/api/values")
                }
            }
        };

        private static readonly List<SpanSummary> MultipleRootSpansWithFullTraces = new List<SpanSummary>
        {
            // dependency call to bing.com
            new SpanSummary
            {
                ItemId = "span4ItemId",
                ItemType = "dependency",
                Name = "HTTP GET bing.com",
                IsSuccessful = true,
                ResponseCode = "200",
                StartTime = DateTime.Parse("2025-05-20T00:01:30Z"),
                EndTime = DateTime.Parse("2025-05-20T00:01:45Z"),
                Duration = TimeSpan.FromSeconds(15),
                SpanId = "span4",
                ParentId = "span2",
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("http.method", "GET"),
                    new KeyValuePair<string, string>("http.url", "https://www.bing.com")
                }
            },
            new SpanSummary
            {
                ItemId = "span1ItemId",
                ItemType = "request",
                Name = "GET /api/values",
                IsSuccessful = false,
                ResponseCode = "500",
                StartTime = DateTime.Parse("2025-05-20T00:00:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:02:00Z"),
                Duration = TimeSpan.FromMinutes(1),
                SpanId = "span1",
                ParentId = null,
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("http.method", "GET"),
                    new KeyValuePair<string, string>("http.url", "/api/values")
                }
            },
            new SpanSummary
            {
                ItemId = "span2ItemId",
                ItemType = "dependency",
                Name = "SQL Database Query",
                IsSuccessful = false,
                ResponseCode = "500",
                StartTime = DateTime.Parse("2025-05-20T00:01:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:01:30Z"),
                Duration = TimeSpan.FromSeconds(30),
                SpanId = "span2",
                ParentId = "span1",
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("db.statement", "SELECT * FROM Users")
                }
            },
            new SpanSummary
            {
                ItemId = "span3ItemId",
                ItemType = "exception",
                Name = "NullReferenceException",
                IsSuccessful = false,
                ResponseCode = "500",
                StartTime = DateTime.Parse("2025-05-20T00:01:30Z"),
                EndTime = DateTime.Parse("2025-05-20T00:02:00Z"),
                Duration = TimeSpan.FromSeconds(30),
                SpanId = "span3",
                ParentId = "span2",
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("exception.message", "Object reference not set to an instance of an object")
                }
            },
            new SpanSummary
            {
                RowId = 4,
                ItemId = "span6ItemId",
                ItemType = "dependency",
                Name = "SQL Database Insert",
                IsSuccessful = true,
                ResponseCode = "201",
                StartTime = DateTime.Parse("2025-05-20T00:02:30Z"),
                EndTime = DateTime.Parse("2025-05-20T00:02:45Z"),
                Duration = TimeSpan.FromSeconds(15),
                SpanId = "span6",
                ParentId = "span5",
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("db.statement", "INSERT INTO Users (Name) VALUES ('Test User')")
                }
            },
            // Add another request -> dependency call (successful) that is a root span
            new SpanSummary
            {
                RowId = 5,
                ItemId = "span5ItemId",
                ItemType = "request",
                Name = "POST /api/values",
                IsSuccessful = true,
                ResponseCode = "201",
                StartTime = DateTime.Parse("2025-05-20T00:02:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:03:00Z"),
                Duration = TimeSpan.FromMinutes(1),
                SpanId = "span5",
                ParentId = null,
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("http.method", "POST"),
                    new KeyValuePair<string, string>("http.url", "/api/values")
                }
            }
        };

        private static readonly List<SpanSummary> SpanWithBrokenReferences = new List<SpanSummary>
        {
            new SpanSummary
            {
                ItemId = "span1ItemId",
                ItemType = "request",
                Name = "GET /api/values",
                IsSuccessful = true,
                ResponseCode = "200",
                StartTime = DateTime.Parse("2025-05-20T00:00:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:01:00Z"),
                Duration = TimeSpan.FromMinutes(1),
                SpanId = "span1",
                ParentId = "span2",
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("http.method", "GET"),
                    new KeyValuePair<string, string>("http.url", "/api/values")
                }
            },
            new SpanSummary
            {
                ItemId = "span2ItemId",
                ItemType = "dependency",
                Name = "SQL Database Query",
                IsSuccessful = false,
                ResponseCode = "500",
                StartTime = DateTime.Parse("2025-05-20T00:01:00Z"),
                EndTime = DateTime.Parse("2025-05-20T00:01:30Z"),
                Duration = TimeSpan.FromSeconds(30),
                SpanId = "span2",
                ParentId = "span1", // Circular reference
                Properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("db.statement", "SELECT * FROM Users")
                }
            }
        };

        [Fact]
        public void SingleRequestSpan_PrintsDescription()
        {
            DistributedTraceResult result = DistributedTraceResult.Create("trace1", SingleRequestSpan);

            Assert.NotNull(result);
            Assert.Equal("This represents a distributed trace. Parent/child relationships are represented by indentation. Columns: ItemId, ItemType, Name, Success, ResultCode, StartToEnd (milliseconds)", result.Description);
            Assert.Equal("span1ItemId, request, GET /api/values, ✅, 200, 0->60000\r\n", result.TraceDetails);
            Assert.Equal(SingleRequestSpan[0].StartTime, result.StartTime);
            Assert.Empty(result.RelevantSpans);
        }

        [Fact]
        public void FullChainToStackTraceSpan_PrintsDescription()
        {
            var spans = new DistributedTraceGraphBuilder("trace1")
                .AddSpans(FullChainToStackTraceSpan)
                .Build()
                .FilterSpansById(null);
            DistributedTraceResult result = DistributedTraceResult.Create("trace1", spans);

            Assert.NotNull(result);
            Assert.Equal("This represents a distributed trace. Parent/child relationships are represented by indentation. Columns: ItemId, ItemType, Name, Success, ResultCode, StartToEnd (milliseconds)", result.Description);
            Assert.Equal("""
                span1ItemId, request, GET /api/values, ❌, 500, 0->120000
                    span2ItemId, dependency, SQL Database Query, ❌, 500, 60000->90000
                        span3ItemId, exception, NullReferenceException, ⚠️, 500, 90000->120000

                """, result.TraceDetails);
            Assert.Equal(SingleRequestSpan[0].StartTime, result.StartTime);
            Assert.Single(result.RelevantSpans);
            Assert.Contains(result.RelevantSpans, span => span.ItemId == "span3ItemId");
        }

        [Fact]
        public void MultipleRootSpansWithFullTraces_PrintsDescription()
        {
            var spans = new DistributedTraceGraphBuilder("trace1")
                .AddSpans(MultipleRootSpansWithFullTraces)
                .Build()
                .FilterSpansById(null);
            DistributedTraceResult result = DistributedTraceResult.Create("trace1", spans);

            Assert.NotNull(result);
            Assert.Equal("This represents a distributed trace. Parent/child relationships are represented by indentation. Columns: ItemId, ItemType, Name, Success, ResultCode, StartToEnd (milliseconds)", result.Description);
            Assert.Equal("""
                span1ItemId, request, GET /api/values, ❌, 500, 0->120000
                    span2ItemId, dependency, SQL Database Query, ❌, 500, 60000->90000
                        span4ItemId, dependency, HTTP GET bing.com, ✅, 200, 90000->105000
                        span3ItemId, exception, NullReferenceException, ⚠️, 500, 90000->120000
                span5ItemId, request, POST /api/values, ✅, 201, 120000->180000
                    span6ItemId, dependency, SQL Database Insert, ✅, 201, 150000->165000

                """, result.TraceDetails);
            Assert.Equal(SingleRequestSpan[0].StartTime, result.StartTime);
            Assert.Single(result.RelevantSpans);
            Assert.Contains(result.RelevantSpans, span => span.ItemId == "span3ItemId");
        }

        [Fact]
        public void SpanWithBrokenReferences_PrintsDescription()
        {
            var spans = new DistributedTraceGraphBuilder("trace1")
                .AddSpans(SpanWithBrokenReferences)
                .Build()
                .FilterSpansById(null);
            DistributedTraceResult result = DistributedTraceResult.Create("trace1", spans);

            Assert.NotNull(result);
            Assert.Equal("This represents a distributed trace. Parent/child relationships are represented by indentation. Columns: ItemId, ItemType, Name, Success, ResultCode, StartToEnd (milliseconds)", result.Description);
            Assert.Equal("""
                span1ItemId, request, GET /api/values, ✅, 200, 0->60000
                    span2ItemId, dependency, SQL Database Query, ❌, 500, 60000->90000

                """, result.TraceDetails);
            Assert.Equal(SingleRequestSpan[0].StartTime, result.StartTime);
            Assert.Empty(result.RelevantSpans);
        }

        [Fact]
        public void NoSpans_PrintsDescription()
        {
            var spans = new DistributedTraceGraphBuilder("trace1")
                .AddSpans(SpanWithBrokenReferences)
                .Build()
                .FilterSpansById("doesntexist");
            DistributedTraceResult result = DistributedTraceResult.Create("trace1", spans);

            Assert.NotNull(result);
            Assert.Equal("No spans available for the selected trace and span. Try a different traceId, spanId, or time range filter", result.Description);
            Assert.Null(result.TraceDetails);
            Assert.Empty(result.RelevantSpans);
        }
    }
}
