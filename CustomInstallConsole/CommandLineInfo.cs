using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CustomInstallConsole
{
    public class CommandLineInfo
    {
        public string ExeFile { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }

        public CommandLineInfo()
        {

        }
        public ProcessStartInfo StartInfo
        {
            get
            {
                return
                    new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        FileName = ExeFile,
                        Arguments = Arguments,
                        WorkingDirectory = WorkingDirectory,
                        UseShellExecute = true,
                        RedirectStandardInput = false,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false
                    };
            }
        }

    }
}
