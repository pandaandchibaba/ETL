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
        public Task<int> Adds(Model.sys_role_engine m)
        {
            string sql = $"insert into sys_user_role values('{m.id}','{m.role_id}','{m.modules_id}')";
            return DapperHelper.Execute(sql);
        }
        public async Task<List<Model.sys_role_engine>> Uptft(string id)
        {
            string sql = $"select * from  sys_role_engine where id={id}";
            return await DapperHelper.GetList<Model.sys_role_engine>(sql);
        }

        public async Task<int> Uptuser(Model.sys_role_engine a)
        {
            string sql = $"Update sys_role_engine set role_id='{a.role_id}',modules_id='{a.modules_id}' where id='{a.id}'";
            return await DapperHelper.Execute(sql);
        }
    }
}

