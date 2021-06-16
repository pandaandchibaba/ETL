using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_join_info
    {
        /// <summary>
        /// 数据关联表
        /// </summary>
        public string id { get; set; }
        public string node_id { get; set; }
        public string node_name { get; set; }
        public string task_id { get; set; }
        public string left_node_id { get; set; }
        public string left_id { get; set; }
        public string join_type { get; set; }
        public string right_node_id { get; set; }
        public string right_id { get; set; }
        public string left_join_field { get; set; }
        public string right_join_field { get; set; }
        public int revision { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}
