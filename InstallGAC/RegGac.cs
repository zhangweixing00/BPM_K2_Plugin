using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InstallWFWizard
{
    public class RegGac
    {
        public static void InstallWFWizard()
        {
            string basePath = RegGac.GetAssemblyPath();
            string gacUtilPath = string.Format("{0}gacutil.exe", basePath);
            string dllPath = basePath+"WFWizard.dll";

            var process= Process.Start(new ProcessStartInfo()
            {
                CreateNoWindow = false,
                //FileName = gacUtilPath,
                //Arguments = " -u WFWizard"
                FileName = "cmd.exe ",
                //Arguments = gacUtilPath + "\n pause",
                UseShellExecute = false,

                RedirectStandardInput = true,

                RedirectStandardOutput = false,

                //RedirectStandardError = true

            });


            var lines = new string[] {

                gacUtilPath+" -u WFWizard"

              ,string.Format("{0} -i {1}",gacUtilPath,dllPath)
            };



            foreach (var cmdLine in lines)
            {

                process.StandardInput.WriteLine(cmdLine);

                process.StandardInput.Flush();



                // Skip the echo characters

                //process.StandardOutput.ReadLine();

                //var result = process.StandardOutput.ReadLine();

                //if (result != string.Format("Hello: {0}", cmdLine))

                //    Console.WriteLine(result);

            }
        }

        public static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _CodeBase = _CodeBase.Substring(8, _CodeBase.LastIndexOf("/") - 8 + 1); // 8是 "file://" 的长度
            return _CodeBase.Replace("/", @"\");
        }
    }
}
