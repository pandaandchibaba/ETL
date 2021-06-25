using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_role
{
   public interface IsysroleRespoditory
    {
        // 显示
        Task<List<IOT.ETL.Model.sys_role>> ShowRoles();

        // 添加
        Task<int> insertRoles(IOT.ETL.Model.sys_role a);

        // 修改
        Task<int> UpdateRoles(IOT.ETL.Model.sys_role a);

        // 删除
        Task<int> delRoles(string id);
    }
}
