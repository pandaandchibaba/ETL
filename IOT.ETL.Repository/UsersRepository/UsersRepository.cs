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
        //获取全部数据
        List<Model.sys_user> list = new List<Model.sys_user>();

        RedisHelper<Model.sys_user> rh = new RedisHelper<Model.sys_user>();
        public UsersRepository()
        {
            UsersKey = "Users_list"; ;
        }

        //登录
        public int LoginUsers(string username, string password)
        {
            string sql = $"select count(*) from sys_user where username='{username}' and password='{password}'";
            var b = DapperHelper.Exescalar(sql);
            int i = Convert.ToInt32(b);
            return i;
        }

        //注册
        public int AddUsers(Model.sys_user a)
        {
            string sql = $"insert into sys_user values (uuid(),'{a.name}','{a.email}','{a.phone}','http://www.ejsedu.com/uploads/allimg/210303/101600V13_0.jpg','{a.username}','{a.password}',1,1,0,'高紫如',now(),'高紫如',now())";
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
        public List<Model.sys_user> GetUsers()
        {
            try
            {
                if (list == null || list.Count == 0)
                {
                    list = DapperHelper.GetList<Model.sys_user>("select * from sys_user");
                    rh.SetList(list, UsersKey);
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        //修改密码
        public int UptPwd(string email, string password)
        {
            string sql = $"update sys_user set password='{password}' where email = '{email}'";
            return DapperHelper.Execute(sql);
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
            string sql = $"insert into sys_user values (uuid(),'{a.name}','1111','{a.phone}','http://www.ejsedu.com/uploads/allimg/210303/101600V13_0.jpg','{a.username}','{a.password}',{a.is_admin},0,0,'高紫如',now(),'高紫如',now())";
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
