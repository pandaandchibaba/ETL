﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_order_info
    {
        /// <summary>
        /// 任务处理排序表
        /// </summary>
        public string id { get; set; }
        public string node_id { get; set; }
        public string node_name { get; set; }
        public string task_id { get; set; }
        public string select_node_id { get; set; }
        public string select_table_name { get; set; }
        public string select_table_field { get; set; }
        public string order_sort { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
      
    }
}
