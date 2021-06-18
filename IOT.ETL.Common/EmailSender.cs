using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Common
{
    public class EmailSender 
    {
        //发送验证码方法
        public static string SendCode(string email)//收件人邮箱号
        {
            //随机验证码
            Random rm = new Random();
            int i;
            string str = string.Empty;
            for (int p = 0; p < 6; p++)
            {
                i = Convert.ToInt32(rm.NextDouble() * 10);
                str += i;
            }
            string content = "谦讯科技提醒您：您正在使用邮箱安全验证服务，您本次操作的验证码是：" + str + ";有效期限为5分钟,请您正在5分钟内完成重置密码功能";
            //调用发送邮箱方法
            SendEmail1(email, "【谦讯科技】后台登录修改用户信息提示", content);//收件人邮箱，邮箱标题，邮箱内容 

            //返回验证码
            return str;
        }

        /// <summary>
        /// 发送邮箱
        /// </summary>
        /// <param name="mailTo">收件人</param>
        /// <param name="mailSubject">标题</param>
        /// <param name="mailContent">内容</param>
        /// <returns></returns>
        public static void SendEmail1(string mailTo, string mailSubject, string mailContent)
        {
            SmtpClient mailClient = new SmtpClient("smtp.qq.com");
            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;
            //Credentials登陆SMTP服务器的身份验证.
            mailClient.Credentials = new NetworkCredential("1652006360@qq.com", "jrylkxhrvirdeaea");//邮箱，
            MailMessage message = new MailMessage(new MailAddress("1652006360@qq.com"), new MailAddress(mailTo));//发件人，收件人
            message.IsBodyHtml = true;
            // message.Bcc.Add(new MailAddress("tst@qq.com")); //可以添加多个收件人
            message.Body = mailContent;//邮件内容
            message.Subject = mailSubject;//邮件主题
                                          //Attachment 附件
                                          //Attachment att = new Attachment(@"C:/hello.txt");
                                          //message.Attachments.Add(att);//添加附件
                                          //Console.WriteLine("Start Send Mail....");
                                          //发送....
            mailClient.Send(message); // 发送邮件


        }
    }
}
