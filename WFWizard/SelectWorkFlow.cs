using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFWizard.WF;
using Microsoft.VisualStudio.TemplateWizard;
using System.IO;

namespace WFWizard
{
    public partial class SelectWorkFlow : Form
    {
        public SelectWorkFlow()
        {
            InitializeComponent();
            AppId = "-1";
            ApprovalHtml = "";
            ReplacementsDictionary = new Dictionary<string, string>();
        }

        private void SelectWorkFlow_Load(object sender, EventArgs e)
        {
            LoadApps();
            tbSearch.TextChanged += new EventHandler(tbSearch_TextChanged);
            btnCreate.DialogResult = DialogResult.OK;

        }

        void tbSearch_TextChanged(object sender, EventArgs e)
        {
            LoadApps();
        }

        private void LoadApps()
        {
            if (string.IsNullOrEmpty(tbSearch.Text))
            {
                dllwfList.DataSource = WF.WF_AppDictManager.GetAppDictToDataTable().ToList<AppInfo>();
            }
            else
            {
                dllwfList.DataSource = WF.WF_AppDictManager.GetAppDictToDataTable().ToList<AppInfo>().Where(x => x.AppName.Contains(tbSearch.Text)).ToList();
            }
            dllwfList.DisplayMember = "AppName";
            if (dllwfList.Items.Count!=0)
            {
                dllwfList.SelectedIndex = 0;
                LoadCBItems();
            }
        }
        public string AppId { get; set; }
        public string ApprovalHtml { get; set; }
        public Dictionary<string, string> ReplacementsDictionary { get; set; }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (dllwfList.Items.Count == 0 || dllwfList.SelectedIndex == -1)
            {
                return;
            }
            try
            {
                AppInfo currentInfo = dllwfList.SelectedValue as AppInfo;
                ReplacementsDictionary.Add("$AppId$", currentInfo.AppId);
                ReplacementsDictionary.Add("$AppName$", currentInfo.AppName);
                ReplacementsDictionary.Add("$WorkFlowName$", currentInfo.WorkFlowName);
                StringBuilder content = new StringBuilder();


               // List<ActInfo> infos = WF.WF_AppDictManager.GetWorkFlowActByAppID(currentInfo.AppId).ToList<ActInfo>();
                List<ActInfo> infos = GetCBItems();


                ///存在会签节点则恢复会签
                if (infos.Exists(x => x.Name == "会签"))
                {
                    ReplacementsDictionary.Add("//Countersign：", "");
                }
                string approvalFormatLine_E = "<tr><th>{0}</th><td colspan='2'></td></tr>";
                foreach (var item in infos)
                {
                    if (item.Name == "会签")
                    {
                        content.AppendLine("<tr><th>相关部门会签： </th><td colspan=\"2\"><CS:Countersign ID=\"Countersign1\" runat=\"server\" IsCanEdit=\"true\"/></td></tr>");
                        content.AppendFormat(approvalFormatLine_E, "相关部门意见");
                        continue;
                    }
                    content.AppendFormat(approvalFormatLine_E, item.Name.Replace("审批", "意见"));
                }
                ReplacementsDictionary.Add("$ApprovalBoxList_E$", content.ToString());

                content.Clear();
                string approvalFormatLine_A = "<tr><th>{2}</th><td colspan='2'><AB:ApprovalBox ID=\"Option_{0}\" Node=\"{1}\"  runat=\"server\" /></td></tr>";
                foreach (var item in infos)
                {
                    if (item.Name == "会签")
                    {
                        content.AppendLine("<tr><th>相关部门会签： </th><td colspan=\"2\"><CS:Countersign ID=\"Countersign1\" runat=\"server\" IsCanEdit=\"true\"/></td></tr>");
                        content.AppendFormat(approvalFormatLine_A, item.Id, item.Name, "相关部门意见");
                        continue;
                    }
                    content.AppendFormat(approvalFormatLine_A, item.Id, item.Name, item.Name.Replace("审批", "意见:"));
                }
                ReplacementsDictionary.Add("$ApprovalBoxList_A$", content.ToString());

                content.Clear();
                string formatLine = "options.Add(Option_{0});\r\n";
                foreach (var item in infos)
                {
                    content.AppendFormat(formatLine, item.Id);
                }
                ReplacementsDictionary.Add("//$CS_ApprovalBoxList$", content.ToString());

                content.Clear();
                formatLine = "dfInfos.Add(new K2_DataFieldInfo(){{DeptCode = \"\",RoleName = \"{0}\",Name = \"\"}});\r\n";
                foreach (var item in infos)
                {
                    if (item.Name == "会签")
                    {
                        content.AppendLine("dfInfos.Add(new K2_DataFieldInfo() { Name = \"CounterSignUsers\", Result = Countersign1.Result });");
                        continue;
                    }
                    content.AppendFormat(formatLine, item.Name.Replace("审批", ""));
                    
                }
                ReplacementsDictionary.Add("//$CS_K2_UserDataFieldCode$", content.ToString());

                List<DFInfo> dfInfos = WF.WF_AppDictManager.GetWorkFlowDFByAppID(currentInfo.AppId).ToList<DFInfo>();
                content.Clear();
                formatLine = "{0},";
                foreach (var item in dfInfos)
                {
                    content.AppendFormat(formatLine, item.Name);
                }
                ReplacementsDictionary.Add("$CS_K2_UserDataField$", content.ToString().Trim(','));
                btnCreate.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                string errorLog = ex.Message + "\r\n" + ex.StackTrace;
                Logger.SaveLog(errorLog);
                MessageBox.Show(errorLog);
                btnCreate.DialogResult = DialogResult.Cancel;
                //throw new WizardBackoutException();
            }
        }

        private void dllwfList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCBItems();
        }



        private void LoadCBItems()
        {
            AppInfo currentInfo = dllwfList.SelectedValue as AppInfo;
            List<ActInfo> infos = WF.WF_AppDictManager.GetWorkFlowActByAppID(currentInfo.AppId).ToList<ActInfo>();
            clbItems.Items.Clear();
            foreach (var item in infos)
            {
                clbItems.Items.Add(item);
            }
        }

        private List<ActInfo> GetCBItems()
        {
            List<ActInfo> infos = new List<ActInfo>();
            foreach (var item in clbItems.Items)
            {
                infos.Add(item as ActInfo);
            }
            return infos;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (clbItems.SelectedIndex==0)
            {
                MessageBox.Show("已经到最上！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ActInfo currentInfo = clbItems.Items[clbItems.SelectedIndex] as ActInfo;
            clbItems.Items[clbItems.SelectedIndex] = clbItems.Items[clbItems.SelectedIndex - 1];
            clbItems.Items[clbItems.SelectedIndex - 1] = currentInfo;
            clbItems.SelectedIndex = clbItems.SelectedIndex - 1;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (clbItems.SelectedIndex>=clbItems.Items.Count-1)
            {
                MessageBox.Show("已经到最下！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ActInfo currentInfo = clbItems.Items[clbItems.SelectedIndex] as ActInfo;
            clbItems.Items[clbItems.SelectedIndex] = clbItems.Items[clbItems.SelectedIndex + 1];
            clbItems.Items[clbItems.SelectedIndex + 1] = currentInfo;
            clbItems.SelectedIndex = clbItems.SelectedIndex +1;
        }
    }
}
