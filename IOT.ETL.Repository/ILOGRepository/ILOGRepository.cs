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
        //定义全部缓存关键字
        string redisKey;
        //获取规则引擎的全部数据
        List<IOT.ETL.Model.etl_data_engine> list = new List<etl_data_engine>();
        //实例化规则引擎的redis
        RedisHelper<Model.etl_data_engine> rh = new RedisHelper<etl_data_engine>();

        //定义视图缓存关键字
        string redisKey2;
        //获取规则引擎视图的全部数据
        List<IOT.ETL.Model.V_IOLG> list2 = new List<V_IOLG>();
        //实例化规则引擎视图的redis
        RedisHelper<Model.V_IOLG> rh2 = new RedisHelper<V_IOLG>();

        //定义登录的关键字
        string loginKey;
        //获取登录的数据
        List<Model.sys_user> users = new List<Model.sys_user>();
        //实例化登录的redis
        RedisHelper<Model.sys_user> loginh = new RedisHelper<Model.sys_user>();

       
        public ILOGRepository()
        {
            redisKey = "ILOG_list";
            list = rh.GetList(redisKey);

            redisKey2 = "ILOG_list2";
            list2 = rh2.GetList(redisKey2);

            loginKey = "Login_list";
            users = loginh.GetList(loginKey);
        }

        //添加
        public async Task<int> AddILOG(etl_data_engine a)
        {
            try
            {
                Model.sys_user us = users.FirstOrDefault();
                a.create_by = us.name;
                a.update_by = us.name;
                string sql = $"insert into etl_data_engine values (UUID(),'{a.engine_name}','{a.engine_type_id}','{a.code_type}','{a.cl_name}',0,'{a.create_by}',now(),'{a.update_by}',now(),'222222')";
                int i =await DapperHelper.Execute(sql);
                if (i > 0)
                {
                    var aa = await DapperHelper.GetList<Model.etl_data_engine>("select * from etl_data_engine order by create_time desc LIMIT 1");
                    a = aa.FirstOrDefault();
                    
                    //存入
                    list.Add(a);
                    rh.SetList(list, redisKey);
                    //不存在
                    list2 = await DapperHelper.GetList<IOT.ETL.Model.V_IOLG>("select * from V_IOLG");
                    //存入
                    rh2.SetList(list2, redisKey2);
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
        public async Task<int> DelILOG(string id)
        {
            try
            {
                string sql = $"delete from etl_data_engine where id in ('{id}')";
                int i =await DapperHelper.Execute(sql);
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
                    //不存在
                    list2 = await DapperHelper.GetList<IOT.ETL.Model.V_IOLG>("select * from V_IOLG");
                    //存入
                    rh2.SetList(list2, redisKey2);
                }
                return i;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //显示
        public async Task<List<V_IOLG>> ShowILOG()
        {
            try
            {
                //判断缓存是否存在
                if (list2 == null || list2.Count == 0)
                {
                    //不存在
                    list2 = await DapperHelper.GetList<IOT.ETL.Model.V_IOLG>("select * from V_IOLG");
                    //存入
                    rh2.SetList(list2, redisKey2);
                }
                //判断缓存是否存在
                if (list == null || list.Count == 0)
                {
                    //不存在
                    list = await DapperHelper.GetList<IOT.ETL.Model.etl_data_engine>("select * from etl_data_engine");
                    //存入
                    rh.SetList(list, redisKey);
                }
                return list2;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //修改
        public async Task<int> UptILOG(etl_data_engine a)
        {
            try
            {
                Model.sys_user us = users.FirstOrDefault();
                a.update_by = us.name;
                string sql = $"update etl_data_engine set engine_name='{a.engine_name}',engine_type_id='{a.engine_type_id}',code_type='{a.code_type}',cl_name='{a.cl_name}',update_by='{a.update_by}' where id='{a.id}'";
                int i = await DapperHelper.Execute(sql);
                if (i > 0)
                {
                    list[list.IndexOf(list.First(x=>x.id==a.id))] = a;
                    //重新存入
                    rh.SetList(list, redisKey);
                    //不存在
                    list2 = await DapperHelper.GetList<IOT.ETL.Model.V_IOLG>("select * from V_IOLG");
                    //存入
                    rh2.SetList(list2, redisKey2);
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
