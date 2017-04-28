using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace CustomInstallConsole
{
    [RunInstaller(true)]
    public partial class CustomInstallercs : System.Configuration.Install.Installer
    {
        public CustomInstallercs()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            var lines = new string[] {
                 "$basePath$gacutil.exe -u WFWizard" 
                 ,string.Format("$basePath$gacutil.exe -i $basePath${0}","WFWizard.dll")
                 //,string.Format("$basePath$VSIXInstaller.exe /uninstall:{0}","VSIX_BPM.Microsoft.e98fd1c1-7c7a-4746-9647-3608f768cb7a")
                 ,string.Format("$basePath$VSIXInstaller.exe $basePath${0}","VSIX_BPM.vsix")
            };

            string physicalRoot = this.Context.Parameters["targetdir"];
            Dictionary<string, string> CommandLines = new Dictionary<string, string>();
            CommandLines.Add(string.Format(@"{0}\gac\gacutil.exe", physicalRoot), string.Format(@"{0}\gac\WFWizard.dll", physicalRoot));
            Hoster hoster = new Hoster();
            hoster.CommandLines = CommandLines;
            hoster.DoWork();
            //Console.WriteLine(hoster.DoWork());

            base.Install(stateSaver);
        }
    }

}
