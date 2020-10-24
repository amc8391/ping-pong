namespace pingpong.Utils
{
    public interface ILogger
    {
        void log(string s);
        void error(string s);
        void warn(string s);
        void info(string s);
        void debug(string s);
    }
}