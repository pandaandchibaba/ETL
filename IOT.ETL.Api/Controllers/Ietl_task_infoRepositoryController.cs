using IOT.ETL.IRepository.Ietl_task_info;
using IOT.ETL.Repository.etl_task_info;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using IOT.ETL.Common;

namespace IOT.ETL.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Ietl_task_infoRepositoryController : ControllerBase
    {

        ////实例化帮助文件
        RedisHelper<Model.sys_user> rl = new RedisHelper<Model.sys_user>();
        //创建登录缓存关键字
        string LoginKey;
        // 获取登录数据
        List<Model.sys_user> lstl = new List<Model.sys_user>();
        //实例化日志
        Logger logger = NLog.LogManager.GetCurrentClassLogger();
        



        //注入
        private readonly Ietl_task_infoRepository _Ietl_task_infoRepository;
        public Ietl_task_infoRepositoryController(Ietl_task_infoRepository   etl_Task_InfoRepository)
        {
            _Ietl_task_infoRepository = etl_Task_InfoRepository;
            LoginKey = "Login_list";
            lstl = rl.GetList(LoginKey);
        }
        //显示
        [Route("/api/getetl_task_infolist")]
        [HttpGet]
        public  IActionResult  getetl_task_infolist()
        {
            var ls = _Ietl_task_infoRepository.Getetl_Task_Infoslist();

            return Ok(new
            {
                msg = "",
                code = 0,
                data = ls
            });
        }
        //显示
        [Route("/api/Getetl_Task_Infos")]
        [HttpGet]
        public  IActionResult Getetl_Task_Infos(string name="",int weight=10,int process_status=10)
        {
            var ls = _Ietl_task_infoRepository.Getetl_Task_Infos(name, weight, process_status);
            if (name == "" && weight ==10  && process_status == 10)
            {
                return Ok(new
                {
                    msg = "",
                    code = 0,
                    data = ls
                });
            }
            if (!string.IsNullOrEmpty(name))
            {
                ls = ls.Where(x => x.Name.Contains(name)).ToList();
            }
            if (weight!=10)
            {
                ls = ls.Where(x => x.Weight.Equals(weight)).ToList();
            }
            if (process_status != 10)
            {
                ls = ls.Where(x => x.Process_status.Equals(process_status)).ToList();
            }
            return Ok(new
            {
                msg = "",
                code = 0,
                data = ls
            });

        }
        //删除
        [Route("/api/Deletl_Task_Infos")]
        [HttpDelete]
        public int Deletl_Task_Infos(string ids)
        {
            try
            {
                int i = _Ietl_task_infoRepository.Deletl_Task_Infos(ids);
                if (i > 0)
                {
                    Model.sys_user sys_User = lstl.FirstOrDefault();
                    logger.Debug($"这个用户{sys_User.name}，删除了一条任务名称为{ids}的数据");
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //添加
        [Route("/api/insertetl_Task_Infos")]
        [HttpPost]
        public int insertetl_Task_Infos(Model.etl_task_info _etl_Task_Info)
        {
            try
            {
                int i = _Ietl_task_infoRepository.insertetl_Task_Infos(_etl_Task_Info);
                if (i>0)
                {
                    Model.sys_user sys_User = lstl.FirstOrDefault();
                    logger.Debug($"这个用户{sys_User.name}，创建了一条任务名称为{_etl_Task_Info.Name}的数据");
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
