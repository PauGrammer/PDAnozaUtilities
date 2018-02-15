using System;

using System.IO;
namespace PDAnozaUtilities
{
    public class ErrorHandler
    {
        public int ErrorID { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string Source { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }

        public ErrorHandler()
        {
            IsSuccessful = true;
        }

        public void SetErrorDetails(string source, string message, string stacktrace)
        {
            IsSuccessful = false;
            Source = source;
            Message = message;
            StackTrace = stacktrace;
        }

        public void LogToNotepad(string logFileName)
        {

            using (var streamWriter = new StreamWriter(logFileName, true))
            {
                streamWriter.WriteLine("\n===========================================================================================================================");
                streamWriter.WriteLine(String.Format("{0} -->\tSource: {1}", DateTime.Now.ToString(), this.Source));
                streamWriter.WriteLine(String.Format("{0} -->\tMessage: {1}", DateTime.Now.ToString(), this.Message));
                streamWriter.WriteLine(String.Format("{0} -->\tStack Trace: {1}", DateTime.Now.ToString(), this.StackTrace));
                streamWriter.WriteLine("============================================================================================================================");
            }
        }
    }
}
