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
        public string node_id { get; set; }//节点ID
        public string node_name { get; set; }//节点名称
        public string task_id { get; set; }//任务ID
        public string task_excel_file { get; set; }//excel文件存储位置
        public string task_excel_sheet { get; set; }//excel文件读取sheet页
        public string task_excel_id { get; set; }//excel表头信息
        public string create_by { get; set; }//创建人
        public DateTime create_time { get; set; }//创建时间
        public string update_by { get; set; }//更新人
        public DateTime update_time { get; set; }//更新时间
    }
}
