using IOT.ETL.Common;
using IOT.ETL.IRepository.ILOGIRepository;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.ILOGRepository
{
    public class ILOGRepository : ILOGIRepository
    {
        //定义缓存关键字
        string redisKey;
        string redisKey2;
        //获取全部数据
        List<IOT.ETL.Model.etl_data_engine> list = new List<etl_data_engine>();

        List<IOT.ETL.Model.V_IOLG> list2 = new List<V_IOLG>();
        //实例化redis
        RedisHelper<Model.etl_data_engine> rh = new RedisHelper<etl_data_engine>();

        RedisHelper<Model.V_IOLG> rh2 = new RedisHelper<V_IOLG>();
        public ILOGRepository()
        {
            redisKey = "ILOG_list";
            redisKey2 = "ILOG_list2";
            list = rh.GetList(redisKey);
            list2 = rh2.GetList(redisKey2);
        }

        //添加
        public int AddILOG(etl_data_engine a)
        {
            try
            {
                string sql = $"insert into etl_data_engine values (UUID(),'{a.engine_name}','{a.engine_type_id}','{a.code_type}','{a.cl_name}',0,'高紫如',now(),'高紫如',now(),'222222')";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    a = DapperHelper.GetList<Model.etl_data_engine>("select * from etl_data_engine order by id desc LIMIT 1").FirstOrDefault();
                    //存入
                    list.Add(a);
                    rh.SetList(list, redisKey);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //删除
        public int DelILOG(string id)
        {
            try
            {
                string sql = $"delete from etl_data_engine where id='{id}'";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    string[] arr = id.Split(',');
                    foreach (var item in arr)
                    {
                        Model.etl_data_engine me = list.FirstOrDefault(x => x.id.Equals(item));
                        list.Remove(me);
                    }
                    //重新存入
                    rh.SetList(list, redisKey);
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //显示
        public List<V_IOLG> ShowILOG()
        {
            try
            {
                //判断缓存是否存在
                if (list2 == null || list2.Count == 0)
                {
                    //不存在
                    list2 = DapperHelper.GetList<IOT.ETL.Model.V_IOLG>("select * from V_IOLG");
                    //存入
                    rh2.SetList(list2, redisKey2);
                }
                return list2;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //修改
        public int UptILOG(etl_data_engine a)
        {
            try
            {
                string sql = $"update etl_data_engine set engine_name='{a.engine_name}',engine_type_id='{a.engine_type_id}',code_type='{a.code_type}',cl_name='{a.cl_name}' where id='{a.id}'";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    Model.etl_data_engine me = list.FirstOrDefault(x => x.id.Equals(a.id));
                    list[list.IndexOf(me)] = a;
                    //重新存入
                    rh.SetList(list, redisKey);
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
