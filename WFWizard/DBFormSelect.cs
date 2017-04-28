using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFWizard.WF;

namespace WFWizard
{
    public partial class DBFormSelect : Form
    {
        public List<BizTableColumn> Infos { get; set; }
        public DBFormSelect(List<BizTableColumn> infos)
        {
            InitializeComponent();
            Infos = infos;

            LoadColInfos(infos);
        }

        private void LoadColInfos(List<BizTableColumn> infos)
        {
            if (infos.Count == 0)
            {
                return;
            }

            dvList.Rows.Clear();
            dvList.Rows.Add(infos.Count);

            for (int index = 0; index < infos.Count; index++)
            {
                dvList.Rows[index].Cells[1].Value = string.IsNullOrEmpty(infos[index].Des) ? infos[index].Name : infos[index].Des;
                dvList.Rows[index].Cells[2].Value = infos[index].Name;
                dvList.Rows[index].Cells[2].Tag = infos[index];
                dvList.Rows[index].Cells[0].Value = infos[index].IsSelected;
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            List<BizTableColumn> newInfos = new List<BizTableColumn>();
            foreach (DataGridViewRow item in dvList.Rows)
            {
                var itemInfo = item.Cells[2].Tag as BizTableColumn;
                itemInfo.Des = item.Cells[1].Value.ToString();
                if (item.Cells[0].Value != null && bool.Parse((item.Cells[0].Value.ToString())))
                {

                    itemInfo.IsSelected = true;
                    newInfos.Add(itemInfo);
                }
                else
                {
                    itemInfo.IsSelected = false;
                }
            }
            //Infos = newInfos;
            this.DialogResult = DialogResult.OK;
        }
    }
}
