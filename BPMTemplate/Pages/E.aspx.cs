using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

[BPM(AppId = "$AppId$")]
public partial class Workflow_EditPage_$safeitemname$
    : E_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitStartDeptment();
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("ERP_@_");
                FormTitle = "";//设置标题
            }
            else
            {
                FormId = _BPMContext.ProcInst.FormId;
                FormTitle = _BPMContext.ProcInst.ProcName;

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
            //var info = BizContext.$TableName$.FirstOrDefault(x => x.FormID == FormId);
            //if (info != null)
            //{
            //$TableToForm_Code$
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        //var info = BizContext.$TableName$.FirstOrDefault(x => x.FormID == FormId);
        //if (info == null)
        //{
        //    BizContext.$TableName$.InsertOnSubmit(new Pkurg.PWorldBPM.Business.BIZ.$TableName$()
        //    {
        //        FormID = FormId,
        //        $FormToTable_Code_0$
        //    });
        //}
        //else
        //{
        //    $FormToTable_Code_1$
        //}
        //BizContext.SubmitChanges();
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：$CS_K2_UserDataField$
        NameValueCollection dataFields = new NameValueCollection();
        //dataFields.Add("IsPass", "1");
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
    protected override bool AfterWorkflowStart(int wfInstanceId)
    {
        return base.AfterWorkflowStart(wfInstanceId);
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
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDepartName.SelectedItem.Value;
    }
    private void InitStartDeptment()
    {
        ddlDepartName.DataSource = GetStartDeptmentDataSource();
        ddlDepartName.DataTextField = "Remark";
        ddlDepartName.DataValueField = "DepartCode";
        ddlDepartName.DataBind();
    }
}
