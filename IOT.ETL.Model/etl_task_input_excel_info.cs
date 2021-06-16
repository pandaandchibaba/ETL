using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_input_excel_info
    {
        /// <summary>
        /// 任务数据输入excel表
        /// </summary>
        public string id { get; set; }
        public string node_id { get; set; }
        public string node_name { get; set; }
        public string task_id { get; set; }
        public string task_excel_file { get; set; }
        public string task_excel_sheet { get; set; }
        public string task_excel_id { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}
