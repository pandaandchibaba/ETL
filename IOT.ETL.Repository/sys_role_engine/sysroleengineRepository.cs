using IOT.ETL.Common;
using IOT.ETL.IRepository.sys_role_engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;

namespace IOT.ETL.Repository.sys_role_engine
{
    public class sysroleengineRepository : IsysroleengineRepository
    {
        RedisHelper<Model.sys_role_engine> rm = new RedisHelper<Model.sys_role_engine>();
        string redisKey;
        List<Model.sys_role_engine> rms = new List<Model.sys_role_engine>();

        public sysroleengineRepository()
        {
            redisKey = "role_engine";
            rms = rm.GetList(redisKey);
        }
        public async Task<int> Adds(Model.sys_role_engine m)
        {
            m.id = Guid.NewGuid().ToString();
            string sql = $"insert into sys_role_engine values('{m.id}','{m.role_id}','{m.modules_id}')";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                var hh = await DapperHelper.GetList<Model.sys_role_engine>("select * from sys_role_engine order by id desc LIMIT 1");
                m = hh.FirstOrDefault();
                List<Model.sys_role_engine> ss = new List<Model.sys_role_engine>();
                ss.Add(m);
                rms = ss;
                rm.SetList(rms,redisKey);
            }
            return i;
        }

        public async Task<object> cha(string id)
        {
            string ss = $"select * from sys_role_engine";
            rms = await DapperHelper.GetList<Model.sys_role_engine>(ss);
            rm.SetList(rms, redisKey);
            string sql = $"select modules_id from sys_role_engine where role_id='{id}'";
            object aa =await DapperHelper.Exescalar(sql);
            return aa;
        }

        public async Task<List<Model.sys_role_engine>> Uptft(string id)
        {
            string sql = $"select * from  sys_role_engine where id='{id}'";
            return await DapperHelper.GetList<Model.sys_role_engine>(sql);
        }

        public async Task<int> Uptuser(Model.sys_role_engine a)
        {
            a.id = Guid.NewGuid().ToString();
            string sql = $"Update sys_role_engine set role_id='{a.role_id}',modules_id='{a.modules_id}' where id='{a.id}'";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                Model.sys_role_engine mm = rms.FirstOrDefault(x => x.role_id.Equals(a.role_id));
                rm.SetList(rms, redisKey);
            }
            return i;
        }
    }
}

