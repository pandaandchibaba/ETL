using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_info
    {
        /// <summary>
        /// 任务信息表
        /// </summary>
        public string id { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
        public int process_status { get; set; }
        public int status { get; set; }
        public int total { get; set; }
        public int process_total { get; set; }
        public decimal proportion { get; set; }
        public int error_total { get; set; }
        public int execute_total { get; set; }
        public int complete_execute_total { get; set; }
        public int success_insert_total { get; set; }
        public int success_update_total { get; set; }
        public int success_delete_total { get; set; }
        public int error_add_total { get; set; }
        public int error_logic_total { get; set; }
        public int complete_time { get; set; }
        public int grand_total_time { get; set; }
        public string process_json { get; set; }
        public int revision { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}
