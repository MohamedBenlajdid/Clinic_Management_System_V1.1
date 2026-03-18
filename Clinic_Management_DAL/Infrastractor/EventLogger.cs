using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;



namespace Clinic_Management_DAL.Infrastractor
{

    using System;
    using System.Diagnostics;
    using System.IO;

    public static class EventLogger
    {
        private const string EventSource = "AtlasClinic";
        private const string EventLogName = "Application";

        // Fallback log file (adjust as needed)
        private static readonly string FallbackLogFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AtlasClinic",
            "logs.txt");

        static EventLogger()
        {
            // Ensure fallback directory exists
            try
            {
                var fallbackDir = Path.GetDirectoryName(FallbackLogFilePath);
                if (!Directory.Exists(fallbackDir))
                    Directory.CreateDirectory(fallbackDir);
            }
            catch
            {
                // Swallow — fallback logging may fail silently
            }
        }

        /// <summary>
        /// Logs an Exception with contextual information to Windows Event Log.
        /// Falls back to file logging if Event Log write fails.
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="context">Context info about where/how the error happened</param>
        public static void Log(Exception ex, string context)
        {
            string message = FormatMessage(ex, context);

            try
            {
                if (!EventLog.SourceExists(EventSource))
                {
                    // Do NOT create source here in production.
                    // Just throw so fallback works.
                    throw new InvalidOperationException($"Event Source '{EventSource}' does not exist.");
                }

                EventLog.WriteEntry(
                    EventSource,
                    message,
                    EventLogEntryType.Error);
            }
            catch
            {
                // Fallback to file logging if event log fails
                try
                {
                    File.AppendAllText(FallbackLogFilePath,
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR\n{message}\n\n");
                }
                catch
                {
                    // Swallow any exceptions to avoid crashing the app from logging errors
                }
            }
        }

        private static string FormatMessage(Exception ex, string context)
        {
            return
                $"Context:\n{context}\n\n" +
                $"Exception Type: {ex.GetType().FullName}\n" +
                $"Message: {ex.Message}\n" +
                $"StackTrace:\n{ex.StackTrace}";
        }
    }




}
