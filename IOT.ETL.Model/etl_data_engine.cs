using System;

namespace IOT.ETL.Model
{
    //数据规则引擎表
    public class etl_data_engine
    {
        public string id { get; set; }              //id
        public string engine_name { get; set; }     //规则名称
        public string engine_type_id { get; set; }  //规则类型id
        public string code_type { get; set; }       //代码类型
        public string cl_name { get; set; }         //类/函数名称
        public int revision { get; set; }           //乐观锁
        public int create_by { get; set; }          //更新人
        public DateTime  create_time { get; set; }        //创建时间
        public string CTime { get { return create_time.ToString("yyy-MM-dd"); } }
        public int update_by { get; set; }          //更新人
        public DateTime update_time { get; set; }        //更新时间
        public string UTime { get { return update_time.ToString("yyy-MM-dd"); } }
        public string engine_code { get; set; }        //规则引擎代码
    }
}
