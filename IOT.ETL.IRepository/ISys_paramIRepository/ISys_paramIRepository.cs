using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.ISys_paramIRepository
{
    public interface ISys_paramIRepository
    {
        //添加
        Task<int> AddSys_param(IOT.ETL.Model.sys_param sys_Param);

        //显示
        Task<List<IOT.ETL.Model.sys_param>> ShowSys_param(string pid);
        //删除
        Task<int> DelSys_param(string ids);

        //修改
        Task<int> UptSys_param(IOT.ETL.Model.sys_param sys_Param);

        //绑定类别
        Task<List<Dictionary<string, object>>> BindParent();
    }
}
