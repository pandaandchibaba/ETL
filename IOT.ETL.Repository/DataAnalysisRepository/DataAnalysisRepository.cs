using IOT.ETL.IRepository.IDataAnalysisRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Common;
using IOT.ETL.Model;

namespace IOT.ETL.Repository.DataAnalysisRepository
{
    public class DataAnalysisRepository : IDataAnalysisRepository
    {
        //mysql的数据库关键字
        private string myDBkey= "myDB";
        //sql的数据库关键字
        private string sqlDBkey = "sqlDB";
        //mysql的数据表关键字
        private string myTablekey= "myTable";
        //mysql的数据库值
        List<myDataBase> lstMydb = new List<myDataBase>();
        //mysql的数据表值
        List<myTable> lstMyTb = new List<myTable>();
        //帮助文件
        RedisHelper<myDataBase> mdbH = new RedisHelper<myDataBase>();
        RedisHelper<myTable> mtH = new RedisHelper<myTable>();
        public DataAnalysisRepository()
        {
            lstMydb = mdbH.GetList(myDBkey);
            lstMyTb = mtH.GetList(myTablekey);
        }
        #region 绑定左侧树
        /// <summary>
        /// 绑定左侧树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> BindTree()
        {
            //实例化一个字段集合
            List<Dictionary<string, object>> tree = new List<Dictionary<string, object>>();
            #region MySql节点
            //实例化一个mysql跟节点
            Dictionary<string, object> myDic = new Dictionary<string, object>();
            myDic.Add("id", "");
            myDic.Add("label", "MySql");
            //mysql的数据库
            if (lstMydb == null || lstMydb.Count == 0)
            {
                //存入缓存
                lstMydb = DapperHelper.GetList<myDataBase>("SHOW DATABASES");
                mdbH.SetList(lstMydb, myDBkey);
            }
            //mysql的数据表
            if (lstMyTb == null || lstMyTb.Count == 0)
            {
                //存入缓存
                lstMyTb = DapperHelper.GetList<myTable>("select Table_Name,Table_Schema from information_schema.tables where table_type='base table'");
                mtH.SetList(lstMyTb, myTablekey);
            }
            //存放mysql数据库节点
            List<Dictionary<string, object>> treeMydb = new List<Dictionary<string, object>>();
            foreach (var db in lstMydb)
            {
                //实例化一个数据库
                Dictionary<string, object> dbDic = new Dictionary<string, object>();
                dbDic.Add("id", "");
                dbDic.Add("label", db.Database);
                //该数据库下的表
                List<myTable> lstDbTb = lstMyTb.Where(x => x.Table_Schema == db.Database).ToList();
                //存放mysql数据表节点
                List<Dictionary<string, object>> treeMytb = new List<Dictionary<string, object>>();
                foreach (var tb in lstDbTb)
                {
                    //实例化一个数据表
                    Dictionary<string, object> tbDic = new Dictionary<string, object>();
                    tbDic.Add("id", tb.Table_Schema);
                    tbDic.Add("label", tb.Table_Name);
                    tbDic.Add("children", null);
                    //放入集合
                    treeMytb.Add(tbDic);
                }
                //数据库下面的表
                dbDic.Add("children", treeMytb);
                //放入集合
                treeMydb.Add(dbDic);
            }
            //MySQL下面的数据库
            myDic.Add("children", treeMydb);
            tree.Add(myDic);
            #endregion

            //实例化一个sql跟节点
            //Dictionary<string, object> sqlDic = new Dictionary<string, object>();
            //myDic.Add("id", "");
            //myDic.Add("label", "Sql");
            return tree;
        } 
        #endregion
        #region 显示查询
        /// <summary>
        /// 显示查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql, string dbName,int code=0)
        {
            return DapperHelper.GetTable(sql,dbName);
        }
        #endregion
    }
}
