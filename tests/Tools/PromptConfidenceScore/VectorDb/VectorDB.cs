using System.Collections.Concurrent;

namespace ToolSelection.VectorDb;

public record Entry(string Id, object? Metadata, float[] Vector);

public record QueryResult(float Score, Entry Entry);

public record QueryOptions(int TopK = 10, float MinimumScore = 0.0f, Func<Entry, bool>? Predicate = null);

public interface IDistanceMetric
{
    float Distance(float[] a, float[] b);
    bool BiggerIsCloser { get; }
}

public class CosineSimilarity : IDistanceMetric
{
    public bool BiggerIsCloser => true;  // Cosine similarity: 1 = most similar, -1 = least similar

    public float Distance(float[] a, float[] b)
    {
        if (a.Length != b.Length)
            throw new ArgumentException("Vector lengths must match");

        double dotProduct = 0.0;
        double magnitudeA = 0.0;
        double magnitudeB = 0.0;

        for (int i = 0; i < a.Length; i++)
        {
            dotProduct += a[i] * b[i];
            magnitudeA += Math.Pow(a[i], 2);
            magnitudeB += Math.Pow(b[i], 2);
        }

        return (float)(dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB)));
    }
}

public class DotProduct : IDistanceMetric
{
    public bool BiggerIsCloser => true;

    public float Distance(float[] a, float[] b)
    {
        if (a.Length != b.Length)
            throw new ArgumentException("Vector lengths must match");

        float dotProduct = 0.0f;
        for (int i = 0; i < a.Length; i++)
        {
            dotProduct += a[i] * b[i];
        }

        return dotProduct;
    }
}

public class VectorDB
{
    private readonly ReaderWriterLockSlim _lock = new();
    private readonly List<Entry> _entries = new();
    private readonly IDistanceMetric _distanceMetric;

    public VectorDB(IDistanceMetric distanceMetric, IEnumerable<Entry>? entries = null)
    {
        _distanceMetric = distanceMetric;
        if (entries != null)
        {
            _entries.AddRange(entries);
            _entries.Sort((a, b) => string.Compare(a.Id, b.Id, StringComparison.Ordinal));
        }
    }

    public int Count
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _entries.Count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    private int BinarySearch(string id)
    {
        return _entries.BinarySearch(new Entry(id, null, Array.Empty<float>()),
            Comparer<Entry>.Create((a, b) => string.Compare(a.Id, b.Id, StringComparison.Ordinal)));
    }

    public void Upsert(Entry entry)
    {
        _lock.EnterWriteLock();
        try
        {
            int index = BinarySearch(entry.Id);
            if (index >= 0)
            {
                _entries[index] = entry;
            }
            else
            {
                _entries.Insert(~index, entry);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public Entry? Get(string id)
    {
        _lock.EnterReadLock();
        try
        {
            int index = BinarySearch(id);
            return index >= 0 ? _entries[index] : null;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public bool Delete(string id)
    {
        _lock.EnterWriteLock();
        try
        {
            int index = BinarySearch(id);
            if (index >= 0)
            {
                _entries.RemoveAt(index);
                return true;
            }
            return false;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public List<QueryResult> Query(float[] vector, QueryOptions options)
    {
        _lock.EnterReadLock();
        try
        {
            return QuerySlice(_entries, vector, options);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    private List<QueryResult> QuerySlice(IEnumerable<Entry> entries, float[] vector, QueryOptions options)
    {
        const int threshold = 100;
        var entryList = entries.ToList();

        if (entryList.Count > threshold)
        {
            int half = entryList.Count / 2;
            var leftTask = Task.Run(() => QuerySlice(entryList.Take(half), vector, options));
            var rightResult = QuerySlice(entryList.Skip(half), vector, options);
            var leftResult = leftTask.Result;

            // Merge results
            var mergedResults = new List<QueryResult>(leftResult.Count + rightResult.Count);
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < leftResult.Count && rightIndex < rightResult.Count)
            {
                bool takeLeft = _distanceMetric.BiggerIsCloser
                    ? leftResult[leftIndex].Score >= rightResult[rightIndex].Score
                    : leftResult[leftIndex].Score <= rightResult[rightIndex].Score;

                if (takeLeft)
                {
                    mergedResults.Add(leftResult[leftIndex++]);
                }
                else
                {
                    mergedResults.Add(rightResult[rightIndex++]);
                }
            }

            // Add remaining results
            while (leftIndex < leftResult.Count)
                mergedResults.Add(leftResult[leftIndex++]);
            while (rightIndex < rightResult.Count)
                mergedResults.Add(rightResult[rightIndex++]);

            return mergedResults;
        }

        var results = new List<QueryResult>();

        foreach (var entry in entryList)
        {
            if (options.Predicate != null && !options.Predicate(entry))
                continue;

            float score = _distanceMetric.Distance(vector, entry.Vector);
            if (score < options.MinimumScore)
                continue;

            var queryResult = new QueryResult(score, entry);

            // Find insertion point
            int insertIndex = results.BinarySearch(queryResult,
                Comparer<QueryResult>.Create((a, b) =>
                {
                    int result = a.Score.CompareTo(b.Score);
                    return _distanceMetric.BiggerIsCloser ? -result : result;
                }));

            if (insertIndex < 0)
                insertIndex = ~insertIndex;

            if (insertIndex == options.TopK)
            {
                // Score is worse than all current results, skip
                continue;
            }

            if (results.Count == options.TopK)
            {
                // Remove the worst result
                results.RemoveAt(results.Count - 1);
            }

            results.Insert(insertIndex, queryResult);
        }

        return results;
    }

    public void Dispose()
    {
        _lock?.Dispose();
    }
}
