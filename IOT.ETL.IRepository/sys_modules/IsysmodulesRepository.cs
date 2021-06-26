using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_modules
{
   public interface IsysmodulesRepository
   {
       Task<List<IOT.ETL.Model.sys_modules>> GetSys_Modules();
   }
}
