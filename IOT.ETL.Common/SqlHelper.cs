using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Common
{
    public class SqlHelper
    {
        #region SQL显示查询
        /// <summary>
        /// 显示查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(string sql, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnSql + dbName))
            {
                return conn.Query<T>(sql).ToList();
            }
        } 
        #endregion

        #region 获取Sql的数据
        /// <summary>
        /// GetSqlDate
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="dbName">数据库名</param>
        /// <returns></returns>
        public static async Task<string> GetSqlDate(string sql, string dbName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnSql + dbName))
                {
                    var reader = await db.QueryAsync(sql);
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
