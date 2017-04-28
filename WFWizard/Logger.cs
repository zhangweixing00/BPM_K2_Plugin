using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WFWizard
{
    public class Logger
    {
        public static void SaveLog(string errorLog)
        {
            errorLog = string.Format("{0}:{1}\r\n", DateTime.Now.ToString(), errorLog);
            File.AppendAllText("C:\\log_bpm.txt", errorLog);
        }
    }
}
