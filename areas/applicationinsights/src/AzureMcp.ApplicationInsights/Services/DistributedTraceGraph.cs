// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;

namespace AzureMcp.ApplicationInsights.Services
{
    public class DistributedTraceGraph(List<SpanSummary> spans)
    {
        private readonly List<SpanSummary> _spans = spans;

        public List<SpanSummary> FilterSpansById(string? spanId)
        {
            List<SpanSummary> filteredSpans = new List<SpanSummary>();
            if (spanId == null)
            {
                // find the root spans
                filteredSpans = _spans.Where(t => t.ParentSpan == null).ToList();
                if (_spans.Count > 0 && filteredSpans.Count == 0)
                {
                    // If no root spans were found, return all spans
                    filteredSpans = _spans;
                }
            }
            else
            {
                var targetSpan = _spans.FirstOrDefault(t => t.SpanId == spanId);

                if (targetSpan == null)
                {
                    // If the specified span wasn't found, return an empty result
                    filteredSpans = new List<SpanSummary>();
                }
                else
                {
                    // Filter to only include the specified span, its ancestors, and direct descendants
                    var currentSpan = targetSpan;
                    // ancestors
                    while (currentSpan.ParentSpan != null)
                    {
                        filteredSpans.Add(currentSpan.ParentSpan);
                        currentSpan = currentSpan.ParentSpan;
                    }
                    // target span
                    filteredSpans.Add(targetSpan);
                    // children
                    filteredSpans.AddRange(targetSpan.ChildSpans);
                }
            }
            return filteredSpans;
        }
    }
}
