using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace IOT.ETL.Common
{
    public class DapperHelper
    {
        /// <summary>
        /// 获取数据
        /// MySql.Data.MySqlClient.MySqlException:“Unknown column 'a.WarehouseId' in 'field list'”</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<List<T>>  GetList<T>(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return (List<T>)await db.QueryAsync<T>(sql);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<int>  Execute(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return await db.ExecuteAsync(sql);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<object> Exescalar(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return await db.ExecuteScalarAsync(sql);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据数据库名称获取其中表数据MySql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        public  static async Task<string> GetDataTable(string sql, string name)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnName + name))
                {
                    var  reader = await db.QueryAsync(sql);

                    string json = JsonConvert.SerializeObject(reader);

                    return  json;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 根据数据库名称获取其中表数据SqlServer
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        public static async Task<string> GetDataTableSql(string sql, string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnNameSql + name))
                {
                    var reader = await db.QueryAsync(sql);

                    string json = JsonConvert.SerializeObject(reader);

                    return json;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取数据_BI数据分析
        ///</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetList_BI<T>(string sql, string name, int flag = 1)
        {
            try
            {
                if (flag != 1)
                {
                    string conn = ConfigurationManager.ConnNameSql + name;
                    using (IDbConnection db = new SqlConnection(conn))
                    {
                        return (List<T>)await db.QueryAsync<T>(sql);
                    }
                }
                else
                {
                    using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                    {
                        return (List<T>)await db.QueryAsync<T>(sql);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 任务管理检测
        ///</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<int> Execute_plan(string sql, string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    string conn = ConfigurationManager.ConnNameSql + name;
                    using (IDbConnection db = new SqlConnection(conn))
                    {
                        return await db.ExecuteAsync (sql);
                    }
                }
                else
                {
                    using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                    {
                        return await db.ExecuteAsync(sql);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        #region 获取MySql的数据
        /// <summary>
        /// DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="dbName">数据库名</param>
        /// <returns></returns>
        public static async Task<string> GetMySqlDate(string sql,string dbName)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnMySql + dbName))
                {
                    var reader = await db.QueryAsync(sql);
                    int i = reader.Count();
                    Random rd = new Random();
                    if (i<10)
                    {
                        if (i>5)
                        {
                            reader = reader.Skip(rd.Next(0, i - 5)).Take(5);
                        }
                        else
                        {
                            reader = reader.Take(5);
                        }
                    }
                    else
                    {
                        reader = reader.Take(10);
                    }
                    return JsonConvert.SerializeObject(reader);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        /// <summary>
        /// 测试连接字符串是否正确
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static int Ceshi(string Conn)
        {
            try
            {
                using (IDbConnection con = new MySqlConnection(Conn))
                {
                    dynamic n = con.Query("select uuid()");
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }





    }
}



