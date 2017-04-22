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

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();//打开链接
                SqlCommand cmd = new SqlCommand(cmdText, conn);//新建执行对象
                cmd.CommandType = commandType;
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                result = cmd.ExecuteNonQuery();//执行
                return result;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { conn.Close(); }
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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString());
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = commandType;
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static string GetSqlStr(string sql)
        {
            return null;
        }
    }
}
