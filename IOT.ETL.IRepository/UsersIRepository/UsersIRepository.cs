using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.UsersIRepository
{
    public interface UsersIRepository
    {
        //注册用户
        int AddUsers(Model.sys_user a);

        //添加用户
        int InsertUsers(Model.sys_user a);

        //登录
        int LoginUsers(string username, string password);

        //获取所有用户信息
        List<Model.sys_user> GetUsers();

        //重置密码
        int UptPwd(string email, string password);



        //删除
        int DelUsers(string id);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        int Uptuser(IOT.ETL.Model.sys_user a);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        int insertUser(IOT.ETL.Model.sys_user a);
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Uptstate(string id);
    }
}
