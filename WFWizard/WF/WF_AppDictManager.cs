using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WFWizard.WF
{
    public class WF_AppDictManager
    {
        public static DataTable GetAppDictByAppName(string appName)
        {
            DataProvider dataProvider = new DataProvider();
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appName",System.Data.SqlDbType.VarChar,500),
            };
            parameters[0].Value = appName;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetAppDictByAppName", parameters);
            return dataTable;
        }

        public static DataTable GetAppDictToDataTable()
        {
            DataProvider dataProvider = new DataProvider();
            DataTable dataTable = dataProvider.ExecuteDataTable("select * from [WF_AppDict]", CommandType.Text);
            return dataTable;
        }
        public static DataTable GetBizTableColumnAndType(string tableName)
        {
            DataProvider dataProvider = new DataProvider();
            DataTable dataTable = dataProvider.ExecuteDataTable(@"select C.Name,T.name as Ntype 
from syscolumns C
join  sys.types	T
on C.xtype=T.user_type_id
where id=object_id('" + tableName + "')", CommandType.Text);
            return dataTable;
        }
        public static DataTable GetAppDictByAppID(string appId)
        {
            DataProvider dataProvider = new DataProvider();
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            };
            parameters[0].Value = appId;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetAppDictByAppId", parameters);
            return dataTable;
        }

        public static void EditAppDict(string appId, string appName, string workFlowName, string formName)
        {
            DataProvider dataProvider = new DataProvider();
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            new SqlParameter("@appName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@workFlowName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@formName",System.Data.SqlDbType.VarChar,1000),
            };
            parameters[0].Value = appId;
            parameters[1].Value = appName;
            parameters[2].Value = workFlowName;
            parameters[3].Value = formName;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_EditAppDict", parameters);
        }

        public static DataTable GetAppListByAppIdOrAppNameOrWorkFlowNameOrFormName(string appId, string appName, string workFlowName, string formName)
        {
            DataProvider dataProvider = new DataProvider();
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            new SqlParameter("@appName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@workFlowName",System.Data.SqlDbType.VarChar,500),
            new SqlParameter("@formName",System.Data.SqlDbType.VarChar,1000),
            };
            parameters[0].Value = appId;
            parameters[1].Value = appName;
            parameters[2].Value = workFlowName;
            parameters[3].Value = formName;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetAppListByAppIdOrAppNameOrWorkFlowNameOrFormName", parameters);
            return dataTable;
        }

        public static DataTable GetWorkFlowActByAppID(string appId)
        {
            DataProvider dataProvider = new DataProvider();
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            };
            parameters[0].Value = appId;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetWorkFlowActByAppID", parameters);
            return dataTable;
        }

        public static DataTable GetWorkFlowDFByAppID(string appId)
        {
            DataProvider dataProvider = new DataProvider();
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@appId",System.Data.SqlDbType.VarChar,100),
            };
            parameters[0].Value = appId;
            DataTable dataTable = dataProvider.ExecutedProcedure("wf_usp_GetWorkFlowDFByAppID", parameters);
            return dataTable;
        }
    }
}
