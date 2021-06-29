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

        public async Task<List<Dictionary<string,object>>> BindType()
        {
            string sql = "select * from sys_modules";
            //获取所有的分类
            List<Model.sys_modules> lst =await DapperHelper.GetList<Model.sys_modules>(sql);
            List<Dictionary<string, object>> result = await Recursion(lst,"0");
            return result;
        }
        /// <summary>
        /// 绑定分类
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, object>>> Recursion(List<Model.sys_modules> lst, string pid)
        {
            List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
            List<Model.sys_modules> sys_s = lst.Where(x=>x.p_id==pid).ToList();

            foreach (var item in sys_s)
            {
                Dictionary<string, object> jsonSub = new Dictionary<string, object>();
                jsonSub.Add("value", item.id);
                jsonSub.Add("label", item.name);
                if (lst.Count(x => x.p_id == item.id) > 0)
                {
                    jsonSub.Add("children", Recursion(lst,item.id));
                }
                json.Add(jsonSub);
            }
            return json;
        }

        public async Task<List<Model.sys_modules>> GetSys_Modules()
        {
            string sql = "select * from  sys_modules";
            return await DapperHelper.GetList<Model.sys_modules>(sql);
        }
    }
}
