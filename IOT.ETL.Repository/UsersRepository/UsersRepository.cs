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

        //创建视图关键字
        string VUsersKey;

        //获取全部数据
        List<Model.sys_user> list = new List<Model.sys_user>();
        List<Model.V_user_role> Vlist = new List<Model.V_user_role>();

        RedisHelper<Model.sys_user> rh = new RedisHelper<Model.sys_user>();
        RedisHelper<Model.V_user_role> Vrh = new RedisHelper<Model.V_user_role>();

        public UsersRepository()
        {
            UsersKey = "Users_list";
            VUsersKey = "VUsers_list";
        }
        
        //登录
        public int LoginUsers(string username, string password)
        {
            string sql = $"select count(*) from sys_user where username='{username}' and password='{DESEncrypt.GetMd5Str(password)}'";
            var b = DapperHelper.Exescalar(sql);
            int i = Convert.ToInt32(b);
            //将登录信息放入缓存
            if (i > 0)
            {
                string sqll = $"select *from sys_user where username='{username}' and password='{password}'";
                lstl = DapperHelper.GetList<Model.sys_user>(sqll);
                rl.SetList(lstl, LoginKey);
            }
            return i;
        }

        //注册
        public int AddUsers(Model.sys_user a)
        {
            string sql = $"insert into sys_user values (uuid(),'{a.name}','{a.email}','{a.phone}','http://www.ejsedu.com/uploads/allimg/210303/101600V13_0.jpg','{a.username}','{DESEncrypt.GetMd5Str(a.password)}',1,1,0,'高紫如',now(),'高紫如',now())";
            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                a = DapperHelper.GetList<Model.sys_user>("select * from sys_user order by id desc LIMIT 1").FirstOrDefault();
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
        public List<Model.V_user_role> GetUsers()
        {
            try
            {
                if (Vlist == null || Vlist.Count == 0)
                {
                    Vlist = DapperHelper.GetList<Model.V_user_role>("select * from V_user_role");
                    Vrh.SetList(Vlist, VUsersKey);
                }
                return Vlist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //修改密码
        public int UptPwd(string email, string password)
        {
            string sql = $"update sys_user set password='{DESEncrypt.GetMd5Str(password)}' where email = '{email}'";
            int i =  DapperHelper.Execute(sql);
            return i;
        }

        //删除
        public int DelUsers(string id)
        {
            string sql = $"delete from sys_user where id='{id}'";
            return DapperHelper.Execute(sql);
        }

        //添加用户
        public int InsertUsers(Model.sys_user a)
        {
            string sql = $"insert into sys_user values (uuid(),'{a.name}','1111','{a.phone}','http://www.ejsedu.com/uploads/allimg/210303/101600V13_0.jpg','{a.username}','{DESEncrypt.GetMd5Str(a.password)}',{a.is_admin},0,0,'高紫如',now(),'高紫如',now())";
            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                a = DapperHelper.GetList<Model.sys_user>("select * from sys_user order by id desc LIMIT 1").FirstOrDefault();
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
    }
}
