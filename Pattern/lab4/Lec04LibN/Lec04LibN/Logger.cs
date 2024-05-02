namespace Lec04LibN
{
    public partial class Logger : ILogger
    {

        private static Logger? _instance;
        private static readonly object _lock = new object();

        private string _logFileName = $"{Directory.GetCurrentDirectory()}/LOG{DateTime.Now.ToString("yyyyMMdd-HH-mm-ss")}.txt";

        private int _countLog = 0;

        private string _title = "";
        private Logger()
        {
            this.log("INIT");
        }
        public void start(string title)
        {
            _title += title + ":";
            this.log("STRT");
        }
        public void stop()
        {
            _title = _title.Remove(_title.Length - 2, 2);
            this.log("STOP");
        }
        public static ILogger create()
        {
            if (_instance == null)
                lock (_lock)
                    if (_instance == null) _instance = new Logger();

            return _instance;
        }
        public void log(string message)
        {
            _countLog++;

            if (message == "STRT" || message == "STOP" || message == "INIT")
            {
                using (var sw = new StreamWriter(_logFileName, true))
                {
                    sw.WriteLine($"{_countLog.ToString().PadLeft(6, '0')}-" +
                        $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}-" +
                        $"{message} {_title}");
                }
            }
            else
            {
                using (var sw = new StreamWriter(_logFileName, true))
                {
                    sw.WriteLine($"{_countLog.ToString().PadLeft(6, '0')}-" +
                        $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}-" +
                        $"INFO {_title} {message}");

                }
            }
        }
    }
}

