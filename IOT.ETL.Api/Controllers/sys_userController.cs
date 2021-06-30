using IOT.ETL.IRepository.sys_user;
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
    public class sys_userController : ControllerBase
    {
        private readonly IsysUserRepository _serviceProvider;
        public sys_userController(IsysUserRepository serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // 显示
        [Route("/api/UserShow")]
        [HttpGet]
        public async Task<IActionResult> UserShow(string sname = "", string yhm = "", string phones = "")
        {
            var ls = await _serviceProvider.ShowUser();
            if (!string.IsNullOrEmpty(sname))
            {
                ls = ls.Where(x => x.name.Contains(sname)).ToList();
            }
            if (!string.IsNullOrEmpty(yhm))
            {
                ls = ls.Where(x => x.username.Contains(yhm)).ToList();
            }
            if (!string.IsNullOrEmpty(phones))
            {
                ls = ls.Where(x => x.phone.Contains(phones)).ToList();
            }
            return Ok(new { data = ls });
        }
       
        // 添加
        [Route("/api/AddUser")]
        [HttpPost]
        public async Task<int> AddUser(Model.sys_user a)
        {
            int ls = await _serviceProvider.insertUser(a);
            return ls;
        }
      
        // 删除
        [Route("/api/DelUsers")]
        [HttpPost]
        public async Task<int> DelUsers(string id)
        {
            int ls = await _serviceProvider.DelUser(id);
            return ls;
        }
     
        // 修改
        [Route("/api/UptUsers")]
        [HttpPost]
        public async Task<int> UptUsers([FromForm]Model.sys_user a)
        {
            int ls = await _serviceProvider.Uptuser(a);
            return ls;
        }
     
        // 修改状态
        [Route("/api/Uptstate")]
        [HttpPost]
        public async Task<int> Uptstate(string id)
        {
            int ls = await  _serviceProvider.Uptstate(id);
            return ls;
        }

    }
}
