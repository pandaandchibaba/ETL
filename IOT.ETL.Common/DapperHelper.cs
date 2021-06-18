using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using Newtonsoft.Json;

namespace IOT.ETL.Common
{
    public  class DapperHelper
    {
        #region 获取数据
        /// <summary>
        /// 获取数据
        /// MySql.Data.MySqlClient.MySqlException:“Unknown column 'a.WarehouseId' in 'field list'”</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(string sql,string dbName="", int code = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(dbName))
                {
                    using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                    {
                        return db.Query<T>(sql).ToList();
                    }
                }
                else
                {
                    using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnMySql+dbName))
                    {
                        return db.Query<T>(sql).ToList();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 获取受影响的行数
        /// <summary>
        /// 获取受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Execute(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return db.Execute(sql);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 首行首列
        /// <summary>
        /// 获取首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object Exescalar(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return db.ExecuteScalar(sql);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 获取MySql的数据
        /// <summary>
        /// DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="dbName">数据库名</param>
        /// <returns></returns>
        public static string GetMySqlDate(string sql,string dbName)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnMySql+dbName))
                {
                    var reader = db.Query(sql);
                    return JsonConvert.SerializeObject(reader);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
