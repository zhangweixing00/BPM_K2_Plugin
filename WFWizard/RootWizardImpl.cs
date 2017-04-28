using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;

namespace WFWizard
{
    public class RootWizardImpl : IWizard
    {
        //private string safeprojectname;
        //private static Dictionary<string, string> globalParameters = new Dictionary<string, string>();

        //public static IEnumerable<KeyValuePair<string, string>> GlobalParameters
        //{
        //    get { return globalParameters; }
        //}
        public bool IsAddSuccess { get; set; }

        public RootWizardImpl()
        {
            IsAddSuccess = true;
        }

        #region IWizard Members

        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem) { }

        public void ProjectFinishedGenerating(EnvDTE.Project project) { }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem) { }

        public void RunFinished() { }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            try
            {
                //dteObject = (automationObject as EnvDTE80.DTE2);
                //safeprojectname = replacementsDictionary["$safeprojectname$"];
                //globalParameters["$safeprojectname$"] = safeprojectname;
                ApprovalSetting options = new ApprovalSetting();
                if (options.ShowDialog() == DialogResult.OK)
                {
                    Logger.SaveLog("Before:"+replacementsDictionary.Count);
                    foreach (KeyValuePair<string, string> item in options.ReplacementsDictionary)
                    {
                        replacementsDictionary.Add(item.Key, item.Value);
                        Logger.SaveLog(string.Format("{0}--{1}",item.Key, item.Value));
                    }
                    Logger.SaveLog("After:" + replacementsDictionary.Count);
                }
                else
                {
                    IsAddSuccess = false;
                    //throw new WizardBackoutException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //throw new WizardBackoutException();
                IsAddSuccess = false;
            }
        }

        public bool ShouldAddProjectItem(string filePath) { return  this.IsAddSuccess; }

        #endregion
    }
}
