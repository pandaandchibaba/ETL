using IOT.ETL.IRepository.sys_role_engine;
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
    public class sys_role_engineController : ControllerBase
    {
        private readonly IsysroleengineRepository _sysroleengineRepository;
        public sys_role_engineController(IsysroleengineRepository sysroleengineRepository)
        {
            _sysroleengineRepository = sysroleengineRepository;
        }
        [HttpGet]
        [Route("/api/AutUpt")]
        public int AutUpt(string rid, string modulesid)
        {
            int i = 0;
            i += _sysroleengineRepository.UptApp(rid, modulesid);
            return i;
        }

        [HttpGet]
        [Route("/api/GetAut")]
        public IActionResult GetAut(string rid)
        {
            var result = _sysroleengineRepository.fromMane(rid);

            return Ok(result);
        }

    }
}
