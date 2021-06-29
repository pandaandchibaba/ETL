using IOT.ETL.IRepository.IDataAnalysisRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Common;
using IOT.ETL.Model;
using System.Diagnostics;

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
        //sql的数据库值
        List<SqlDataBase> lstdb = new List<SqlDataBase>();
        //mysql的数据表值
        List<myTable> lstMyTb = new List<myTable>();
        //帮助文件
        RedisHelper<myDataBase> mdbH = new RedisHelper<myDataBase>();
        RedisHelper<myTable> mtH = new RedisHelper<myTable>();
        RedisHelper<SqlDataBase> sdbH = new RedisHelper<SqlDataBase>();
        #region 构造函数
        /// <summary>
        /// 初次加载取出
        /// </summary>
        public DataAnalysisRepository()
        {
            //mysql数据库
            lstMydb = mdbH.GetList(myDBkey);
            //mysql数据表
            lstMyTb = mtH.GetList(myTablekey);
            //SQL数据库
            lstdb = sdbH.GetList(sqlDBkey);
        } 
        #endregion

        #region 绑定左侧树
        /// <summary>
        /// 绑定左侧树
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dictionary<string, object>>> BindTree()
        {
            //实例化一个字段集合
            List<Dictionary<string, object>> tree = new List<Dictionary<string, object>>();
            #region MySql节点
            //实例化一个mysql跟节点
            Dictionary<string, object> myDic = new Dictionary<string, object>();
            myDic.Add("id", "");
            myDic.Add("label", "MySql");
            myDic.Add("floor", 1);
            //mysql的数据库
            if (lstMydb == null || lstMydb.Count == 0)
            {
                //存入缓存
                lstMydb = await DapperHelper.GetList<myDataBase>("SHOW DATABASES");
                mdbH.SetList(lstMydb, myDBkey);
            }
            //mysql的数据表
            if (lstMyTb == null || lstMyTb.Count == 0)
            {
                //存入缓存
                lstMyTb = await DapperHelper.GetList<myTable>("select Table_Name,Table_Schema from information_schema.tables where table_type='base table'");
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
                dbDic.Add("floor", 2);
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
                    tbDic.Add("floor", 3);
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

            #region SQL节点
            //实例化一个sql跟节点
            Dictionary<string, object> sqlDic = new Dictionary<string, object>();
            sqlDic.Add("id", "");
            sqlDic.Add("label", "Sql");
            sqlDic.Add("floor", 1);
            //sql的数据库
            if (lstdb == null || lstdb.Count == 0)
            {
                //存入缓存
                lstdb = SqlHelper.GetList<SqlDataBase>("select name,dbid from sysdatabases", "master");
                sdbH.SetList(lstdb, sqlDBkey);
            }
            //存放mysql数据库节点
            List<Dictionary<string, object>> treeSqldb = new List<Dictionary<string, object>>();
            foreach (var db in lstdb)
            {
                //实例化一个数据库
                Dictionary<string, object> dbDic = new Dictionary<string, object>();
                dbDic.Add("id", "");
                dbDic.Add("label", db.name);
                dbDic.Add("floor", 2);
                //该数据库下的表
                List<SqlTable> lstDbTb = SqlHelper.GetList<SqlTable>("select name from sysobjects where xtype='U'", db.name);
                //存放mysql数据表节点
                List<Dictionary<string, object>> treeMytb = new List<Dictionary<string, object>>();
                foreach (var tb in lstDbTb)
                {
                    //实例化一个数据表
                    Dictionary<string, object> tbDic = new Dictionary<string, object>();
                    tbDic.Add("id", db.name);
                    tbDic.Add("label", tb.name);
                    tbDic.Add("floor", 3);
                    tbDic.Add("children", null);
                    //放入集合
                    treeMytb.Add(tbDic);
                }
                //数据库下面的表
                dbDic.Add("children", treeMytb);
                //放入集合
                treeSqldb.Add(dbDic);
            }
            sqlDic.Add("children", treeSqldb);
            tree.Add(sqlDic);
            #endregion

            return tree;
        }
        #endregion

        #region 根据不同的数据库显示不同的表数据
        /// <summary>
        /// 根据不同的数据库显示不同的表数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async Task<JsonTable> GetDateTable(string sql, string dbName,string code)
        {
            JsonTable jt = new JsonTable();  //返回对象
            Stopwatch sw = new Stopwatch();  //实例化秒表
            if (code=="MySql")  //mysql
            {
                sw.Start();
                jt.Json= await DapperHelper.GetMySqlDate(sql, dbName);
                sw.Stop();
            }
            else  //sql
            {
                sw.Start();
                jt.Json = await SqlHelper.GetSqlDate(sql, dbName);
                sw.Stop();
            }
            jt.Time = sw.ElapsedMilliseconds / 1000.0 + "秒";  //执行时间
            return jt;
        }
        #endregion
    }
}
