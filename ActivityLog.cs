using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class ActivityLog
    {
        private List<string> logEntries = new List<string>();
        private int maxDisplay = 10;

        public void AddEntry(string action)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            logEntries.Add($"[{timestamp}] {action}");
        }

        public List<string> GetLastEntries(int count)
        {
            if (count > logEntries.Count)
                count = logEntries.Count;

            if (count <= 0)
                return new List<string>();

            return logEntries.GetRange(logEntries.Count - count, count);
        }

        public List<string> GetAllEntries()
        {
            return logEntries;
        }

        public int GetEntryCount()
        {
            return logEntries.Count;
        }

        public void ClearLog()
        {
            logEntries.Clear();
        }
    }
}