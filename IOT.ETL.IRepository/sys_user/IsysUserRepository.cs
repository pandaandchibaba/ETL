using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_user
{
   public interface IsysUserRepository
    {
        // 显示
        Task<List<IOT.ETL.Model.sys_user>> ShowUser();

        // 删除
        Task<int> DelUser(string id);

        // 修改
        Task<int> Uptuser(IOT.ETL.Model.sys_user a);

        // 添加
        Task<int> insertUser(IOT.ETL.Model.sys_user a);

        // 修改状态
        Task<int> Uptstate(string id);
    }
}
