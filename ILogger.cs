using System.Collections.Generic;
namespace Trivia
{
    public interface ILogService
    {
        void Log(string action);
        string GetLog();
    }

    class Logger : ILogService
    {
        List<string> history = new List<string>();
        string[] GetGameHistory() => this.history.ToArray();

        public void Log(string action) =>history.Add(action);
        public string GetLog() => string.Join("\r\n", GetGameHistory()) + "\r\n";

    }
}