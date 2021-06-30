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
        public static async Task<List<T>> GetList<T>(string sql, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnSql + dbName))
            {
                try
                {
                    return (List<T>)await conn.QueryAsync<T>(sql);
                }
                catch (Exception)
                {

                    throw;
                }

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
                    int i = reader.Count();
                    Random rd = new Random();
                    if (i < 10)
                    {
                        if (i > 5)
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
    }
}
