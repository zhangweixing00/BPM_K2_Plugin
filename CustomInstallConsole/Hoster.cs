using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CustomInstallConsole
{
    public class Hoster
    {
        public StringBuilder ExecResult { get; set; }
        public List<CommandLineInfo> CommandLines { get; set; }
        //public string WorkingDirectory { get; set; }
        public Hoster()
        {
            CommandLines = new List<CommandLineInfo>();
            ExecResult = new StringBuilder();
        }

        public string DoWork()
        {
            foreach (CommandLineInfo item in CommandLines)
            {
                var process = Process.Start(item.StartInfo);
                process.WaitForExit();
                ExecResult.AppendLine(process.ExitCode.ToString());
                Console.WriteLine(process.ExitCode);
                process.Close();
                process.Dispose();
            }
            return ExecResult.ToString();
        }

        public static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _CodeBase = _CodeBase.Substring(8, _CodeBase.LastIndexOf("/") - 8 + 1); // 8是 "file://" 的长度
            return _CodeBase.Replace("/", @"\");
        }
    }
}
