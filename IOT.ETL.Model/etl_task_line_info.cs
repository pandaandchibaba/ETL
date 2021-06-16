using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_line_info
    {
        /// <summary>
        /// 处理信息连接表
        /// </summary>
        public string id { get; set; }
        public string task_id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int execute_order { get; set; }
    }
}
