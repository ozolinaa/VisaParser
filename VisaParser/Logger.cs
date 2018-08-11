using System;
using System.Collections.Generic;

namespace VisaParser
{
    public class Logger
    {
        private int _utcHourToSendLog;
        private List<string> _logItems;
        private DateTime _lastSentLogUTCTime;
        private IEnumerable<string> _emails;

        public Logger(int utcHourToSendLog, IEnumerable<string> emails)
        {
            _utcHourToSendLog = utcHourToSendLog;
            _logItems = new List<string>();
            _lastSentLogUTCTime = DateTime.MinValue;
            _emails = emails;
        }

        public void Log(string logItem)
        {
            Console.WriteLine(logItem);
            _logItems.Add(logItem);
        }

        public bool IsNowTimeToSendLog()
        {
            DateTime now = DateTime.UtcNow;
            return (now.Hour == _utcHourToSendLog && (now - _lastSentLogUTCTime).Hours > 1);
        }

        public void SendLogedItems()
        {
            string logMsg = string.Join("<br />", _logItems);
            _logItems = new List<string>();
            using (Notifier notifier = new Notifier())
            {
                foreach (string email in _emails)
                {
                    notifier.SendEmail(email, "MSK VISA LOG", logMsg);
                }
            }
            _lastSentLogUTCTime = DateTime.UtcNow;
        }
    }
}
