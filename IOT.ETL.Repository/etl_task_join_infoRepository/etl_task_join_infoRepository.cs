using IOT.ETL.IRepository.Ietl_task_join_infoRepository;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.etl_task_join_infoRepository
{
    public class etl_task_join_infoRepository : Ietl_task_join_infoRepository
    {
        public int insertetl_Task_Join_Infos(etl_task_join_info etl_Task_Join_Info)
        {
            string sql = $"insert into etl_task_join_info values('{}')";
        }
    }
}
