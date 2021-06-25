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

        // 注入
        private readonly ISys_paramIRepository _sys_ParamIRepository;
        public ISys_paramIController(ISys_paramIRepository sys_ParamIRepository)
        {
            _sys_ParamIRepository = sys_ParamIRepository;
            LoginKey = "Login_list";
            lstl = rl.GetList(LoginKey);
        }

        // 绑定下拉
        [HttpGet]
        [Route("/api/Binds")]
        public async Task<IActionResult> Binds()
        {
            return Ok( await _sys_ParamIRepository.BindParent());
        }

        // 显示参数字典
        [Route("/api/ShowSys_param")]
        [HttpGet]
        public async Task<IActionResult> ShowSys_param(string pid=null)
        {
            try
            {
                //获取全部数据
                List<IOT.ETL.Model.sys_param> sys_Params = await _sys_ParamIRepository.ShowSys_param(pid);
                return Ok(new
                {
                    msg = "",
                    code = 0,
                    data = sys_Params
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        // 添加
        [Route("/api/AddSys_param")]
        [HttpPost]
        public async Task<int> AddSys_param([FromForm]IOT.ETL.Model.sys_param sys_Param)
        {
            int i = await _sys_ParamIRepository.AddSys_param(sys_Param);
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

        // 删除
        [Route("/api/DelSys_param")]
        [HttpDelete]
        public async Task<int> DelSys_param(string ids)
        {
            int i = await _sys_ParamIRepository.DelSys_param(ids);
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

        // 修改
        [Route("/api/UptSys_param")]
        [HttpPut]
        public async Task<int> UptSys_param(IOT.ETL.Model.sys_param sys_Param)
        {
            int i = await _sys_ParamIRepository.UptSys_param(sys_Param);
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
