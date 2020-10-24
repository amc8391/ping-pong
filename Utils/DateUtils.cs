namespace pingpong.Utils
{
    public class DateUtils : IDateUtils
    {
        private string isoDateFormat = "yyyy-MM-dd'T'HH:mm:ss.fffK";

        public string toIsoDateSting(System.DateTime dt) {
            return dt.ToString(isoDateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}