using IOT.ETL.IRepository.sys_modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sys_modulesController : ControllerBase
    {
        private readonly IsysmodulesRepository _sysmodulesRepository;
        public sys_modulesController(IsysmodulesRepository sysmodulesRepository)
        {
            _sysmodulesRepository = sysmodulesRepository;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        [Route("/api/gotModules")]
        [HttpGet]
        public IActionResult gotModules()
        {
            var ls =_sysmodulesRepository.GetSys_Modules ();
            return Ok(new { data = ls });
        }

    }
}
