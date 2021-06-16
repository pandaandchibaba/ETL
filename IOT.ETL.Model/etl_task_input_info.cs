using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_input_info
    {
        /// <summary>
        /// 任务数据输入表
        /// </summary>
        public string id { get; set; }
        public string node_id { get; set; }
        public string node_name { get; set; }
        public string task_id { get; set; }
        public string database_id { get; set; }
        public string database_name { get; set; }
        public string table_name { get; set; }
        public string table_as_name { get; set; }
        public string table_field { get; set; }
        public string table_as_field { get; set; }
        public string field_as_egine_id { get; set; }
        public int revision { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}
