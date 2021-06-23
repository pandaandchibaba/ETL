using IOT.ETL.Common;
using IOT.ETL.IRepository.UsersIRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersIRepository _usersIRepository;

        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化

        public UsersController(UsersIRepository usersIRepository)
        {
            _usersIRepository = usersIRepository;
        }

        //注册
        [Route("/api/AddUsers")]
        [HttpPost]
        public int AddUsers([FromForm] IOT.ETL.Model.sys_user a)
        {
            try
            {
                //先获取所有用户的信息
                var list = _usersIRepository.GetUsers();
                //查看用户是否注册过
                var users = list.FirstOrDefault(x => x.username.Equals(a.username) || x.email.Equals(a.email));
                if (users != null)
                {
                    return -2;
                }
                var s = _usersIRepository.AddUsers(a);
                return s;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //添加用户
        [Route("/api/InsertUsers")]
        [HttpPost]
        public int InsertUsers([FromForm] IOT.ETL.Model.sys_user a)
        {
            int i = _usersIRepository.InsertUsers(a);
            return i;
        }

        //显示全部用户信息
        [Route("/api/GetUsers")]
        [HttpGet]
        public IActionResult GetUsers(string nm = "", string ph="",string nm2="")
        {
            //获取全部数据
            var ls = _usersIRepository.GetUsers();
            if (!string.IsNullOrEmpty(nm))
            {
                ls = ls.Where(x => x.name.Contains(nm)).ToList();
            }
            if (!string.IsNullOrEmpty(ph))
            {
                ls = ls.Where(x => x.phone.Contains(ph)).ToList();
            }
            if (!string.IsNullOrEmpty(nm2))
            {
                ls = ls.Where(x => x.username.Contains(nm2)).ToList();
            }
            return Ok(new
            {
                msg = "",
                code = 0,
                data = ls
            });
        }

        //登录
        [Route("/api/LoginUsers")]
        [HttpGet]
        public int LoginUsers(string username, string password)
        {
            int i =_usersIRepository.LoginUsers(username, password);
            return i;
        }

        //修改密码
        [Route("/api/UptPwd")]
        [HttpPost]
        public int UptPwd(string email, string password)
        {
            var i = _usersIRepository.UptPwd(email, password);
            return i;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/AddUseras")]
        [HttpPost]
        public int AddUseras(Model.sys_user a)
        {
            int ls = _usersIRepository.insertUser(a);
            return ls;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/api/DelUseress")]
        [HttpPost]
        public int DelUseress(string id)
        {
            int ls = _usersIRepository.DelUsers(id);
            return ls;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/UptUseras")]
        [HttpPost]
        public int UptUseras([FromForm]Model.sys_user a)
        {
            int ls = _usersIRepository.Uptuser(a);
            return ls;
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/api/Uptstatae")]
        [HttpGet]
        public int Uptstatae(string id)
        {
            int ls = _usersIRepository.Uptstate(id);
            return ls;
        }
    }
}
