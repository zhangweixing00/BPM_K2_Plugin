using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TemplateWizard;
using InstallWFWizard;

namespace WFWizard
{
    public class RootWizardImpl : IWizard
    {
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
                RegGac.InstallWFWizard();
            }
            catch (Exception ex)
            {

            }
        }

        public bool ShouldAddProjectItem(string filePath) { return true; }

        #endregion
    }
}
