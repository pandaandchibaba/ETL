using IOT.ETL.Common;
using IOT.ETL.IRepository.UsersIRepository;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.UsersRepository
{
    public class UsersRepository : UsersIRepository
    {
        //创建缓存关键字
        string UsersKey;
        List<Model.sys_user> list = new List<Model.sys_user>();
        RedisHelper<Model.sys_user> rh = new RedisHelper<Model.sys_user>();

        //创建视图关键字
        string VUsersKey;
        List<Model.V_user_role> Vlist = new List<Model.V_user_role>();
        RedisHelper<Model.V_user_role> Vrh = new RedisHelper<Model.V_user_role>();

        //创建登录缓存关键字
        string LoginKey;
        List<Model.sys_user> lstl = new List<Model.sys_user>();
        RedisHelper<Model.sys_user> rl = new RedisHelper<Model.sys_user>();

        public UsersRepository()
        {
            UsersKey = "Users_list";
            list = rh.GetList(UsersKey);

            LoginKey = "Login_list";
            lstl = rl.GetList(LoginKey);

            VUsersKey = "VUsers_list";
            Vlist = Vrh.GetList(VUsersKey);
        }
        
        //登录
        public async Task<int> LoginUsers(string username, string password)
        {
            string sql = $"select count(*) from sys_user where username='{username}' and password='{DESEncrypt.GetMd5Str(password)}'";
            var b = await DapperHelper.Exescalar(sql);
            int i = Convert.ToInt32(b);
            //将登录信息放入缓存
            if (i > 0)
            {
                string sqll = $"select * from sys_user where username='{username}' and password='{DESEncrypt.GetMd5Str(password)}'";
                list = await DapperHelper.GetList<Model.sys_user>(sqll);
                rh.SetList(list, LoginKey);
            }
            return i;
        }

        //注册
        public async Task<int> AddUsers(Model.sys_user a)
        {
            Model.sys_user us = list.FirstOrDefault();
            a.create_by = us.name;
            a.update_by = us.name;
            string sql = $"insert into sys_user values (uuid(),'{a.name}','{a.email}','{a.phone}','http://www.ejsedu.com/uploads/allimg/210303/101600V13_0.jpg','{a.username}','{DESEncrypt.GetMd5Str(a.password)}',1,1,0,'{a.create_by}',now(),'{a.create_by}',now())";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                var aa = await DapperHelper.GetList<Model.sys_user>("select * from sys_user order by id desc LIMIT 1");
                a = aa.FirstOrDefault();
                //存入
                list.Add(a);
                rh.SetList(list, UsersKey);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //获取所有用户信息
        public async Task<List<Model.V_user_role>> GetUsers()
        {
            try
            {
                if (Vlist == null || Vlist.Count == 0)
                {
                    Vlist = await DapperHelper.GetList<Model.V_user_role>("select * from V_user_role");
                    Vrh.SetList(Vlist, VUsersKey);
                }
                return Vlist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //邮箱修改密码
        public async Task<int> UptPwd(string email, string password)
        {
            string sql = $"update sys_user set password='{DESEncrypt.GetMd5Str(password)}' where email = '{email}'";
            int i = await DapperHelper.Execute(sql);
            return i;
        }

        //电话修改密码
        public async Task<int> UptPhone(string phone, string password)
        {
            string sql = $"update sys_user set password='{DESEncrypt.GetMd5Str(password)}' where phone = '{phone}'";
            int i = await DapperHelper.Execute(sql);
            return i;
        }
    }
}
