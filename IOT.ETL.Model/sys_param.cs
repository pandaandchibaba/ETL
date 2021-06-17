using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    /// <summary>
    /// 参数字典表
    /// </summary>
    public class sys_param
    {
        //主键id
        public string Id { get; set; }
        //编码
        public string Code { get; set; }
        //名称
        public string Name { get; set; }
        //父级id
        public string Pid { get; set; }
        //1 默认使用  0 不默认使用
        public int Default_status { get; set; }
        //1是系统属性，不可删除，0是自定义属性可以删除
        public int Is_system { get; set; }
        //1可用 0不可用
        public int Is_del { get; set; }
        //排序
        public int Order_by { get; set; }
        //解释、备注
        public string Text { get; set; }
        //创建人
        public string Create_by { get; set; }
        //创建时间
        public DateTime Create_time { get; set; }
        //显示创建时间
        public string Create_times { get { return Create_time.ToString("yyyy-MM-dd HH:mm:ss");} }
        //修改人 （默认为创建人）
        public string Update_by { get; set; }
        //修改时间 （默认为创建时间）
        public DateTime Update_time { get; set; }
        //显示修改时间
        public string Update_times { get { return Update_time.ToString("yyyy-MM-dd HH:mm:ss"); } }
        //父级名称
        public string fname { get; set; }
    }
}
