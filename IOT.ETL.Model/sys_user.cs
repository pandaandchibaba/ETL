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
        public string id { get; set; }                   //id
        public string name { get; set; }                 //姓名
        public string email { get; set; }                //邮箱
        public string phone { get; set; }                //手机号码
        public string img_url { get; set; }              //头像
        public string username { get; set; }             //用户名
        public string password { get; set; }             //密码
        public int is_admin { get; set; }                //是否是管理员 1是0否
        public int status { get; set; }                  //状态 1正常0不正常
        public int revision { get; set; }                //乐观锁
        public string create_by { get; set; }            //创建人
        public DateTime  create_time { get; set; }       //创建时间
        public string update_by { get; set; }            //更新人
        public DateTime UPDATED_TIME { get; set; }       //更新时间
    }
}
