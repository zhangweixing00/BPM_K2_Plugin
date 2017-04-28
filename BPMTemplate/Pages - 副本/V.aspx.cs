using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Entities;
using Pkurg.PWorld.Business.Permission;
using System.Data;
using SourceCode.Workflow.Client;
using Pkurg.PWorldBPM.Business;

using Pkurg.PWorldBPM.Common.Log;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Common;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;
using System.Text;

[BPM(AppId="$AppId$")]
public partial class Workflow_ApprovePage_$safeitemname$ 
    : A_WorkflowFormBase
{
    /// <summary>
    /// 对已保存的表单，从数据库中加载表单数据
    /// </summary>
    protected  void InitFormData()
    {
        try
        {
            ///加载业务数据
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ///加载页面数据
            string instId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(instId))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
                InitFormData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }
    }
}
