using IOT.ETL.Common;
using IOT.ETL.IRepository.ILOGIRepository;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.ILOGRepository
{
    public class ILOGRepository : ILOGIRepository
    {
        //添加
        public int AddILOG(etl_data_engine a)
        {
            string sql = $"insert into etl_data_engine values (UUID(),'{a.engine_name}','{a.engine_type_id}','{a.code_type}','{a.cl_name}',0,'高紫如',now(),'高紫如',now(),'222222')";
            return DapperHelper.Execute(sql);
        }

        //删除
        public int DelILOG(string id)
        {
            string sql = $"delete from etl_data_engine where id='{id}'";
            return DapperHelper.Execute(sql);
        }

        //显示
        public List<V_IOLG> ShowILOG()
        {
            string sql = $"select * from V_IOLG";
            return DapperHelper.GetList<V_IOLG>(sql);
        }

        //修改
        public int UptILOG(etl_data_engine a)
        {
            string sql = $"update etl_data_engine set engine_name='{a.engine_name}',engine_type_id='{a.engine_type_id}',code_type='{a.code_type}',cl_name='{a.cl_name}' where where id='{a.id}'";
            return DapperHelper.Execute(sql);
        }
    }
}
