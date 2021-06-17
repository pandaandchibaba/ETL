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
        int AddSys_param(IOT.ETL.Model.sys_param sys_Param);

        //显示
        List<IOT.ETL.Model.sys_param> ShowSys_param();

        //删除
        int DelSys_param(string ids);

        //修改
        int UptSys_param(IOT.ETL.Model.sys_param sys_Param);
    }
}
