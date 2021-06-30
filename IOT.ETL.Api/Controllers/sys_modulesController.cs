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

        // 显示
        [Route("/api/gotModules")]
        [HttpGet]
        public async Task<IActionResult> gotModules()
        {
            var ls = await _sysmodulesRepository.GetSys_Modules();
            return Ok(new { data = ls });
        }

        [Route("/api/Bindtypes")]
        [HttpGet]
        public async Task<IActionResult> Bindtypes()
        {
            List<Dictionary<string, object>> lst =await _sysmodulesRepository.BindType();
            return Ok(lst);
        }

    }
}
