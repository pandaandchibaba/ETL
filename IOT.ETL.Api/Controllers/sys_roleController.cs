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

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        [Route("/api/RolesShow")]
        [HttpGet]
        public IActionResult RolesShow(string sname="")
        {
            var ls =_sysroleRespoditory.ShowRoles();
            if (!string.IsNullOrEmpty(sname))
            {
                ls = ls.Where(x => x.role_name.Contains(sname)).ToList();
            }
            return Ok(new { data = ls });
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/AddRoles")]
        [HttpPost]
        public int AddRoles(Model.sys_role a)
        {
            int ls = _sysroleRespoditory.insertRoles(a);
            return ls;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/api/DelRoles")]
        [HttpPost]
        public int DelRoles(string id)
        {
            int ls = _sysroleRespoditory.delRoles(id);
            return ls;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/UptRoles")]
        [HttpPost]
        public int UptRoles([FromForm]Model.sys_role a)
        {
            int ls =_sysroleRespoditory.UpdateRoles(a);
            return ls;
        }
    }
}
