using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFWizard;

namespace Test_WFWizard
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //SelectWorkFlow options = new SelectWorkFlow();
            ApprovalSetting options = new ApprovalSetting();
            options.ShowDialog();
        }
    }
}
