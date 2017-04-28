using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFWizard.WF;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace WFWizard
{
    public partial class ApprovalSetting : Form
    {
        public ApprovalSetting()
        {
            InitializeComponent();
            AppId = "-1";
            ApprovalHtml = "";
            ReplacementsDictionary = new Dictionary<string, string>();
            CurrentBizTableColumns = new List<BizTableColumn>();
        }

        [Serializable]
        [XmlInclude(typeof(ActInfo))]
        public class ApprovalInfo
        {
            public string Name { get; set; }
            public List<ActInfo> ActInfos { get; set; }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dllwfList.Items.Count == 0 || dllwfList.SelectedIndex == -1)
            {
                return;
            }
            try
            {
                Dictionary<string, string> formDictionary = GetFormDictionary();
                foreach (var item in formDictionary)
                {
                    ReplacementsDictionary.Add(item.Key, item.Value);
                }

                btnSave.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                string errorLog = ex.Message + "\r\n" + ex.StackTrace;
                Logger.SaveLog(errorLog);
                MessageBox.Show(errorLog);
                btnSave.DialogResult = DialogResult.Cancel;
                //throw new WizardBackoutException();
            }
        }

        private Dictionary<string, string> GetFormDictionary()
        {
            Dictionary<string, string> formDictionary = new Dictionary<string, string>();
            AppInfo currentInfo = dllwfList.SelectedValue as AppInfo;
            formDictionary.Add("$AppId$", currentInfo.AppId);
            formDictionary.Add("$AppName$", currentInfo.AppName);
            formDictionary.Add("$WorkFlowName$", currentInfo.WorkFlowName);
            StringBuilder content = new StringBuilder();


            // List<ActInfo> infos = WF.WF_AppDictManager.GetWorkFlowActByAppID(currentInfo.AppId).ToList<ActInfo>();
            List<ApprovalInfo> infos = GetApprovalInfos();
            Save(infos);

            ///存在会签节点则恢复会签
            //if (infos.Exists(x => x.Name == "会签"))
            //{
            //    ReplacementsDictionary.Add("//Countersign：", "");
            //}

            string approvalFormatLine_E = "<tr><th>{0}</th><td colspan='2'></td></tr>";
            foreach (var item in infos)
            {
                if (item.ActInfos.Exists(x => x.Name == "会签"))
                {
                    content.AppendLine("<tr><th>相关部门会签： </th><td colspan=\"2\"><CS:Countersign ID=\"Countersign1\" runat=\"server\" IsCanEdit=\"true\"/></td></tr>");
                    content.AppendFormat(approvalFormatLine_E, "相关部门意见");
                }
                else if (item.ActInfos.Exists(x => x.Name == "集团会签"))
                {
                    content.AppendLine("<tr><th>集团相关部门会签： </th><td colspan=\"2\"><CSG:Countersign_Group ID=\"Countersign_Group1\" runat=\"server\" IsCanEdit=\"true\"/></td></tr>");
                    content.AppendFormat(approvalFormatLine_E, "集团相关部门意见");
                }
                else
                {
                    content.AppendFormat("<tr><th>{0}： </th><td colspan=\"2\"></td></tr>", item.Name.Replace("审批", "意见"));
                }
                //content.AppendFormat(approvalFormatLine_E, item.Name.Replace("审批", "意见"));
            }

            formDictionary.Add("$ApprovalBoxList_E$", content.ToString());


            content.Clear();
            string approvalFormatLine_A = "<AB:ApprovalBox ID=\"Option_{0}\" Node=\"{1}\"  runat=\"server\" />";
            foreach (var item in infos)
            {

                if (item.ActInfos.Exists(x => x.Name == "会签"))
                {
                    content.AppendLine("<tr><th>相关部门会签： </th><td colspan=\"2\"><CS:Countersign ID=\"Countersign1\" runat=\"server\" IsCanEdit=\"true\"/></td></tr>");
                    item.Name = "相关部门意见";
                }
                else if (item.ActInfos.Exists(x => x.Name == "集团会签"))
                {
                    content.AppendLine("<tr><th>集团相关部门会签： </th><td colspan=\"2\"><CSG:Countersign_Group ID=\"Countersign_Group1\" runat=\"server\" IsCanEdit=\"true\"/></td></tr>");
                    item.Name = "相关部门意见";
                }

                content.AppendFormat("<tr><th>{0}：</th><td colspan='2'>", item.Name.Replace("审批", "意见"));

                foreach (var subItem in item.ActInfos)
                {
                    //if (subItem.Name == "会签")
                    //{
                    //    content.AppendLine("<CS:Countersign ID=\"Countersign1\" runat=\"server\" IsCanEdit=\"true\"/>");
                    //}
                    //else if (subItem.Name == "集团会签")
                    //{
                    //    content.AppendLine("<CSG:Countersign_Group ID=\"Countersign_Group1\" runat=\"server\" IsCanEdit=\"true\"/>");
                    //}
                    //else
                    //{
                    content.AppendFormat(approvalFormatLine_A, subItem.Id, subItem.Name);
                    //}
                }

                content.Append("</td></tr>");

                //content.AppendFormat(approvalFormatLine_A, item.Id, item.Name, item.Name.Replace("审批", "意见:"));
            }
            formDictionary.Add("$ApprovalBoxList_A$", content.ToString());

            //content.Clear();
            //string formatLine = "options.Add(Option_{0});\r\n";
            //foreach (var item in infos)
            //{
            //    content.AppendFormat(formatLine, item.Id);
            //}
            //ReplacementsDictionary.Add("//$CS_ApprovalBoxList$", content.ToString());

            content.Clear();
            string formatLine = "dfInfos.Add(new K2_DataFieldInfo(){{DeptCode = \"\",RoleName = \"{0}\",Name = \"\"}});\r\n";
            foreach (var item in infos)
            {

                foreach (var subItem in item.ActInfos)
                {
                    if (subItem.Name == "会签")
                    {
                        content.AppendLine("dfInfos.Add(new K2_DataFieldInfo() { Name = \"CounterSignUsers\", Result = Countersign1.Result });");
                        continue;
                    }
                    content.AppendFormat(formatLine, subItem.Name.Replace("审批", "").Replace("意见", "").Replace("集团", ""));

                }

            }
            formDictionary.Add("//$CS_K2_UserDataFieldCode$", content.ToString());

            List<DFInfo> dfInfos = WF.WF_AppDictManager.GetWorkFlowDFByAppID(currentInfo.AppId).ToList<DFInfo>();
            content.Clear();
            formatLine = "{0},";
            foreach (var item in dfInfos)
            {
                content.AppendFormat(formatLine, item.Name);
            }
            formDictionary.Add("$CS_K2_UserDataField$", content.ToString().Trim(','));

            //*****DB处理
            var linqTableName = currentInfo.WorkFlowName.Replace("K2Workflow\\", "");
            formDictionary.Add("$TableName$", linqTableName);

            //$TableToForm_Code$
            content.Clear();
            formatLine = @"
            tb_{0}.Text=info.{0};";
            foreach (var item in CurrentBizTableColumns)
            {
                if (!item.IsSelected)
                {
                    continue;
                }
                content.AppendFormat(formatLine, item.Name);
            }
            formDictionary.Add("$TableToForm_Code$", content.ToString().Trim(','));

            //$FormToTable_Code_0$
            content.Clear();
            formatLine = @"
            info.{0}=tb_{0}.Text,";
            foreach (var item in CurrentBizTableColumns)
            {
                if (!item.IsSelected)
                {
                    continue;
                }
                content.AppendFormat(formatLine, item.Name);
            }
            formDictionary.Add("$FormToTable_Code_0$", content.ToString().Trim(','));

            //$FormToTable_Code_1$
            content.Clear();
            formatLine = @"
            info.{0}=tb_{0}.Text;";
            foreach (var item in CurrentBizTableColumns)
            {
                if (!item.IsSelected)
                {
                    continue;
                }
                content.AppendFormat(formatLine, item.Name);
            }
            formDictionary.Add("$FormToTable_Code_1$", content.ToString().Trim(','));

            //$FormToTable_Form$
            content.Clear();
            // content.Append("<tr>");
            formatLine = "<th>{0}:</th><td> <asp:TextBox ID=\"tb_{1}\" MaxLength=\"80\" runat=\"server\" CssClass=\"txt\" /></td>";
            int iLine = 0;
            foreach (var item in CurrentBizTableColumns)
            {
                if (!item.IsSelected)
                {
                    continue;
                }
                if (iLine % 2 == 0)
                {
                    content.Append("<tr>");
                }
                content.AppendFormat(formatLine, item.Des, item.Name);
                iLine++;
                if (iLine % 2 == 0)
                {
                    content.Append("</tr>");
                }
            }
            // content.Append("</tr>");
            formDictionary.Add("<!--$FormToTable_Form$-->", content.ToString().Trim(','));

            string erp1Value = "";
            if (currentInfo.WorkFlowName.Contains("ERP_"))
            {
                erp1Value = @"<iframe id='iframe' style='width: 1050px; height: 700px; border: none;' src='<%= IFrameHelper.GetErpUrl() %>'>
                            </iframe>";

            }

            formDictionary.Add("<!--$FormToTable_Form_ERP1$-->", erp1Value);
            return formDictionary;
        }

        protected void Save(List<ApprovalInfo> infos)
        {
            string xmlData = "";
            XmlSerializer serializer = new XmlSerializer(typeof(List<ApprovalInfo>));

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, infos);
                xmlData = sw.ToString();
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(Path.Combine(path, (dllwfList.SelectedItem as WFWizard.WF.AppInfo).AppName + ".bpi"), xmlData);
        }

        string path = "C:\\BPM_PlugIn";

        protected List<ApprovalInfo> LoadApprovalInfos()
        {
            List<ApprovalInfo> infos = new List<ApprovalInfo>();
            string filePath = Path.Combine(path, (dllwfList.SelectedItem as WFWizard.WF.AppInfo).AppName + ".bpi");
            if (File.Exists(filePath))
            {
                string xmlData = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(xmlData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<ApprovalInfo>));
                    using (StringReader sr = new StringReader(xmlData))
                    {
                        infos.AddRange(serializer.Deserialize(sr) as List<ApprovalInfo>);
                    }
                }
            }

            AppInfo currentInfo = dllwfList.SelectedValue as AppInfo;
            List<ActInfo> dbInfos = WF.WF_AppDictManager.GetWorkFlowActByAppID(currentInfo.AppId).ToList<ActInfo>();
            foreach (var dbItem in dbInfos)
            {
                if (!infos.Exists(x => x.ActInfos.Exists(y => y.Name == dbItem.Name)))
                {
                    infos.Add(new ApprovalInfo()
                    {
                        Name = dbItem.Name,
                        ActInfos = new List<ActInfo>() { dbItem }
                    });
                }
            }

            return infos;
        }

        private List<ApprovalInfo> GetApprovalInfos()
        {
            List<ApprovalInfo> infos = new List<ApprovalInfo>();
            foreach (DataGridViewRow item in dvList.Rows)
            {
                infos.Add(new ApprovalInfo()
                {
                    Name = item.Cells[1].Value.ToString(),
                    ActInfos = item.Cells[2].Tag as List<ActInfo>
                });
            }

            return infos;
        }
        private void ApprovalSetting_Load(object sender, EventArgs e)
        {
            LoadApps();
            tbSearch.TextChanged += new EventHandler(tbSearch_TextChanged);
            btnSave.DialogResult = DialogResult.OK;
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
                dllwfList.DataSource = WF.WF_AppDictManager.GetAppDictToDataTable().ToList<AppInfo>().Where(x => x.AppName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            dllwfList.DisplayMember = "AppName";
            if (dllwfList.Items.Count != 0)
            {
                dllwfList.SelectedIndex = 0;
                LoadWorkItemData();
                LoadDataBase();
            }
        }

        private void LoadWorkItemData()
        {
            List<ApprovalInfo> infos = LoadApprovalInfos();
            if (infos.Count == 0)
            {
                return;
            }

            dvList.Rows.Clear();
            dvList.Rows.Add(infos.Count);

            for (int index = 0; index < infos.Count; index++)
            {
                dvList.Rows[index].Cells[1].Value = infos[index].Name;
                //dvList.Rows[index].Cells[2].Value = infos[index].Name;
                StringBuilder actNames = new StringBuilder();
                foreach (var item in infos[index].ActInfos)
                {
                    actNames.AppendFormat("{0},", item.Name);
                }
                dvList.Rows[index].Cells[2].Value = actNames.ToString().Trim(',');
                dvList.Rows[index].Cells[2].Tag = infos[index].ActInfos;
            }
        }

        private void LoadData()
        {
            AppInfo currentInfo = dllwfList.SelectedValue as AppInfo;
            List<ActInfo> infos = WF.WF_AppDictManager.GetWorkFlowActByAppID(currentInfo.AppId).ToList<ActInfo>();
            if (infos.Count == 0)
            {
                return;
            }

            dvList.Rows.Clear();
            dvList.Rows.Add(infos.Count);
            for (int index = 0; index < infos.Count; index++)
            {
                dvList.Rows[index].Cells[1].Value = infos[index].Name;
                dvList.Rows[index].Cells[2].Value = infos[index].Name;

                dvList.Rows[index].Cells[2].Tag = new List<ActInfo> { infos[index] };
            }

        }
        public string AppId { get; set; }
        public string ApprovalHtml { get; set; }
        public Dictionary<string, string> ReplacementsDictionary { get; set; }
        public Dictionary<string, string> FormDictionary { get; set; }

        private void dllwfList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWorkItemData();
            LoadDataBase();
        }

        public List<BizTableColumn> CurrentBizTableColumns { get; set; }

        private void LoadDataBase()
        {
            AppInfo currentInfo = dllwfList.SelectedValue as AppInfo;
            string tableName = currentInfo.WorkFlowName.Replace("K2Workflow\\", "Biz.");
            List<BizTableColumn> infos = WF.WF_AppDictManager.GetBizTableColumnAndType(tableName).ToList<BizTableColumn>();


            if (infos.Count == 0)
            {
                btnDB.Text = "未匹配到数据库";
                btnDB.ForeColor = Color.Red;
                btnDB.Enabled = false;
                CurrentBizTableColumns.Clear();
            }
            else
            {
                btnDB.Text = "已匹配到数据库";
                btnDB.ForeColor = Color.Green;
                btnDB.Enabled = true;
                CurrentBizTableColumns = infos;
                infos.ForEach(x => x.IsSelected = true);
            }

        }

        private void btnTogether_Click(object sender, EventArgs e)
        {
            List<ActInfo> infos = new List<ActInfo>();
            int rowIndex = -1;
            string actNames = "";
            for (int index = 0; index < dvList.Rows.Count; index++)
            {
                if (dvList.Rows[index].Cells[0].Value != null && bool.Parse((dvList.Rows[index].Cells[0].Value.ToString())))
                {
                    if (rowIndex == -1)
                    {
                        rowIndex = index;
                    }
                    else
                    {
                        actNames += "," + dvList.Rows[index].Cells[2].Value.ToString();
                        infos.AddRange(dvList.Rows[index].Cells[2].Tag as List<ActInfo>);
                        dvList.Rows[index].Cells[2].Tag = null;
                    }
                }
                // 
            }
            if (infos.Count == 0 || rowIndex == -1)
            {
                MessageBox.Show("无需合并", "提示");
                return;
            }

            ///更新合并数据行
            dvList.Rows[rowIndex].Cells[2].Value = dvList.Rows[rowIndex].Cells[2].Value.ToString() + actNames;
            (dvList.Rows[rowIndex].Cells[2].Tag as List<ActInfo>).AddRange(infos);

            //删除被合并数据行
            for (int index = dvList.Rows.Count - 1; index >= 0; index--)
            {
                if (dvList.Rows[index].Cells[2].Tag == null)
                {
                    dvList.Rows.RemoveAt(index);
                }
            }

        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            for (int index = dvList.Rows.Count - 1; index >= 0; index--)
            {
                if (dvList.Rows[index].Cells[0].Value != null && bool.Parse((dvList.Rows[index].Cells[0].Value.ToString())))
                {
                    string[] actNames = dvList.Rows[index].Cells[2].Value.ToString().Split(',');
                    List<ActInfo> actInfos = dvList.Rows[index].Cells[2].Tag as List<ActInfo>;
                    if (actNames.Length == 1)
                    {
                        //无需分离
                        continue;
                    }

                    dvList.Rows[index].Cells[2].Value = actNames[0];
                    dvList.Rows[index].Cells[2].Tag = new List<ActInfo>() { actInfos[0] };

                    for (int subIndex = 1; subIndex < actNames.Length; subIndex++)
                    {
                        dvList.Rows.Insert(index + subIndex);
                        dvList.Rows[index + subIndex].Cells[2].Value = actNames[subIndex];
                        dvList.Rows[index + subIndex].Cells[2].Tag = new List<ActInfo>() { actInfos[subIndex] };
                        dvList.Rows[index + subIndex].Cells[1].Value = actNames[subIndex];
                    }
                }
                // 
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            for (int index = 1; index < dvList.Rows.Count; index++)
            {
                if (dvList.Rows[index].Cells[0].Value != null && bool.Parse((dvList.Rows[index].Cells[0].Value.ToString())))
                {
                    string actNames = dvList.Rows[index].Cells[2].Value.ToString();
                    var tmpList = dvList.Rows[index].Cells[2].Tag;
                    string name = dvList.Rows[index].Cells[1].Value.ToString();

                    dvList.Rows[index].Cells[2].Value = dvList.Rows[index - 1].Cells[2].Value;
                    dvList.Rows[index].Cells[1].Value = dvList.Rows[index - 1].Cells[1].Value;
                    dvList.Rows[index].Cells[2].Tag = dvList.Rows[index - 1].Cells[2].Tag;
                    dvList.Rows[index].Cells[0].Value = false;

                    dvList.Rows[index - 1].Cells[2].Value = actNames;
                    dvList.Rows[index - 1].Cells[1].Value = name;
                    dvList.Rows[index - 1].Cells[2].Tag = tmpList;
                    dvList.Rows[index - 1].Cells[0].Value = true;

                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            for (int index = dvList.Rows.Count - 2; index >= 0; index--)
            {
                if (dvList.Rows[index].Cells[0].Value != null && bool.Parse((dvList.Rows[index].Cells[0].Value.ToString())))
                {
                    string actNames = dvList.Rows[index].Cells[2].Value.ToString();
                    var tmpList = dvList.Rows[index].Cells[2].Tag;
                    string name = dvList.Rows[index].Cells[1].Value.ToString();

                    dvList.Rows[index].Cells[2].Value = dvList.Rows[index + 1].Cells[2].Value;
                    dvList.Rows[index].Cells[1].Value = dvList.Rows[index + 1].Cells[1].Value;
                    dvList.Rows[index].Cells[2].Tag = dvList.Rows[index + 1].Cells[2].Tag;
                    dvList.Rows[index].Cells[0].Value = false;

                    dvList.Rows[index + 1].Cells[2].Value = actNames;
                    dvList.Rows[index + 1].Cells[1].Value = name;
                    dvList.Rows[index + 1].Cells[2].Tag = tmpList;
                    dvList.Rows[index + 1].Cells[0].Value = true;

                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> formDictionary = GetFormDictionary();
            StringBuilder viewString = new StringBuilder();

            foreach (var item in formDictionary)
            {
                viewString.AppendFormat("{0}:\r\n{1}\r\n-----------------------------------------\r\n", item.Key, item.Value);
            }

            File.WriteAllText("C:\\plugin-tmpview.txt", viewString.ToString());
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo("notepad.exe", "C:\\plugin-tmpview.txt");
            proc.Start();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int index = dvList.Rows.Count - 1; index >= 0; index--)
            {
                if (dvList.Rows[index].Cells[0].Value != null && bool.Parse((dvList.Rows[index].Cells[0].Value.ToString())))
                {
                    dvList.Rows.RemoveAt(index);
                }
            }
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            DBFormSelect dbForm = new DBFormSelect(CurrentBizTableColumns);
            if (dbForm.ShowDialog() == DialogResult.OK)
            {
                //
            }
        }


    }
}
