using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.Ietl_task_info
{
    public  interface Ietl_task_infoRepository
    {
        //显示方法(szh)
        List<Model.etl_task_info>  Getetl_Task_Infoslist();

        //显示查询(szh)
        List<Model.etl_task_info> Getetl_Task_Infos(string name,int weight,int process_status);
        //删除(szh)
        int Deletl_Task_Infos(string ids);
        //添加名称和任务权重(szh)
        int insertetl_Task_Infos(Model.etl_task_info _etl_Task_Info);

    }
}
