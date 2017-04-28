using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WFWizard.WF
{
    public class DataProvider
    {
        private string connectionString;
        /// <summary>
        /// 设置数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            set { connectionString = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataProvider()
        {
            this.connectionString = ConnectionStringConst.DBConnectionString;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public DataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #region 非连接查询SqlDataAdapter对象的Fill()方法，Fill()之前连接自动打开，Fill()之后自动关闭
        /// <summary>
        /// 1执行一个非连接式查询，并返回结果集
        /// </summary>
        /// <param name="sql">要执行的查询SQL文本命令</param>
        /// <returns>返回查询结果集</returns>
        public DataTable ExecuteDataTable(string sql)
        {
            return ExecuteDataTable(sql, CommandType.Text, null);

        }
        public DataTable ExecutedProcedure(string procName, SqlParameter[] parameters)
        {
            return ExecuteDataTable(procName, CommandType.StoredProcedure, parameters);
        }

        /// <summary>
        /// 2执行一个查询，返回查询结果
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者sql文本命令</param>
        /// <returns>返回查询结果集</returns>
        public DataTable ExecuteDataTable(string sql, CommandType commandType)
        {
            return ExecuteDataTable(sql, commandType, null);
        }
        /// <summary>
        /// 3执行一个查询，并返回结果非连接查询
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者sql文本命令</param>
        /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
        /// <returns>返回查询结果集</returns>
        public DataTable ExecuteDataTable(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            //实例化DataType,用于装载查询结果集
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //设置command的CommandType为指定的CommandType
                    command.CommandType = commandType;
                    //如果同时传入了参数，则添加这些参数
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    //通过包含SQL的SqlCommand实例来实例化SqlDataAdapter对象
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    //填充DataTable
                    adapter.Fill(data);
                }
                return data;
            }
        }
        #endregion

        #region SqlDataReader连接式查询SqlCommand类的ExecuteReader方法
        /// <summary>
        /// 1执行一个连接式查询，返回一个SqlDataReader对象的实例
        /// </summary>
        /// <param name="sql">要执行的sql文本命令</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string sql)
        {
            return ExecuteReader(sql, CommandType.Text, null);
        }
        /// <summary>
        ///  2执行一个查询，返回一个SqlDataReader对象的实例
        /// </summary>
        /// <param name="sql">要执行的sql命令</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者sql文本命令</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string sql, CommandType commandType)
        {
            return ExecuteReader(sql, commandType, null);
        }
        /// <summary>
        /// 3执行一个查询，返回一个SqlDataReader对象的实例连接查询
        /// </summary>
        /// <param name="sql">要执行的sql命令</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者sql文本命令</param>
        /// <param name="parameters">sql语句或存储过程的参数数组</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            if (parameters != null)
            {
                foreach (SqlParameter paramete in parameters)
                {
                    command.Parameters.Add(parameters);
                }
            }
            connection.Open();
            //CommandBehavior.CloseConnection参数指示关闭Reader对象时关闭与其关联的Connection对象
            return command.ExecuteReader(CommandBehavior.CloseConnection);

        }
        #endregion

        #region 返回第一行第一列command.ExecuteScalar();
        /// <summary>
        /// 执行一个查询sql，返回第一行第一列，忽略其他行和列
        /// </summary>
        /// <param name="sql">要执行的sql语句文本命令</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, CommandType.Text, null);
        }
        /// <summary>
        ///  执行一个查询sql，返回第一行第一列，忽略其他行和列
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object EcecuteScalar(string sql, CommandType commandType)
        {
            return ExecuteScalar(sql, commandType, null);
        }
        public object ExecuteScalar(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            //定义一个变量，装载object类型返回值
            object result = null;
            //using语句在此处的用法用于定义一个范围，在此范围的末尾释放对象，相当于try{connection.Open();}finally{connection.Close();}
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlConnection和SqlCommand对象都是想了IDisposable接口,都会用Dispose()方法实现资源的释放，是使用using{}语句的前提条件
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    //打开连接
                    connection.Open();
                    //调用SqlCommand对象的ExecuteScalar（）方法，返回第一行第一列
                    result = command.ExecuteScalar();
                }
            }
            return result;
        }
        #endregion

        #region 执行对数据的增删改操作，SqlCommand类的ExecuteNonQuery
        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, CommandType.Text, null);
        }
        public int ExecuteNonQuery(string sql, CommandType commandType)
        {
            return ExecuteNonQuery(sql, commandType, null);
        }
        /// <summary>
        /// 对数据库执行增删改操作
        /// </summary>
        /// <param name="sql">要执行的sql命令</param>
        /// <param name="commandType">要执行的查询语句的类型</param>
        /// <param name="parameters">参数数组</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //设置command的CommandType类型
                    command.CommandType = commandType;
                    //如果同时传入了参数，添加这些参数
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    //打开数据库连接
                    connection.Open();
                    //调用SqlCommand类的ExecuteNonQuery()方法，返回受影响的行数
                    count = command.ExecuteNonQuery();
                }
            }
            //返回count受影响的行数
            return count;
        }
        #endregion

        #region 返回当前连接的数据库中所有用户创建的数据库
        /// <summary>
        /// 返回当前连接的数据库中所有用户创建的数据库
        /// </summary>
        /// <returns></returns>
        public DataTable GetTables()
        {
            DataTable data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                data = connection.GetSchema("Tables");
            }
            return data;
        }
        #endregion
    }
}
