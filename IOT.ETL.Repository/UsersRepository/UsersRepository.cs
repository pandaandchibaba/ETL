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
        //创建登录缓存关键字
        string LoginKey;
        //获取全部数据
        List<Model.sys_user> list = new List<Model.sys_user>();
        //登录集合
        List<Model.sys_user> lstl = new List<Model.sys_user>();
        RedisHelper<Model.sys_user> rl = new RedisHelper<Model.sys_user>();
        RedisHelper<Model.sys_user> rh = new RedisHelper<Model.sys_user>();
        public UsersRepository()
        {
            UsersKey = "Users_list";
            list = rh.GetList(UsersKey);
            LoginKey = "Login_list";
            lstl = rl.GetList(LoginKey);
        }
        
        //登录
        public int LoginUsers(string username, string password)
        {
            string sql = $"select count(*) from sys_user where username='{username}' and password='{password}'";
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
            string sql = $"insert into sys_user values (uuid(),'{a.name}','{a.email}','{a.phone}','http://www.ejsedu.com/uploads/allimg/210303/101600V13_0.jpg','{a.username}','{a.password}',1,1,0,'{a.name}',now(),'{a.name}',now())";
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
            int i= DapperHelper.Execute(sql);
            if (i > 0)
            {
                Model.sys_user sys_User = list.FirstOrDefault(x => x.id.ToString() == id);
                list.Remove(sys_User);
                //从新存入
                rh.SetList(list, UsersKey);

            }
            return i;
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
        //修改
        public int Uptuser(Model.sys_user a)
        {
            string sql = $"Update sys_user set name='{a.name}',email='{a.email}',phone='{a.phone}',img_url='{a.img_url}',username='{a.username}',password='{a.password}',is_admin='{a.is_admin}',status='{a.status}',revision='{a.revision}',create_by='{a.create_by}',create_time='{a.create_time}',update_by='{a.update_by}',UPDATED_TIME='{a.UPDATED_TIME}'where  id='{a.id}'";
            int i= DapperHelper.Execute(sql);
            if (i > 0)
            {
                list[list.IndexOf(list.Find(x => x.id == a.id))] = a;
                rh.SetList(list, UsersKey);
               
            }
            return i;
        }
        //添加
        public int insertUser(Model.sys_user a)
        {
            a.id = Guid.NewGuid().ToString();
            string sql = $"insert into sys_user values('{a.id}','{a.name}','{a.email}','{a.phone}','{a.img_url}','{a.username}','{a.password}','{a.is_admin}','{a.status}','{a.revision}','{a.create_by}','{a.create_time}','{a.update_by}','{a.UPDATED_TIME}')";
            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                //a = DapperHelper.GetList<Model.sys_user>("select * from sys_user order by id desc LIMIT 1").FirstOrDefault();
                //存入
                list.Add(a);
                rh.SetList(list, UsersKey);
            }
            return i;
        }
        //修改状态
        public int Uptstate(string id)
        {
            IOT.ETL.Model.sys_user ls = list.First(x => x.id == id);
            if (ls.status == 0)
            {
                ls.status = 1;
            }
            else
            {
                ls.status = 0;
            }
            string sql = $"Update sys_user set status='{ls.status}' where id='{ls.id}'";
            int i= DapperHelper.Execute(sql);
            if (i > 0)
            {
                rh.SetList(list,UsersKey);
               
            }
            return i;



        }
    }
}
