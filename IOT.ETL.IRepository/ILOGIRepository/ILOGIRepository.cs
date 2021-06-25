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
        Task<int> AddILOG(IOT.ETL.Model.etl_data_engine a);

        //显示
        Task<List<IOT.ETL.Model.V_IOLG>> ShowILOG();

        //删除
        Task<int> DelILOG(string id);

        //修改
        Task<int>  UptILOG(IOT.ETL.Model.etl_data_engine a);

    }
}
