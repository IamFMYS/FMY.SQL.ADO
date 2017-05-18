using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FMY.SQL.ADO
{
    public class SQLHelper
    {
        private static string connectionStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="parameters">sql参数</param>
        /// <returns>受影响行数</returns>
        public static int Excute(string cmdText, CommandType commandType, params SqlParameter[] parameters)
        {
            //test
            int result = 0;
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();//打开链接
                using (SqlCommand cmd = new SqlCommand(cmdText, conn)) //新建执行对象
                {
                    cmd.CommandType = commandType;
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);
                    result = cmd.ExecuteNonQuery();//执行
                    return result;
                }
            }
        }
        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="parameters">sql参数</param>
        /// <returns></returns>
        public static DataTable Select(string cmdText, CommandType commandType, params SqlParameter[] parameters)
        {
            DataTable result = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmdText, conn))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    result = ds.Tables[0];
                    return result;
                }
            }
        }
        /// <summary>
        /// 获取一个数据对象
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="parameters">sql参数</param>
        /// <returns>第一行第一列数据</returns>
        public static object SelectFirst(string cmdText, CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }                
            }
        }

        public static string GetSqlStr(string sql)
        {
            return null;
        }
    }
}
