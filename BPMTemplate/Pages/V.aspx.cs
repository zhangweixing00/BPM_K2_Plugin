using System;
using System.Linq;

[BPM(AppId = "$AppId$")]
public partial class Workflow_ApprovePage_$safeitemname$ 
    :V_WorkflowFormBase
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
    }
}
