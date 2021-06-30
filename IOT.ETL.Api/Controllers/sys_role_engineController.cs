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
            return await _sysroleengineRepository.Uptuser(a);
        }
        [Route("/api/AutAdd")]
        [HttpPost]
        public async Task<int> AutAdd([FromForm]Model.sys_role_engine m)
        {
            return await _sysroleengineRepository.Adds(m);
        }

        [HttpGet]
        [Route("/api/GetAut")]
        public async Task<IActionResult> GetAut(string id)
        {
            var result = await _sysroleengineRepository.Uptft(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("/api/Getcha")]
        public async Task<IActionResult> Getcha(string id)
                     {
            object ls = await _sysroleengineRepository.cha(id);
            if (ls == null)
            {
                return Ok(-1);
            }
            else
            {
                string ss = ls.ToString();
                string[] arr = ss.Split(',');
                return Ok(arr);
            }
        }

    }
}
