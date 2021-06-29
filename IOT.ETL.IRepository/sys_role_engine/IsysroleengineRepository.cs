using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_role_engine
{
   public interface IsysroleengineRepository
    {
        //权限
        Task<int> Uptuser(Model.sys_role_engine a);
        Task<List<Model.sys_role_engine>> Uptft(string id);
        Task<int> Adds(Model.sys_role_engine m);
        
    }
}
