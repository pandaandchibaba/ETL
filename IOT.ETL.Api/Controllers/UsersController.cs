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
        private readonly IDatabase _redis;

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
        public IActionResult LoginUsers(string username, string password)
        {
            try
            {
                var s = _usersIRepository.LoginUsers(username, password);
                if (s > 0)
                {
                    logger.Debug($"用户：{username}，在" + DateTime.Now + "登录成功");
                    return Ok(s);
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
                throw;
            }
            return Ok(-1);
        }

        //修改密码
        [Route("/api/UptPwd")]
        [HttpPost]
        public int UptPwd(string email, string password)
        {
            try
            {
                //判断该邮箱是否经历过验证
                string getcodeEmail = _redis.StringGet("getcodeEmail");
                if (getcodeEmail != email)
                {
                    return -1;
                }
                var i = _usersIRepository.UptPwd(email, password);
                logger.Debug($"邮箱号：{email}，在" + DateTime.Now + "修改密码成功");
                //返回
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // 验证邮箱是否已经注册,若没有怎发送邮箱验证码
        [HttpGet]
        [Route("/api/TestEmail")]
        public int TestEmail(string email)
        {
            try
            {
                //获取所有数据
                var list = _usersIRepository.GetUsers();
                var ss = list.FirstOrDefault(x => x.email.Equals(email));
                //判断是否为空
                if (ss == null)
                {
                    return -1;//说明这个邮箱没有被注册
                }

                //调用发送验证码帮助类,并返回当前验证码
                string code = EmailSender.SendCode(ss.email);
                logger.Debug($"邮箱号：{email}，在" + DateTime.Now + "申请验证码");
                //发送验证码当前时间
                var time = DateTime.Now;
                //存储发送验证码的时间 
                _redis.StringSet("sendTime", time.ToString());
                //存储验证码
                _redis.StringSet("code", code);
                //用来存储获取验证码时的邮箱号 
                _redis.StringSet("getcodeEmail", email);
                return 1;//发送成功
            }
            catch (Exception)
            {

                throw;
            }
        }


        //验证验证码
        [HttpGet]
        [Route("/api/TestCode")]
        public int TestCode(string email, string fcode)
        {
            try
            {
                //邮箱
                //string getcodeEmail= HttpContext.Session.GetString("getcodeEmail");
                string getcodeEmail = _redis.StringGet("getcodeEmail");
                //验证码
                //string code= HttpContext.Session.GetString("code");
                string code = _redis.StringGet("code");
                //发送时间
                //string sendTime = HttpContext.Session.GetString("sendTime");
                string sendTime = _redis.StringGet("sendTime");
                //判断
                if (email != getcodeEmail)
                {

                    return -1;//此时邮箱号发生了改变 
                }
                if (code != fcode)
                {
                    //说明验证码不正确
                    return -2;
                }
                //判断验证码是否过期
                //获取当前时间
                var Ntime = DateTime.Now;
                var mins = (Ntime - Convert.ToDateTime(sendTime)).TotalMinutes;
                if (mins > 5)
                {
                    return -3;
                }
                //验证码5分钟过期，判断是否超过
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
