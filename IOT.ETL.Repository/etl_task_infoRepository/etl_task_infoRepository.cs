using IOT.ETL.Common;
using IOT.ETL.IRepository.Ietl_task_info;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.etl_task_info
{
    public class etl_task_infoRepository : Ietl_task_infoRepository
    {
        string redisKey;//全局变量
        List<Model.etl_task_info> lt = new List<Model.etl_task_info>();
        RedisHelper<Model.etl_task_info> rd = new RedisHelper<Model.etl_task_info>();//redis缓存

        public etl_task_infoRepository()//最先加载
        {
            redisKey = "etl_task_info_list";//redis名称
            lt = rd.GetList(redisKey);//存放
        }

        public async Task<List<T>> dbtable<T>()
        {
            throw new NotImplementedException();
        }

        /// 删除
        public async Task<int> Deletl_Task_Infos(string ids)
        {
            try
            {
                string sql = $"DELETE FROM etl_task_info WHERE id in ('{ids}') ";
                int i = await DapperHelper.Execute(sql);
                if (i > 0)
                {
                    string[] arr = ids.Split(',');
                    foreach (var item in arr)
                    {
                        Model.etl_task_info _etl_Task_Info = lt.FirstOrDefault(x => x.Id.ToString() == item);
                        lt.Remove(_etl_Task_Info);
                    }
                    rd.SetList(lt, redisKey);
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="weight">任务权重级别</param>
        /// <param name="process_status">任务执行状态</param>
        /// <returns></returns>
        public async Task<List<Model.etl_task_info>> Getetl_Task_Infos(string name, int weight, int process_status)
        {
            lt = null;
            try

            {
                if (lt == null || lt.Count == 0)
                {
                    string sql = $"select * from etl_task_info";

                    lt = await DapperHelper.GetList<Model.etl_task_info>(sql);
                    rd.SetList(lt, redisKey);
                }
                return lt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // 简单显示
        public async Task<List<Model.etl_task_info>> Getetl_Task_Infoslist()
        {
            lt = null;
            try
            {
                if (lt == null || lt.Count == 0)
                {
                    string sql = "select * from etl_task_info ";
                   lt= await DapperHelper.GetList<Model.etl_task_info>(sql);
                    rd.SetList(lt, redisKey);
                }
                return lt;
                    
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<int> insertetl_Task_Infos(Model.etl_task_info _etl_Task_Info)
        {

            string sql = $"insert into etl_task_info values('UUID()','{_etl_Task_Info.Name}','{_etl_Task_Info.Weight}','1','0','0','0','0','0','0','0','0','0','0','0','0','10','10','0','1','user',NOW(),'user',NOW());";
            int i= await DapperHelper.Execute(sql);
            if (i>0)
            {
                lt.Add(_etl_Task_Info);
                rd.SetList(lt, redisKey);
                return i;
            }
            else
            {
                return 0;
            }
        }


    }
}
