using IOT.ETL.IRepository.sys_role;
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
    public class sys_roleController : ControllerBase
    {
        private readonly IsysroleRespoditory _sysroleRespoditory;
        public sys_roleController(IsysroleRespoditory sysroleRespoditory)
        {
            _sysroleRespoditory = sysroleRespoditory;
        }

        // 显示
        [Route("/api/RolesShow")]
        [HttpGet]
        public async Task<IActionResult> RolesShow(string sname="")
        {
            var ls =await _sysroleRespoditory.ShowRoles();
            if (!string.IsNullOrEmpty(sname))
            {
                ls = ls.Where(x => x.role_name.Contains(sname)).ToList();
            }
            return Ok(new { data = ls });
        }

        // 添加
        [Route("/api/AddRoles")]
        [HttpPost]
        public async Task<int> AddRoles(Model.sys_role a)
        {
            int ls = await  _sysroleRespoditory.insertRoles(a);
            return ls;
        }
        
        // 删除
        [Route("/api/DelRoles")]
        [HttpPost]
        public async Task<int> DelRoles(string id)
        {
            int ls = await _sysroleRespoditory.delRoles(id);
            return ls;
        }
        
        // 修改
        [Route("/api/UptRoles")]
        [HttpPost]
        public async Task<int> UptRoles([FromForm]Model.sys_role a)
        {
            int ls = await _sysroleRespoditory.UpdateRoles(a);
            return ls;
        }
    }
}
