﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.Ietl_task_join_info
{
   public  interface Ietl_task_join_infoRepository
    {
        Task<int> AddInputA(IOT.ETL.Model.etl_task_input_info ta);
        Task<int> AddInputB(IOT.ETL.Model.etl_task_input_info ta);
        Task<int> AddJoin(IOT.ETL.Model.etl_task_join_info jo);
        Task<List<IOT.ETL.Model.etl_task_VModel>> ShowField(string database_name);
        Task<List<IOT.ETL.Model.etl_task_input_info>>  FanFieldIDAsync(string id);
        Task<List<IOT.ETL.Model.etl_task_VModel>>  SelectBind(string database_name, string id);                                 
        Task<List<IOT.ETL.Model.etl_task_VModel>>   OutputAllField(string id);
        Task<int> ExecuteAllSql(string id, string name);
    }
}
