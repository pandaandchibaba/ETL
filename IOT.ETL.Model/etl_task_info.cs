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
        public string name { get; set; }//任务名称
        public int weight { get; set; }//任务权重级别 0紧急任务1高2中3低
        public int process_status { get; set; }//任务执行状态 -1待执行 0可执行 1正在执行 2执行完成
        public int status { get; set; }//任务状态 1无错误0有错误
        public int total { get; set; }//总处理条数
        public int process_total { get; set; }//已处理条数
        public decimal proportion { get; set; }//任务完成度
        public int error_total { get; set; }//错误条数
        public int execute_total { get; set; }//总需执行次数
        public int complete_execute_total { get; set; }//已完成执行次数
        public int success_insert_total { get; set; }//成功插入条数
        public int success_update_total { get; set; }//成功修改条数
        public int success_delete_total { get; set; }//成功删除条数
        public int error_add_total { get; set; }//插入失败条数
        public int error_logic_total { get; set; }//逻辑失败条数
        public int complete_time { get; set; }//预计完成时间 精确到秒
        public int grand_total_time { get; set; }//累计处理时间 精确到秒
        public string process_json { get; set; }//前端json对象
        public int revision { get; set; }//乐观锁
        public string create_by { get; set; }//创建人
        public DateTime create_time { get; set; }//创建时间
        public string update_by { get; set; }//更新人
        public DateTime update_time { get; set; }//更新时间
    }
}
