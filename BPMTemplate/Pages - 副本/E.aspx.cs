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
using System.Diagnostics;
using System.Xml;
using System.Net;
using System.IO;
using Pkurg.PWorldBPM.Business.AttachmentMan;
using System.Text;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;

[BPM(AppId="$AppId$")]
public partial class Workflow_EditPage_$safeitemname$
    : E_WorkflowFormBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("ERP_@_");
                FormTitle = "";//设置标题
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;

                InitFormData();

                //Countersign1.CounterSignDeptId = StartDeptId;
            }
        }
    }

    protected override void InitFormData()
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

    protected override void SaveFormData()
    {
       
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：$CS_K2_UserDataField$
        NameValueCollection dataFields = new NameValueCollection();
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：$CS_K2_UserDataField$
        ///自动生成
        //$CS_K2_UserDataFieldCode$

        return dfInfos;
    }

    /// <summary>
    /// 流程发起前操作
    /// </summary>
    /// <returns></returns>
    protected override bool BeforeWorkflowStart()
    {
        return true;
    }

    /// <summary>
    /// 流程成功启动后操作
    /// </summary>
    protected override void  AfterWorkflowStart()
    {
 	 
    }


    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        Save();
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        Submit();
    }

    /// <summary>
    /// 终止
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        DelWorkflow();
    }

}
