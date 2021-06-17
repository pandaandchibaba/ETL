using IOT.ETL.IRepository.IDataAnalysisRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Common;

namespace IOT.ETL.Repository.DataAnalysisRepository
{
    public class DataAnalysisRepository : IDataAnalysisRepository
    {
        #region 显示查询
        /// <summary>
        /// 显示查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql, string dbName,int code=0)
        {
            return DapperHelper.GetTable(sql, dbName);
        } 
        #endregion
    }
}
