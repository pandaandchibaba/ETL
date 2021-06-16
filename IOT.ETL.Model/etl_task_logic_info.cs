using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_logic_info
    {
        /// <summary>
        /// 任务处理过程表
        /// </summary>
        public string id { get; set; }
        public string node_id { get; set; }
        public string node_name { get; set; }
        public string task_id { get; set; }
        public string select_node_id { get; set; }
        public string select_node_table { get; set; }
        public string select_node_field { get; set; }
        public string annal_engine { get; set; }
        public string field_engine { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}
