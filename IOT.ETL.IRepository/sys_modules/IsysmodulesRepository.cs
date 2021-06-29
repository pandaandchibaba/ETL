using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_modules
{
   public interface IsysmodulesRepository
   {
        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
       Task<List<IOT.ETL.Model.sys_modules>> GetSys_Modules();
        /// <summary>
        /// 绑定类别下拉
        /// </summary>
        /// <returns></returns>
        Task <List <Dictionary<string, object>>> BindType();

    }
}
