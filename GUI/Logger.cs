using System;
using System.IO;
using System.Threading.Tasks;

namespace GUI
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app_log.txt");

        public static void Log(string message)
        {
            try
            {
                var text = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [Thread:{System.Threading.Thread.CurrentThread.ManagedThreadId}] {message}{Environment.NewLine}";
                lock (_lock)
                {
                    File.AppendAllText(LogFile, text);
                }
            }
            catch { }
        }

        public static Task LogAsync(string message) => Task.Run(() => Log(message));
    }
}
