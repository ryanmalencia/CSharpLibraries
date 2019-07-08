using System;
using System.IO;
using CSharpLibraries.WebAPITools;

namespace CSharpLibraries.Logging
{
    public enum LogLevel
    {
        All = 0,
        Garbage = 1,
        Trace = 2,
        Debug = 3,
        Profile = 4,
        Info = 5,
        Warning = 6,
        Error = 7,
        Critial = 8,
        Silent = 9
    }

    public sealed class LoggerOptions
    {
        public bool OutputToConsole = false;
        public bool OutputToFile = false;
        public bool OutputToWebAPI = false;
        public LogLevel LogLevel = LogLevel.All;
        public string LogFile = Path.Combine(Path.GetTempPath(), "cslogger.log");
        public string WebAPIUrl = "http://localhost/";
    }

    public sealed class Logger
    {
        private static bool m_bInitalized = false;
        private static StreamWriter m_swStreamWriter = null;
        private static LoggerOptions m_loLoggerOptions;
        private static WebAPIClient m_WebAPIClient = null;

        public static Logger Instance { get; } = new Logger();

        private Logger()
        {

        }

        static Logger()
        {

        }

        public bool Init(LoggerOptions loggerOptions)
        {
            if(!m_bInitalized)
            {
                if (loggerOptions.OutputToFile)
                {
                    m_swStreamWriter = new StreamWriter(new FileStream(loggerOptions.LogFile, FileMode.Create));
                }
                if(loggerOptions.OutputToWebAPI)
                {
                    m_WebAPIClient = new WebAPIClient(loggerOptions.WebAPIUrl);
                }
                m_loLoggerOptions = loggerOptions;
                m_bInitalized = true;
                return true;
            }
            return false;
        }

        static string ConvertEnumToString(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.All:
                    return "All";
                case LogLevel.Garbage:
                    return "Garbage";
                case LogLevel.Trace:
                    return "Trace";
                case LogLevel.Profile:
                    return "Profile";
                case LogLevel.Info:
                    return "Info";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Critial:
                    return "Critial";
                case LogLevel.Silent:
                    return "Silent";
            }
            return "";
        }

        private bool OutputToFile
        {
            get
            {
                return m_loLoggerOptions.OutputToFile;
            }
        }

        private bool OutputToConsole
        {
            get
            {
                return m_loLoggerOptions.OutputToConsole;
            }
        }

        private bool OutputToWebAPI
        {
            get
            {
                return m_loLoggerOptions.OutputToWebAPI;
            }
        }

        private LogLevel LogLevel
        {
            get
            {
                return m_loLoggerOptions.LogLevel;
            }
        }

        public void Log(string message, LogLevel logLevel)
        {
            string logStr = "[" + DateTime.Now.ToString() + "] " + ConvertEnumToString(logLevel) + ": " + message;
            if (OutputToFile && m_swStreamWriter != null && logLevel >= LogLevel && LogLevel != LogLevel.Silent)
            {
                m_swStreamWriter.WriteLine(logStr);
                m_swStreamWriter.Flush();
            }
            if (OutputToConsole)
                Console.WriteLine(logStr);
            if(OutputToWebAPI)
                m_WebAPIClient.Post("Log", logStr);
        }
    }
}
