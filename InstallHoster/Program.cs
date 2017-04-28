using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace InstallHoster
{
    class Program
    {
        public static void GoToErrorPage(string msg, Exception ex = null)
        {
            if (ex == null)
            {
                ex = new Exception(msg);
            }
        }
        static void Main(string[] args)
        {
            if (args != null )
            {
                args.ToList().ForEach(x => File.AppendAllText("C:\\321.txt", x));
                string physicalRoot = @"C:\Program Files (x86)\Microsoft\PlugInSetup\";  //args[0];
                var lines = new string[] {
                 "\"$basePath$gac\\gacutil.exe\" -u WFWizard" 
                 ,string.Format("\"$basePath$gac\\gacutil.exe\" -i \"$basePath$gac\\{0}\"","WFWizard.dll")
                 //,string.Format("$basePath$VSIXInstaller.exe /uninstall:{0}","VSIX_BPM.Microsoft.e98fd1c1-7c7a-4746-9647-3608f768cb7a")
                // ,string.Format("\"$basePath$VSIXInstaller.exe\" \"$basePath${0}\"","VSIX_BPM.vsix")
            };
                Hoster hoster = new Hoster(physicalRoot);
                hoster.CommandLines = lines.ToList();
                Console.WriteLine(hoster.DoWork());
            }


        }
    }
}
