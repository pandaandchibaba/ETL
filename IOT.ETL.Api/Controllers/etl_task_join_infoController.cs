
using IOT.ETL.IRepository.BI_DataAnalysis;
using IOT.ETL.IRepository.IDataAnalysisRepository;
using IOT.ETL.IRepository.Ietl_task_join_info;
using IOT.ETL.Model;
using IOT.ETL.Repository.etl_task_join_info;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    public enum enum_DataBase
    {
        MySQL = 1,
        SqlServer = 2
    }
    [Route("api/[controller]")]
    [ApiController]
    public class etl_task_join_infoController : ControllerBase
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Ietl_task_join_infoRepository _etl_Task_Join_InfoRepository;
        private readonly IBI_DataAnalysisRepositorys  _bI_DataAnalysisRepositorys;

        public etl_task_join_infoController(Ietl_task_join_infoRepository etl_Task_Join_InfoRepository, IBI_DataAnalysisRepositorys iBI_DataAnalysisRepositor)
        {
            _etl_Task_Join_InfoRepository = etl_Task_Join_InfoRepository;
            _bI_DataAnalysisRepositorys = iBI_DataAnalysisRepositor;
        }

        [HttpPost]
        [Route("/api/AddInputA")]
        //获取到第一个表的数据
        public async Task<int> AddInputA(etl_task_input_info ta)
        {
            int i = 0;
            try
            {
                i = await _etl_Task_Join_InfoRepository.AddInputA(ta);
            }
            catch (Exception ex)
            {
                if (i > 0)
                {
                    logger.Debug($"第一个表的数据获取成功");
                }
                else
                {
                    logger.Debug($"第一个表的数据获取失败,报错：{ex}");
                }
                throw;
            }
            return i;
        }
        [HttpPost]
        [Route("/api/AddInputB")]
        //获取到第二个表的数据
        public async Task<int> AddInputB(etl_task_input_info ta)
        {

            int i = 0;
            try
            {
                i = await _etl_Task_Join_InfoRepository.AddInputB(ta);
            }
            catch (Exception ex)
            {
                if (i > 0)
                {
                    logger.Debug($"第二个表的数据获取成功");
                }
                else
                {
                    logger.Debug($"第二个表的数据获取失败,报错：{ex}");
                }
                throw;
            }
            return i;
        }
        [HttpPost]
        [Route("/api/AddJoin")]
        //拼接sql语句
        public async Task<int> AddJoin([FromForm] etl_task_join_info jo)
        {
            int i = await _etl_Task_Join_InfoRepository.AddJoin(jo);
            return i;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //根据数据库名称查出所有字段
        [HttpGet]
        [Route("/api/ShowField")]
        public async Task<OkObjectResult> ShowField(string database_name = "")
        {
            return Ok(new { msg = "", code = 0, data = await _etl_Task_Join_InfoRepository.ShowField(database_name) });
        }
        [HttpGet]
        [Route("/api/FanFieldID")]
        //根据任务ID查询出其任务设计的相关信息   显示出对应字段 
        public async Task<OkObjectResult> FanFieldID(string id = "")
        {
            return Ok(new { msg = "", code = 0, data = await _etl_Task_Join_InfoRepository.FanFieldIDAsync(id) });
        }
        [HttpGet]
        [Route("/api/SelectBind")]
        //根据ID和所属的数据库进行筛选   显示其中选中字段
        public async Task<OkObjectResult> SelectBind(string database_name = "", string id = "")
        {
            return Ok(new { msg = "", code = 0, data = await _etl_Task_Join_InfoRepository.SelectBind(database_name, id) });
        }
        [HttpGet]
        [Route("/api/OutputAllField")]
        //所有选中的字段输出
        public async Task<OkObjectResult> OutputAllField(string id = "")
        {
            return Ok(new { msg = "", code = 0, data = await _etl_Task_Join_InfoRepository.OutputAllField(id) });
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //点击执行
        [HttpPost]
        [Route("/api/ExecuteAllSql")]
        public async Task<int> ExecuteAllSql([FromForm]SuccessNum sn)
        {
            try
            {
                string id = sn.j;
                int flag = 1;
                string name = "";
                enum_DataBase ed = (enum_DataBase)flag;
                int i = 0;
                switch (ed)
                {
                    case enum_DataBase.MySQL:
                        //获取全部数据
                        i = await _etl_Task_Join_InfoRepository.ExecuteAllSql(id, "");
                        break;
                    case enum_DataBase.SqlServer:
                        //获取全部数据
                        i = await _etl_Task_Join_InfoRepository.ExecuteAllSql(id, name);
                        break;
                }

                if (!string.IsNullOrEmpty(name))
                {
                    logger.Debug($"根据数据库名称，sql查询其数据库并返回，执行SQL为:sqlserve");
                }
                else
                {
                    logger.Debug($"根据数据库名称，sql查询其数据库并返回，执行SQL为:mysql");
                }


                return i;
            }
            catch (Exception)
            {

                throw;
            }





        }
        //数据库树         public async Task<string>  DatabaseTree()
        [Route("/api/DatabaseTreeTwo")]
        [HttpGet]
        public async Task<string> DatabaseTreeTwo()
        {
            try
            {
                //获取MySql全部数据
                List<Model.GetDataBases> getDataBases = await _bI_DataAnalysisRepositorys.GetDatabaseName(1);
                List<Model.GetDataBases> getDataBasesSql = await _bI_DataAnalysisRepositorys.GetDatabaseName(2);

                //用于拼接的字符串
                StringBuilder builder = new StringBuilder();

                builder.Append("{");
                builder.Append("id:1");
                builder.Append(",label:'MYSQL数据库'");
                builder.Append(",children:[");

                //MySql第一层循环 拼接数据库名
                for (int i = 0; i < getDataBases.Count; i++)
                {
                    Random rd = new Random();
                    int a = rd.Next();
                    builder.Append("{");
                    builder.Append("id:" + $"{a}");
                    builder.Append(",label:'" + getDataBases[i].SCHEMA_NAME + "'");


                    List<Model.GetTables> getTables = await _bI_DataAnalysisRepositorys.GetTableName(getDataBases[i].SCHEMA_NAME, 1);
                    builder.Append(",children:[");

                    //MySql第二层循环 拼接数据库下的表名
                    for (int j = 0; j < getTables.Count; j++)
                    {
                        int b = rd.Next();
                        builder.Append("{id:" + $"{b}");
                        builder.Append(",label:'" + getTables[j].Table_Name + "'},");
                    }

                    builder.Append("]},");

                }
                builder.Append("]},");

                builder.Append("{");
                builder.Append("id:2");
                builder.Append(",label:'SqlServer数据库'");
                builder.Append(",children:[");

                //SqlServer第一层循环 拼接数据库名
                for (int i = 0; i < getDataBasesSql.Count; i++)
                {
                    Random rd = new Random();
                    int c = rd.Next();
                    builder.Append("{");
                    builder.Append("id:" + $"{c}");
                    builder.Append(",label:'" + getDataBasesSql[i].SCHEMA_NAME + "'");


                    List<Model.GetTables> getTablesSql = await _bI_DataAnalysisRepositorys.GetTableName(getDataBasesSql[i].SCHEMA_NAME, 2);
                    builder.Append(",children:[");

                    //SqlServer第二层循环 拼接数据库下的表名
                    for (int j = 0; j < getTablesSql.Count; j++)
                    {
                        int d = rd.Next();

                        builder.Append("{id:" + $"{d}");
                        builder.Append(",label:'" + getTablesSql[j].Table_Name + "'},");
                    }

                    builder.Append("]},");

                }
                builder.Append("]},");




                //添加日志
                logger.Debug($"显示数据库名及其表名，拼接树");

                //返回字符串并去掉末尾逗号
                return builder.ToString().TrimEnd(',');
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
