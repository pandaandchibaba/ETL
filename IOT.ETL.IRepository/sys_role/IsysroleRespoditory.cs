using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_role
{
   public interface IsysroleRespoditory
    {
      /// <summary>
      /// 显示
      /// </summary>
      /// <returns></returns>
        List<IOT.ETL.Model.sys_role>ShowRoles();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        int insertRoles(IOT.ETL.Model.sys_role a);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        int UpdateRoles(IOT.ETL.Model.sys_role a);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int delRoles(string id);
    }
}
