namespace ToolSelection.Models;

public class SuccessRateMetrics
{
    public int TopChoiceCount { get; set; }
    public int HighConfidenceCount { get; set; }
    public int TopChoiceHighConfidenceCount { get; set; }
    public int TotalTests { get; set; }

    public double TopChoicePercentage => (double)TopChoiceCount / TotalTests * 100;
    public double HighConfidencePercentage => (double)HighConfidenceCount / TotalTests * 100;
    public double TopChoiceHighConfidencePercentage => (double)TopChoiceHighConfidenceCount / TotalTests * 100;
}
