﻿using System;
using System.Linq;
using System.Collections.Specialized;

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
            //var info = BizContext.$TableName$.FirstOrDefault(x => x.FormID == FormId);
            //if (info != null)
            //{
            //$TableToForm_Code$
            //}
            lbDeptName.Text = SysContext.WF_WorkFlowInstance.FirstOrDefault(x => _BPMContext.ProcID == x.InstanceID).CreateDeptName;

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
                FormId = _BPMContext.ProcInst.FormId;
                FormTitle = _BPMContext.ProcInst.ProcName;
                InitFormData();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }
        }

        ShowButton();

        SetMenu();
    }


    protected override bool BeforeWorkflowApproval(ref string action, ref string option)
    {
        uploadAttachments.SaveAttachment(FormId);
        switch (action)
        {
            case "提交":
                action = "同意";
                break;
            case "不同意":
                ChangeResultToUnAgree();
                break;
            default:
                break;
        }
        option = string.IsNullOrEmpty(option) ? action : option;
        return true;

    }

    //批准
    protected void Agree_Click(object sender, EventArgs e)
    {
        string action = "同意";
        Approval(action);
    }
    //拒绝
    protected void Reject_Click(object sender, EventArgs e)
    {
        string action = "不同意";
        Approval(action);
    }
    //提交
    protected void Submit_Click(object sender, EventArgs e)
    {
        string action = "提交";
        Approval(action);
    }

    /// <summary>
    /// 更改结果
    /// </summary>
    public void ChangeResultToUnAgree()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "0");
        WorkflowManage.ModifyDataField(_BPMContext.Sn, dataFields);
    }

    /// <summary>
    /// 按钮设置1
    /// </summary>
    private void SetMenu()
    {
        if (K2_TaskItem != null && K2_TaskItem.ActivityInstanceDestination.Name == "集团会签")
        {
            UnOptions.Visible = false;
            lbAgree.Text = "提交";
        }
    }

    /// <summary>
    /// 按钮设置2
    /// </summary>
    private void ShowButton()
    {
        if (string.IsNullOrEmpty(Request.QueryString["sn"]))
        {
            Options.Visible = false;
            ASOptions.Visible = false;
        }
        else
        {
            bool isAddSign = new Workflow_Common().IsAddSign(Request.QueryString["sn"], _BPMContext.CurrentUser.LoginId);
            if (isAddSign)
            {
                Options.Visible = false;
                ASOptions.Visible = true;
            }
            else
            {
                Options.Visible = true;
                ASOptions.Visible = false;
            }
        }

    }
   
    //WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    //BFApprovalRecord bfApproval = new BFApprovalRecord();

    #region 执行过程中更新参数

    /// <summary>
    /// 执行过程中更新参数
    /// </summary>
    private void UpdateWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();
        switch (K2_TaskItem.ActivityInstanceDestination.Name)
        {
            //示例
            //case "步骤":
            //    dataFields.Add("对应流程参数",Workflow_Common.GetRoleUsers("部门ID", "角色"));
            //    break;
            default:
                break;
        }

        if (dataFields.Count != 0 && !string.IsNullOrEmpty(_BPMContext.Sn))
        {
            WorkflowHelper.UpdateDataFields(_BPMContext.Sn, dataFields, _BPMContext.CurrentUser.ApprovalUser);
        }
    }
    #endregion
}
