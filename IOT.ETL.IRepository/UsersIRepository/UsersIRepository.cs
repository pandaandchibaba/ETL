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
        Task<int> AddUsers(Model.sys_user a);

        //登录
        Task<int> LoginUsers(string username, string password);

        //获取所有用户信息
        Task<List<Model.V_user_role>> GetUsers();

        //邮箱修改密码
        Task<int> UptPwd(string email, string password);

        //电话修改密码
        Task<int> UptPhone(string phone, string password);
    }
}
