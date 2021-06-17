using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Common;
using IOT.ETL.IRepository.ISys_paramIRepository;
using IOT.ETL.Model;

namespace IOT.ETL.Repository.ISys_paramRepository
{
    public class ISys_paramRepository : ISys_paramIRepository
    {
        //添加
        public int AddSys_param(sys_param sys_Param)
        {
            string sql = $"INSERT INTO sys_param VALUES(UUID(),'{sys_Param.Code}','{sys_Param.Name}',{sys_Param.Pid},{sys_Param.Default_status},{sys_Param.Is_system},{sys_Param.Is_del},{sys_Param.Order_by},'{sys_Param.Text}','{sys_Param.Create_by}',NOW(),'{sys_Param.Update_by}',NOW())";
            return DapperHelper.Execute(sql);

        }
        //删除
        public int DelSys_param(string id)
        {
            throw new NotImplementedException();
        }
        //修改
        public int UptSys_param(sys_param sys_Param)
        {
            throw new NotImplementedException();
        }
        //显示
        List<sys_param> ISys_paramIRepository.ShowSys_param()
        {
            //按order_by排序
            string sql = "SELECT *FROM sys_param order by order_by";
            return DapperHelper.GetList<IOT.ETL.Model.sys_param>(sql);
        }
    }
}
