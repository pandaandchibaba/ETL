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
        int UptApp(string rid,string modulesid);

        string fromMane(string rid);
    }
}
