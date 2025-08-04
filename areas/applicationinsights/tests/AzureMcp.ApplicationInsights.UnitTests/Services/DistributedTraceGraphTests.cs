// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;
using AzureMcp.ApplicationInsights.Services;
using Xunit;

namespace AzureMcp.ApplicationInsights.UnitTests.Services;

[Trait("Area", "ApplicationInsights")]
public class DistributedTraceGraphTests
{
    private static readonly List<SpanSummary> SingleSpanTrace = new()
    {
        new SpanSummary
        {
            SpanId = "span1",
            ParentId = null,
            Name = "RootSpan",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1),
            ChildSpans = new List<SpanSummary>()
        }
    };

    private static readonly List<SpanSummary> AllRootSpanTrace = new()
    {
        new SpanSummary
        {
            SpanId = "span1",
            ParentId = null,
            Name = "RootSpan1",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1),
            ChildSpans = new List<SpanSummary>()
        },
        new SpanSummary
        {
            SpanId = "span2",
            ParentId = null,
            Name = "RootSpan2",
            StartTime = DateTime.UtcNow.AddSeconds(2),
            EndTime = DateTime.UtcNow.AddSeconds(3),
            ChildSpans = new List<SpanSummary>()
        }
    };

    private static readonly List<SpanSummary> MultiLayerTrace = new()
    {
        new SpanSummary
        {
            SpanId = "span1",
            ParentId = null,
            Name = "RootSpan",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1)
        },
        new SpanSummary
        {
            SpanId = "span2",
            ParentId = "span1",
            Name = "ChildSpan1",
            StartTime = DateTime.UtcNow.AddSeconds(0.5),
            EndTime = DateTime.UtcNow.AddSeconds(0.8),
            ChildSpans = new List<SpanSummary>()
        },
        new SpanSummary
        {
            SpanId = "span3",
            ParentId = "span2",
            Name = "ChildSpan2",
            StartTime = DateTime.UtcNow.AddSeconds(0.6),
            EndTime = DateTime.UtcNow.AddSeconds(0.9),
            ChildSpans = new List<SpanSummary>()
        },
        new SpanSummary
        {
            SpanId = "span4",
            ParentId = null,
            Name = "RootSpan2",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1)
        },
        new SpanSummary
        {
            SpanId = "span5",
            ParentId = "span4",
            Name = "ChildSpan3",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1)
        },
    };

    private static readonly List<SpanSummary> TraceWithNonExistentParents = new()
    {
        new SpanSummary
        {
            SpanId = "span1",
            ParentId = null,
            Name = "RootSpan",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1)
        },
        new SpanSummary
        {
            SpanId = "span2",
            ParentId = "nonExistentParent",
            Name = "ChildSpan1",
            StartTime = DateTime.UtcNow.AddSeconds(0.5),
            EndTime = DateTime.UtcNow.AddSeconds(0.8)
        }
    };

    private static readonly List<SpanSummary> TraceWithMultipleChildren = new()
    {
        new SpanSummary
        {
            SpanId = "span1",
            ParentId = null,
            Name = "RootSpan",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1)
        },
        new SpanSummary
        {
            SpanId = "span2",
            ParentId = "span1",
            Name = "ChildSpan1",
            StartTime = DateTime.UtcNow.AddSeconds(0.5),
            EndTime = DateTime.UtcNow.AddSeconds(0.8)
        },
        new SpanSummary
        {
            SpanId = "span3",
            ParentId = "span1",
            Name = "ChildSpan2",
            StartTime = DateTime.UtcNow.AddSeconds(0.6),
            EndTime = DateTime.UtcNow.AddSeconds(0.9)
        },
        new SpanSummary
        {
            SpanId = "span4",
            ParentId = null,
            Name = "RootSpan2",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddSeconds(1)
        }
    };

    [Fact]
    public void SingleSpanTrace_NoSpanId_ReturnsRootSpan()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(SingleSpanTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById(null);
        // Assert
        Assert.Single(result);
        Assert.Equal("span1", result[0].SpanId);
    }

    [Fact]
    public void SingleSpanTrace_WithMatchingSpanId_ReturnsSpan()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(SingleSpanTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById("span1");
        // Assert
        Assert.Single(result);
        Assert.Equal("span1", result[0].SpanId);
    }

    [Fact]
    public void SingleSpanTrace_WithNonMatchingSpanId_ReturnsEmptyList()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(SingleSpanTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById("nonExistentSpan");
        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void AllRootSpanTrace_NoSpanId_ReturnsAllRootSpans()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(AllRootSpanTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById(null);
        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, span => span.SpanId == "span1");
        Assert.Contains(result, span => span.SpanId == "span2");
    }

    [Fact]
    public void AllRootSpanTrace_WithMatchingSpanId_Returns1Span()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(AllRootSpanTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById("span1");
        // Assert
        Assert.Single(result);
        Assert.Equal("span1", result[0].SpanId);
    }

    [Fact]
    public void AllRootSpanTrace_WithNonMatchingSpanId_ReturnsEmptyList()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(AllRootSpanTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById("nonExistentSpan");
        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void MultiLayerTrace_NoSpanId_ReturnsAllRootSpans()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(MultiLayerTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById(null);
        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, span => span.SpanId == "span1");
        Assert.Contains(result, span => span.SpanId == "span4");
    }

    [Fact]
    public void MultiLayerTrace_WithMatchingSpanIds_ReturnsSpanAndAncestorsAndChildren()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(MultiLayerTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById("span2");
        // Assert
        Assert.Equal(3, result.Count);
        Assert.Contains(result, span => span.SpanId == "span1");
        Assert.Contains(result, span => span.SpanId == "span2");
        Assert.Contains(result, span => span.SpanId == "span3");
    }

    [Fact]
    public void MultiLayerTrace_WithNonMatchingSpanId_ReturnsEmptyList()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(MultiLayerTrace)
            .Build();
        // Act
        var result = graph.FilterSpansById("nonExistentSpan");
        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void TraceWithNonExistentParents_NoSpanId_ReturnsAllRootSpans()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(TraceWithNonExistentParents)
            .Build();
        // Act
        var result = graph.FilterSpansById(null);
        // Assert
        Assert.Contains(result, span => span.SpanId == "span1");
        Assert.Contains(result, span => span.SpanId == "span2");
    }

    [Fact]
    public void TraceWithNonExistentParents_WithMatchingSpanId_ReturnsMatchingSpan()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(TraceWithNonExistentParents)
            .Build();
        // Act
        var result = graph.FilterSpansById("span2");
        // Assert
        Assert.Single(result);
        Assert.Equal("span2", result[0].SpanId);
    }

    [Fact]
    public void TraceWithNonExistentParents_WithNonMatchingSpanId_ReturnsEmptyList()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(TraceWithNonExistentParents)
            .Build();
        // Act
        var result = graph.FilterSpansById("nonExistentSpan");
        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void TraceWithMultipleChildren_NoSpanId_ReturnsAllRootSpans()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(TraceWithMultipleChildren)
            .Build();
        // Act
        var result = graph.FilterSpansById(null);
        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, span => span.SpanId == "span1");
        Assert.Contains(result, span => span.SpanId == "span4");
    }

    [Fact]
    public void TraceWithMultipleChildren_WithMatchingSpanId_ReturnsSpanAndAncestorsAndChildren()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(TraceWithMultipleChildren)
            .Build();
        // Act
        var result = graph.FilterSpansById("span1");
        // Assert
        Assert.Equal(3, result.Count);
        Assert.Contains(result, span => span.SpanId == "span1");
        Assert.Contains(result, span => span.SpanId == "span2");
        Assert.Contains(result, span => span.SpanId == "span3");
    }

    [Fact]
    public void TraceWithMultipleChildren_WithNonMatchingSpanId_ReturnsEmptyList()
    {
        // Arrange
        var graph = new DistributedTraceGraphBuilder("trace1")
            .AddSpans(TraceWithMultipleChildren)
            .Build();
        // Act
        var result = graph.FilterSpansById("nonExistentSpan");
        // Assert
        Assert.Empty(result);
    }

}
