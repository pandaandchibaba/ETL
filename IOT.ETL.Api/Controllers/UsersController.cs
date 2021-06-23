using IOT.ETL.Common;
using IOT.ETL.IRepository.UsersIRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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

        public UsersController(UsersIRepository usersIRepository,RedisHelper1 helper1)
        {
            _usersIRepository = usersIRepository;
            _redis = helper1.GetDatabase();
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

        //邮箱修改密码
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

        //验证邮箱是否已经注册,若没有怎发送邮箱验证码
        [HttpGet]
        [Route("/api/TestEmail")]
        public int TestEmail(string email)
        {
            try
            {
                //获取所有数据  查询邮箱所在位置
                var ss = _usersIRepository.GetUsers().FirstOrDefault(x => x.email.Equals(email));
                //判断是否为空
                if (ss == null)
                {
                    return -1;//说明这个邮箱没有被注册
                }
                //调用发送验证码帮助类,并返回当前验证码
                string code = EmailSender.SendCode(ss.email);

                logger.Debug($"邮箱号：{email}，在  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   申请验证码：" + code);

                //发送验证码当前时间
                var time = DateTime.Now;

                //存储发送验证码的时间 
                _redis.StringSet("sendTime",time.ToString());

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
                string getcodeEmail = _redis.StringGet("getcodeEmail");
                //验证码
                string code = _redis.StringGet("code");
                //发送时间
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
                if (mins > 1)
                {
                    return -3;
                }
                //验证码1分钟过期，判断是否超过
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //电话修改密码
        [Route("/api/UptPhone")]
        [HttpPost]
        public int UptPhonw(string phone, string password)
        {
            try
            {
                var i = _usersIRepository.UptPhone(phone, password);
                logger.Debug($"手机号：{phone}，在" + DateTime.Now + "修改密码成功");
                //返回
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("/api/CallPhone")]
        public int CallPhone(string phone)
        {
            string PostUrl = "http://106.ihuyi.com/webservice/sms.php?method=Submit";
            //登录“互亿无线网站”查看用户名 登录用户中心->验证码通知短信>产品总览->API接口信息->APIID
            string account = "C52108202";
            //登录“互亿无线网站”查看密码 登录用户中心->验证码通知短信>产品总览->API接口信息->APIKEY
            string password = "2343aa16831985b4e2781bbc3b8346ac";
            //接收短信的用户的手机号码
            string mobile = phone;
            //随机生成四位数 可以模仿向用户发送验证码
            Random rad = new Random();
            int code = rad.Next(1000, 10000);   //生成随机数
            string content = "您的验证码是：" + code + " 。请不要把验证码泄露给其他人。";

            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";  //用户名+密码+注册的手机号+验证码

            UTF8Encoding encoding = new UTF8Encoding();  //万国码
            //将 account, password, mobile, content 这四个内容添加到postStrTpl字符串当中
            //并利用encoding.GetBytes()将括号里面的字符串转化为二进制类型
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, account, password, mobile, content)); //将字符串postStrTpl中的格式项替换为四个个指定的 Object 实例的值的文本等效项。再转为二进制数据

            //新建一个请求对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(PostUrl);//对统一资源标识符 (URI) 发出请求。 这是一个 abstract 类。
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postData.Length;

            Stream newStream = myRequest.GetRequestStream();
            //间postData合并到 PostUrl中去
            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();


            //以http://106.ihuyi.com/webservice/sms.php?method=Submit&account=你的APIID&password=你的APIKEY&mobile=接收短信的用户的手机号码&content=您的验证码是：" + mobile_code + " 。请不要把验证码泄露给其他人。"    发起https请求   并获取请求结果
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                logger.Debug($"手机号：{phone}，在  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   申请验证码：" + code);
                // 发送验证码当前时间
                var time = DateTime.Now;

                //存储发送验证码的时间 
                _redis.StringSet("sendTime", time.ToString());

                //存储验证码
                _redis.StringSet("code", code);

                //用来存储获取验证码时的邮箱号 
                _redis.StringSet("getcodephone", phone);
                return 1;
            }
            else
            {
                return 0;
                //访问失败
            }
        }

        //验证验证码
        [HttpGet]
        [Route("/api/TestCodePhone")]
        public int TestCodePhone(string phone, string fcode)
        {
            try
            {
                //邮箱
                string getcodephone = _redis.StringGet("getcodephone");
                //验证码
                string code = _redis.StringGet("code");
                //发送时间
                string sendTime = _redis.StringGet("sendTime");
                //判断
                if (phone != getcodephone)
                {

                    return -1;//此时手机号发生了改变 
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
                if (mins > 1)
                {
                    return -3;
                }
                //验证码1分钟过期，判断是否超过
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
