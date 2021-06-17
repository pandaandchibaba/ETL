using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Model;

namespace IOT.ETL.IRepository.IDataAnalysisRepository
{
    public interface IDataAnalysisRepository
    {
        #region 显示查询接口
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql, string dbName, int code = 0);
        #endregion

        #region 绑定树
        /// <summary>
        /// 绑定树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> BindTree();
        #endregion
    }
}
