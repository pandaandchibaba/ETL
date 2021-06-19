using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_user
{
   public interface IsysUserRepository
    {
        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        List<IOT.ETL.Model.sys_user> ShowUser();
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DelUser(string id);
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
