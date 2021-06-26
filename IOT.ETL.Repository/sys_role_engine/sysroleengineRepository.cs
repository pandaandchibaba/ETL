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
        public async Task<string> fromMane(string rid)
        {
            string sql = $@"";
            var query = await DapperHelper.GetList<IOT.ETL.Model.sys_modules>(sql);
            StringBuilder sb = new StringBuilder();
            foreach (var s in query)
            {
                sb.Append(s.id).Append(',');
            }
            var arr = "["+sb.ToString().TrimEnd(',')+"]";
            return arr;
        }
         
        public async Task<int> UptApp(string  rid, string modulesid)
        {
            int i = 0;
            //先删除此角色下边的所有权限
            string sql = $"delete from sys_role_engine where id='{rid}'";
            await DapperHelper.Execute(sql);
            string[] arr = modulesid.Split(',');
            foreach (var s in arr)
            {
                string str = $"insert into sys_role_engine(rid,modules_id)values('{rid}',{s})";
                i += await DapperHelper.Execute(str);
            }
            return i;
        }
    }
}

