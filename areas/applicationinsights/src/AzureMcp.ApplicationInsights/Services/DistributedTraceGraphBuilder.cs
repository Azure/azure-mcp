// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;

namespace AzureMcp.ApplicationInsights.Services
{
    public class DistributedTraceGraphBuilder(string traceId)
    {
        private readonly Dictionary<int, List<string>> _parents = new();
        private readonly Dictionary<string, SpanSummary> _indexBySpanId = new();
        private readonly Dictionary<string, List<SpanSummary>> _children = new();
        private readonly List<SpanSummary> _spans = new();
        private readonly string _traceId = traceId;

        public DistributedTraceGraphBuilder AddSpans(IEnumerable<SpanSummary> spans)
        {
            int rowId = 0;

            // Process each row in the query results
            foreach (var row in spans)
            {
                row.RowId = rowId; // Assign a unique RowId to each span
                this.AddSpan(row);
                rowId++;
            }

            return this;
        }

        public DistributedTraceGraphBuilder AddSpan(SpanSummary span)
        {
            string? currentSpanId = span.SpanId;
            string? parentSpanId = span.ParentId;

            if (parentSpanId != null && currentSpanId != parentSpanId && parentSpanId != _traceId)
            {
                if (_parents.TryGetValue(span.RowId, out List<string>? existingParents))
                {
                    // If we already have parents for this span, add the new parent
                    existingParents.Add(parentSpanId);
                }
                else
                {
                    // Otherwise, create a new entry for this span with its parent
                    _parents[span.RowId] = new List<string> { parentSpanId };
                }

                if (_children.TryGetValue(parentSpanId, out List<SpanSummary>? existingChildren))
                {
                    // If we already have children for this parent, add the new child
                    existingChildren.Add(span);
                }
                else
                {
                    // Otherwise, create a new entry for this parent with its child
                    _children[parentSpanId] = new List<SpanSummary> { span };
                }
            }

            if (currentSpanId != null)
            {
                _indexBySpanId[currentSpanId] = span;
            }

            _spans.Add(span);

            return this;
        }

        public DistributedTraceGraph Build()
        {
            foreach (var span in _spans)
            {
                span.ChildSpans = span.SpanId != null && _children.TryGetValue(span.SpanId, out List<SpanSummary>? childSpans) ? childSpans : new List<SpanSummary>();

                if (_parents.TryGetValue(span.RowId, out List<string>? parentIds) && parentIds?.Count > 0)
                {
                    span.ParentSpan = parentIds.Select(p => _indexBySpanId.TryGetValue(p, out SpanSummary? parentSpan) ? parentSpan : null).FirstOrDefault();
                }
            }

            return new DistributedTraceGraph(_spans.ToList());
        }
    }
}
