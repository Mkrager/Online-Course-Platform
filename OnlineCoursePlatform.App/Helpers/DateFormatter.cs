using System.Globalization;

namespace OnlineCoursePlatform.App.Helpers
{
    public static class DateFormatter
    {
        public static string ToShortDate(DateTime date)
        {
            return date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
        }
    }
}
