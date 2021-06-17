using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Common
{
    public class AdoHelper
    {
        #region 获取数据表数据
        /// <summary>
        /// 获取数据表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable(string sql, string dbName = "")
        {
            //非托管资源要使用using
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnSql))
            {
                //打开数据库连接字符串
                conn.Open();
                //创建适配器对象
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                //新建数据表
                DataTable dt = new DataTable();
                //数据填充
                sda.Fill(dt);
                return dt;

            }
        }
        #endregion

        #region 转成泛型集合
        /// <summary>
        /// 转成泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>(string sql) where T : class, new()
        {
            //获取数据
            DataTable dt = GetTable(sql);
            //获取对象类
            var tp = typeof(T);
            //获取对象的属性
            var props = tp.GetProperties();
            //获取所有数据行
            var rows = dt.Rows;
            //定义一个泛型集合
            List<T> list = new List<T>();
            //遍历所有的行
            foreach (DataRow row in rows)
            {
                //实例化一个对象
                T t = new T();
                //遍历所有属性
                foreach (var prop in props)
                {
                    //判断表的列名与模型的属性是否相等且不为空
                    if (dt.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        //反射给模型的属性
                        prop.SetValue(t, row[prop.Name]);
                    }
                }
                list.Add(t);
            }
            return list;
        } 
        #endregion
    }
}
