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
        
        [Route("/api/AutUpt")]
        [HttpPost]
        public async Task<int> AutUpt(Model.sys_role_engine a)
        {
            int i = await _sysroleengineRepository.Uptuser(a);        
            return i;
        }
        [Route("/api/AutAdd")]
        [HttpPost]
        public async Task<int> AutAdd(Model.sys_role_engine m)
        {
            int i = await _sysroleengineRepository.Adds(m);
            return i;
        }

        [HttpGet]
        [Route("/api/GetAut")]
        public async Task<IActionResult> GetAut(string id)
        {
            var result = await _sysroleengineRepository.Uptft(id);

            return Ok(result);
        }

    }
}
