using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.ETL.IRepository.ISys_paramIRepository;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ISys_paramIController : Controller
    {
        /// <summary>
        /// 注入
        /// </summary>
        private readonly ISys_paramIRepository _sys_ParamIRepository;
        public ISys_paramIController(ISys_paramIRepository sys_ParamIRepository)
        {
            _sys_ParamIRepository = sys_ParamIRepository;
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
        public IActionResult ShowSys_param()
        {
            //获取全部数据
            List<IOT.ETL.Model.sys_param> sys_Params = _sys_ParamIRepository.ShowSys_param();
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
            return i;
        }


    }
}
