using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CustomInstallConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Hoster hoster = new Hoster();
            string physicalRoot = Path.GetDirectoryName(Hoster.GetAssemblyPath().Trim('\\'));
            File.AppendAllText("C:\\321.txt", physicalRoot);

            string gacPath = Path.Combine(physicalRoot,Environment.Is64BitOperatingSystem?"gacX64":"gac");
            string templatePath = Path.Combine(physicalRoot, "template");
            List<CommandLineInfo> infos = null;

            if (args != null && args.Length > 0 && args[0] == "-u")
            {
                infos = new List<CommandLineInfo>()
                {
                    new CommandLineInfo()
                    {
                         ExeFile="gacutil.exe",
                         Arguments="-u WFWizard",
                         WorkingDirectory=gacPath
                    },
                    new CommandLineInfo()
                    {
                         ExeFile="VSIXInstaller.exe",
                         Arguments=" /uninstall:VSIX_BPM.Microsoft.e98fd1c1-7c7a-4746-9647-3608f768cb7a",
                         WorkingDirectory=templatePath
                    }
                };
            }
            else
            {
                infos = new List<CommandLineInfo>()
                {
                    new CommandLineInfo()
                    {
                         ExeFile="gacutil.exe",
                         Arguments="-u WFWizard",
                         WorkingDirectory=gacPath
                    },
                    new CommandLineInfo()
                    {
                         ExeFile="gacutil.exe",
                         Arguments="-i WFWizard.dll",
                         WorkingDirectory=gacPath
                    },
                    new CommandLineInfo()
                    {
                         ExeFile="VSIXInstaller.exe",
                         Arguments=" /uninstall:VSIX_BPM.Microsoft.e98fd1c1-7c7a-4746-9647-3608f768cb7a",
                         WorkingDirectory=templatePath
                    },
                    new CommandLineInfo()
                    {
                         ExeFile="VSIXInstaller.exe",
                         Arguments=" VSIX_BPM.vsix",
                         WorkingDirectory=templatePath
                    }
                };
            }
            hoster.CommandLines = infos;
            string result = hoster.DoWork();
            File.AppendAllText("C:\\321.txt", result);
        }
    }
}
