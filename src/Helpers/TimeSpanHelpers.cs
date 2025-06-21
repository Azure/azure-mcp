namespace AzureMcp.Helpers
{
    public static class TimeSpanHelpers
    {
        /// <summary>
        /// Parses an ISO 8601 duration string (e.g., PT1M, PT5M, PT1H, P1D) into a TimeSpan
        /// </summary>
        /// <param name="iso8601Duration">The ISO 8601 duration string to parse</param>
        /// <param name="result">The parsed TimeSpan if successful</param>
        /// <returns>True if parsing was successful, false otherwise</returns>
        public static bool TryParseISO8601Duration(string iso8601Duration, out TimeSpan result)
        {
            result = TimeSpan.Zero;

            if (string.IsNullOrWhiteSpace(iso8601Duration))
                return false;

            // ISO 8601 duration format: P[n]Y[n]M[n]DT[n]H[n]M[n]S or P[n]W
            // For Azure metrics, we typically see: PT1M, PT5M, PT1H, P1D, etc.

            var duration = iso8601Duration.Trim().ToUpperInvariant();

            if (!duration.StartsWith("P"))
                return false;

            try
            {
                var timespan = TimeSpan.Zero;
                var index = 1; // Skip the 'P'
                var inTimeSection = false;

                while (index < duration.Length)
                {
                    if (duration[index] == 'T')
                    {
                        inTimeSection = true;
                        index++;
                        continue;
                    }

                    // Find the number part
                    var numberStart = index;
                    while (index < duration.Length && (char.IsDigit(duration[index]) || duration[index] == '.'))
                    {
                        index++;
                    }

                    if (index == numberStart || index >= duration.Length)
                        return false;

                    var numberStr = duration.Substring(numberStart, index - numberStart);
                    if (!double.TryParse(numberStr, out var number))
                        return false;

                    var unit = duration[index];

                    switch (unit)
                    {
                        case 'D':
                            if (inTimeSection)
                                return false; // Days can't be in time section
                            timespan = timespan.Add(TimeSpan.FromDays(number));
                            break;
                        case 'H':
                            if (!inTimeSection)
                                return false; // Hours must be in time section
                            timespan = timespan.Add(TimeSpan.FromHours(number));
                            break;
                        case 'M':
                            if (inTimeSection)
                            {
                                timespan = timespan.Add(TimeSpan.FromMinutes(number));
                            }
                            else
                            {
                                // Months - approximate as 30 days
                                timespan = timespan.Add(TimeSpan.FromDays(number * 30));
                            }
                            break;
                        case 'S':
                            if (!inTimeSection)
                                return false; // Seconds must be in time section
                            timespan = timespan.Add(TimeSpan.FromSeconds(number));
                            break;
                        case 'Y':
                            if (inTimeSection)
                                return false; // Years can't be in time section
                                              // Years - approximate as 365 days
                            timespan = timespan.Add(TimeSpan.FromDays(number * 365));
                            break;
                        case 'W':
                            if (inTimeSection)
                                return false; // Weeks can't be in time section
                            timespan = timespan.Add(TimeSpan.FromDays(number * 7));
                            break;
                        default:
                            return false;
                    }

                    index++;
                }

                result = timespan;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Converts a TimeSpan to ISO 8601 duration format (e.g., PT1M, PT5M, PT1H, P1D)
        /// </summary>
        public static string FormatTimeSpanAsISO8601(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays >= 1 && timeSpan.TotalDays % 1 == 0)
            {
                return $"P{(int)timeSpan.TotalDays}D";
            }

            if (timeSpan.TotalHours >= 1 && timeSpan.TotalHours % 1 == 0)
            {
                return $"PT{(int)timeSpan.TotalHours}H";
            }

            if (timeSpan.TotalMinutes >= 1 && timeSpan.TotalMinutes % 1 == 0)
            {
                return $"PT{(int)timeSpan.TotalMinutes}M";
            }

            if (timeSpan.TotalSeconds >= 1 && timeSpan.TotalSeconds % 1 == 0)
            {
                return $"PT{(int)timeSpan.TotalSeconds}S";
            }

            // Fallback for sub-second intervals
            return $"PT{timeSpan.TotalSeconds:F3}S";
        }
    }
}
