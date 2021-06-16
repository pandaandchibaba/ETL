using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.ILOGIRepository
{
    public interface ILOGIRepository
    {
        //添加
        int AddILOG(IOT.ETL.Model.etl_data_engine a);

        //显示
        List<IOT.ETL.Model.V_IOLG> ShowILOG();

        //删除
        int DelILOG(string id);

        //修改
        int UptILOG(IOT.ETL.Model.etl_data_engine a);

    }
}
