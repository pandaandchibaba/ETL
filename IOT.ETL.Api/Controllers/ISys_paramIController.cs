using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.ISys_paramIRepository;
using NLog;
using IOT.ETL.Common;
namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ISys_paramIController : Controller
    {
        //创建登录缓存关键字
        string LoginKey;
        //登录集合
        List<Model.sys_user> lstl = new List<Model.sys_user>();
        //实例化帮助文件
        RedisHelper<Model.sys_user> rl = new RedisHelper<Model.sys_user>();
        //实例化日志
        Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 注入
        /// </summary>
        private readonly ISys_paramIRepository _sys_ParamIRepository;
        public ISys_paramIController(ISys_paramIRepository sys_ParamIRepository)
        {
            _sys_ParamIRepository = sys_ParamIRepository;
            LoginKey = "Login_list";
            lstl = rl.GetList(LoginKey);
        }

        /// <summary>
        /// 显示参数字典
        /// </summary>
        /// <param name="nm1"></param>
        /// <param name="nm2"></param>
        /// <param name="nm3"></param>
        /// <returns></returns>
        [Route("/api/ShowSys_param")]
        [HttpGet]
        public IActionResult ShowSys_param(string pid=null)
        {
            //获取全部数据
            List<IOT.ETL.Model.sys_param> sys_Params = _sys_ParamIRepository.ShowSys_param(pid);
            return Ok(new
            {
                msg = "",
                code = 0,
                data = sys_Params
            });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sys_Param"></param>
        /// <returns></returns>
        [Route("/api/AddSys_param")]
        [HttpPost]
        public int AddSys_param(IOT.ETL.Model.sys_param sys_Param)
        {
            int i = _sys_ParamIRepository.AddSys_param(sys_Param);
            //判断是否添加成功  添加成功写入日志
            if (i>0)
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                //添加text文本日志
                logger.Debug($"{sys_User.name}添加了一条节点名称为{sys_Param.Name}数据");
            }
            return i;
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("/api/DelSys_param")]
        [HttpDelete]
        public int DelSys_param(string ids)
        {
            int i = _sys_ParamIRepository.DelSys_param(ids);
            //判断是否添加成功  添加成功写入日志
            if (i > 0)
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                //添加text文本日志
                logger.Debug($"{sys_User.name}删除了一条节点id为{ids}数据");
            }
            return i;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("/api/UptSys_param")]
        [HttpPut]
        public int UptSys_param(IOT.ETL.Model.sys_param sys_Param)
        {
            int i = _sys_ParamIRepository.UptSys_param(sys_Param);
            //判断是否添加成功  添加成功写入日志
            if (i > 0)
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                //添加text文本日志
                logger.Debug($"{sys_User.name}修改了一条节点名称为{sys_Param.Name}数据");
            }
            return i;
        }


    }
}
