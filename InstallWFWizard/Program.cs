using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstallWFWizard
{
    class Program
    {
        static void Main(string[] args)
        {
            //new FusionUtil().Install(@"F:\Work\Work\WFWizard\WFWizard\WFWizard\bin\Debug\WFWizard.dll");
             RegGac.InstallWFWizard();
        }
    }
}
