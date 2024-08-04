namespace AspireSmallFinance.Utilities
{
    public static class DateUtils
    {
        private static readonly string ISO_DATE_FORMAT = "yyyy-MM-dd";
        public static List<DateTime> GetWeeklyBucketsForDateRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> buckets = new List<DateTime>();
            DateTime currentDate = startDate.AddDays(7);

            if (currentDate >= endDate)
            {
                buckets.Add(currentDate);
                return buckets;
            }

            while (currentDate < endDate) {
                buckets.Add(currentDate);
                currentDate = currentDate.AddDays(7);

                if(currentDate >= endDate)
                {
                    buckets.Add(endDate);
                    break;
                }
            }
            return buckets;

        }

        public static string GetISODateString(this DateTime dateTime)
        {
            return dateTime.ToString(ISO_DATE_FORMAT);
        }
    }
}
