using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InstallHoster
{
    public class Hoster
    {
        private string physicalRoot;

        public List<string> CommandLines { get; set; }
        public StringBuilder ExecResult { get; set; }

        public Hoster()
        {
            CommandLines = new List<string>();
            ExecResult = new StringBuilder();
        }

        public Hoster(string physicalRoot)
        {
            // TODO: Complete member initialization
            this.physicalRoot = physicalRoot;
        }

        public string DoWork()
        {
           // string basePath = GetAssemblyPath();
            // CommandLines.ForEach(x => x=x.Replace("$basePath$", basePath));
            for (int index = 0; index < CommandLines.Count; index++)
            {
                CommandLines[index] = CommandLines[index].Replace("$basePath$", physicalRoot);
            }

            var process = Process.Start(new ProcessStartInfo()
            {
                CreateNoWindow = true,
                FileName = "cmd.exe ",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = false,
                RedirectStandardError = false
            });
            //process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
            //process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            //process.BeginOutputReadLine();
            //process.BeginErrorReadLine();
            foreach (var cmdLine in CommandLines)
            {
                process.StandardInput.WriteLine(cmdLine);
                process.StandardInput.Flush();
            }
            //int code = process.ExitCode;
            process.WaitForExit();
            process.Close();
            //if (process.ExitCode != 0)
            //{
            //    ExecResult.Append(process.StandardError.ReadToEnd());
            //    process.Close();
            //    return ExecResult.ToString(); 
                
            //}    
            //process.WaitForExit();
            return ""; //ExecResult.ToString();
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            ExecResult.AppendLine(e.Data);
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            ExecResult.AppendLine(e.Data);
        }


        public static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _CodeBase = _CodeBase.Substring(8, _CodeBase.LastIndexOf("/") - 8 + 1); // 8是 "file://" 的长度
            return _CodeBase.Replace("/", @"\");
        }
    }
}
