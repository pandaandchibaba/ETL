using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{

    /// <summary>
    /// 用户表
    /// </summary>
   public class sys_user
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string img_url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int is_admin { get; set; }
        public int status { get; set; }
        public int revision { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime UPDATED_TIME { get; set; }
    }
}
