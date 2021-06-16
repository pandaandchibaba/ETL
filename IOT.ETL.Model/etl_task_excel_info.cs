using System;

namespace IOT.ETL.Model
{
    public class etl_task_excel_info
    {
        /// <summary>
        /// 数据引擎excel表头模板表
        /// </summary>
        public string id { get; set; }
        public string name { get; set; }
        public string head_list { get; set; }
        public string filed_list { get; set; }
        public string type_list { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}
