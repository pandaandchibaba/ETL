using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Common;
using IOT.ETL.IRepository.ISys_paramIRepository;
using IOT.ETL.Model;

namespace IOT.ETL.Repository.ISys_paramRepository
{
    public class ISys_paramRepository : ISys_paramIRepository
    {
        //实例化帮助文件
        RedisHelper<Model.sys_param> rp = new RedisHelper<sys_param>();
        RedisHelper<Model.sys_user> rl = new RedisHelper<Model.sys_user>();
        //定义缓存关键字
        string redisKey;
        //创建登录缓存关键字
        string LoginKey;
        // 获取全部数据
        List<IOT.ETL.Model.sys_param> lst = new List<sys_param>();
        //登录集合
        List<Model.sys_user> lstl = new List<Model.sys_user>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public ISys_paramRepository()
        {
            redisKey = "sys_param_list";
            lst = rp.GetList(redisKey);
            LoginKey = "Login_list";
            lstl = rl.GetList(LoginKey);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sys_Param"></param>
        /// <returns></returns>
        public int AddSys_param(sys_param sys_Param)
        {
            try
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                string sql = $"INSERT INTO sys_param VALUES(UUID(),'{sys_Param.Code}','{sys_Param.Name}','{sys_Param.Pid}',{sys_Param.Default_status},{sys_Param.Is_system},{sys_Param.Is_del},{sys_Param.Order_by},'{sys_Param.Text}','{sys_User.name}',NOW(),'{sys_User.name}',NOW())";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    sys_Param = DapperHelper.GetList<Model.sys_param>("SELECT *FROM sys_param ORDER BY create_time DESC LIMIT 1").FirstOrDefault();
                    lst.Add(sys_Param);
                    rp.SetList(lst, redisKey);
                    return i;
                }
                else
                {
                    return i;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Dictionary<string, object>> BindParent()
        {
            string sql = "SELECT *FROM sys_param ORDER BY order_by;";
            List<Model.sys_param> list = DapperHelper.GetList<Model.sys_param>(sql);
            List<Dictionary<string, object>> Alltree = Recursion(list,"0");
            //return Alltree;
        }
        /// <summary>
        /// 递归方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> Recursion(List<Model.sys_param> lst, string pid) 
        {
            //字典集合
            List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
            //获取所有的子节点集合
            List<Model.sys_param> lstson = lst.Where(x => x.Pid.Equals(pid)).ToList();
            //循环所有的子节点
            foreach (var item in lstson)
            {
                Dictionary<string, object> jsonsub = new Dictionary<string, object>();
                jsonsub.Add("value", item.Id);
                jsonsub.Add("label", item.Name);
                if (lst.Count(x=>x.Pid==item.Id)>0)
                {
                    jsonsub.Add("children", Recursion(lst, item.Id));
                }
                json.Add(jsonsub);
            }
            return json;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelSys_param(string ids)
        {
            string sql = $"DELETE FROM sys_param WHERE id in('{ids}')";
            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                //截取
                string[] arr = ids.Split(',');
                foreach (var item in arr)
                {
                    //找到要删除的对象
                    Model.sys_param sys_Param = lst.FirstOrDefault(x => x.Id.Equals(item));
                    lst.Remove(sys_Param);
                }
                //从新存入
                rp.SetList(lst, redisKey);
            }
            return i;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sys_Param"></param>
        /// <returns></returns>
        public int UptSys_param(sys_param sys_Param)
        {
            try
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                string sql = $"UPDATE sys_param SET code='{sys_Param.Code}',name='{sys_Param.Name}',is_system={sys_Param.Is_system},default_status={sys_Param.Default_status},order_by={sys_Param.Order_by},text='{sys_Param.Text}',update_by='{sys_User.name}',update_time=NOW()  WHERE id='{sys_Param.Id}'";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    lst[lst.IndexOf(lst.First(x => x.Id == sys_Param.Id))] = sys_Param;
                    //从新存入
                    rp.SetList(lst, redisKey);
                    return i;
                }
                else
                {
                    return i;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        List<sys_param> ISys_paramIRepository.ShowSys_param(string pid)
        {
            lst = null;
            try
            {
                List<Model.sys_param> ls = new List<Model.sys_param>();
                //判断缓存是否存在
                if (lst == null || lst.Count == 0)
                {
                    lst = DapperHelper.GetList<IOT.ETL.Model.sys_param>("SELECT a.*,b.name fname FROM sys_param a LEFT  JOIN sys_param b on a.pid=b.id ORDER BY a.order_by");

                    //lst.Where(m => m.Pid == m.Id).FirstOrDefault().HasChildren = true;

                    //from s in lst join 

                    foreach (var s in lst)
                    {
                        foreach (var ss in lst)
                        {
                            if (s.Id == ss.Pid)
                            {
                                s.HasChildren = true;
                            }
                        }
                    }

                    //不存在
                    //按order_by排序  左连接 子节点在前 父节点在后
                    if (pid != "0" || pid != null || !string.IsNullOrEmpty(pid))
                    {
                        ls = lst.Where(x => x.Pid == pid).ToList();
                    }
                }
                if (ls.Count != 0)
                {
                    return ls;
                }
                if (pid == "0" || pid == null)
                {
                    return lst.Where(m => m.Pid == "0").ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
