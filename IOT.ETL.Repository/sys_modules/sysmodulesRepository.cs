using IOT.ETL.Common;
using IOT.ETL.IRepository.sys_modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.sys_modules
{
    public class sysmodulesRepository : IsysmodulesRepository
    {
        public async Task<List<Model.sys_modules>> GetSys_Modules()
        {
            string sql = "select * from  sys_modules";
            return await DapperHelper.GetList<Model.sys_modules>(sql);
        }
    }
}
