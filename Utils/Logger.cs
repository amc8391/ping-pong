namespace pingpong.Utils
{
    public class Logger : ILogger
    {
        private IDateUtils _dateUtils;
        private string defaultLogLevel = "INFO";
        
        public Logger (IDateUtils dateUtils) {
            _dateUtils = dateUtils;
        }

        private void loggerHelper(string level, string s)
        {
            string date = _dateUtils.toIsoDateSting(System.DateTime.UtcNow);
            System.Console.WriteLine(date + " - " + level + ": " + s);
        }

        public void log(string s)
        {
            loggerHelper(defaultLogLevel, s);
        }

        public void error(string s)
        {
            loggerHelper("ERROR", s);
        }

        public void warn(string s)
        {
            loggerHelper("WARN", s);
        }
        public void info(string s)
        {
            loggerHelper("INFO", s);
        }
        public void debug(string s)
        {
            loggerHelper("WARN", s);
        }
    }
}