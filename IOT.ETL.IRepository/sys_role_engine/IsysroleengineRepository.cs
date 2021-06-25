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
        Task<int> UptApp(string rid,string modulesid);

        Task<string> fromMane(string rid);
    }
}
