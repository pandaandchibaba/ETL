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

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        [Route("/api/UserShow")]
        [HttpGet]
        public IActionResult UserShow(string sname = "", string yhm = "", string phones = "")
        {
            var ls = _serviceProvider.ShowUser();
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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/AddUser")]
        [HttpPost]
        public int AddUser(Model.sys_user a)
        {
            int ls = _serviceProvider.insertUser(a);
            return ls;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/api/DelUsers")]
        [HttpPost]
        public int DelUsers(string id)
        {
            int ls = _serviceProvider.DelUser(id);
            return ls;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/UptUsers")]
        [HttpPost]
        public int UptUsers(Model.sys_user a)
        {
            int ls = _serviceProvider.Uptuser(a);
            return ls;
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/Uptstate")]
        [HttpPost]
        public int Uptstate(string id)
        {
            int ls = _serviceProvider.Uptstate(id);
            return ls;
        }

    }
}
