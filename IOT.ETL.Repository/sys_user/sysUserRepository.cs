using IOT.ETL.Common;
using IOT.ETL.IRepository.sys_user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.sys_user
{
    public class sysUserRepository : IsysUserRepository
    {
        // 删除
        public async Task<int> DelUser(string id)
        {
            string sql = $"delete from sys_user where id='{id}'";
            return await DapperHelper.Execute(sql);
        }

        // 添加
        public async Task<int> insertUser(Model.sys_user a)
        {
            string sql = $"insert into sys_user values('{a.id}','{a.name}','{a.email}','{a.phone}','{a.img_url}','{a.username}','{a.password}','{a.is_admin}','{a.status}','{a.revision}','{a.create_by}','{a.create_time}','{a.update_by}','{a.UPDATED_TIME}')";
            return await DapperHelper.Execute(sql);
        }

        // 显示
        public async Task<List<Model.sys_user>> ShowUser()
        {
            string sql = "select *  from sys_user";
            return await DapperHelper.GetList<Model.sys_user>(sql);
        }

        // 修改状态
        public async Task<int> Uptstate(string id)
        {
            var aa = await DapperHelper.GetList<IOT.ETL.Model.sys_user>($"select * from sys_user where id='{id}'");
            IOT.ETL.Model.sys_user ls = aa.FirstOrDefault();
            if (ls.status == 0)
            {
                ls.status = 1;
            }
            else
            {
                ls.status = 0;
            }
            string sql = $"Update sys_user set status='{ls.status}' where id='{ls.id}'";
            return await DapperHelper.Execute(sql);
        }

        // 修改
        public async Task<int> Uptuser(Model.sys_user a)
        {
            string sql = $"Update sys_user set name='{a.name}',email='{a.email}',phone='{a.phone}',img_url='{a.img_url}',username='{a.username}',password='{a.password}',is_admin='{a.is_admin}',status='{a.status}',revision='{a.revision}',create_by='{a.create_by}',create_time='{a.create_time}',update_by='{a.update_by}',UPDATED_TIME='{a.UPDATED_TIME}'where  id='{a.id}'";
            return await DapperHelper.Execute(sql);
        }
    }
}
