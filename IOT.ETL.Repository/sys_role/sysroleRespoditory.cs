using IOT.ETL.Common;
using IOT.ETL.IRepository.sys_role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.sys_role
{
    public class sysroleRespoditory : IsysroleRespoditory
    {
        // 删除
        public async Task<int> delRoles(string id)
        {
            string sql = $"delete  from sys_role where id ='{id}'";
            return await DapperHelper.Execute(sql);
        }

        // 添加
        public async Task<int> insertRoles(Model.sys_role a)
        {
            a.id = Guid.NewGuid().ToString();
            string sql = $"insert into sys_role VALUES('{a.id}','{a.role_name}','{a.role_status}','{a.revision}','{a.create_by}','{a.create_time}','{a.update_by}','{a.update_time}')";
            return await DapperHelper.Execute(sql);
        }

        // 显示
        public async Task<List<Model.sys_role>> ShowRoles()
        {
            string sql = "select * from sys_role";
            return await DapperHelper.GetList<Model.sys_role>(sql);
        }

        // 修改
        public async Task<int> UpdateRoles(Model.sys_role a)
        {
            string sql = $"Update sys_role set role_name='{a.role_name}',role_status='{a.role_status}',revision='{a.revision}',create_by='{a.create_by}',create_time='{a.create_time}',update_by='{a.update_by}',update_time='{a.update_time}' where id='{a.id}'";
            return await DapperHelper.Execute(sql);
        }
    }
}
