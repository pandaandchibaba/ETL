using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    //角色表
    public class sys_role
    {
        public string id { get; set; }             //id
        public string role_name { get; set; }      //角色名称
        public int role_status { get; set; }       //角色状态 1正常0禁用
        public int revision { get; set; }          //乐观锁
        public string create_by { get; set; }      //创建人
        public DateTime create_time { get; set; }  //创建时间
        public string update_by { get; set; }      //更新人
        public DateTime update_time { get; set; }  //更新时间
    }
}
