using IOT.ETL.IRepository.IDataAnalysisRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAnalysisController : ControllerBase
    {
        private readonly IDataAnalysisRepository _dataAnalysis;
        public DataAnalysisController(IDataAnalysisRepository dataAnalysis)
        {
            _dataAnalysis = dataAnalysis;
        }

        #region 显示
        /// <summary>
        /// 显示查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("/api/GetTable")]
        public IActionResult GetTable(string sql, string dbName, int code = 0)
        {
            return Ok(_dataAnalysis.GetTable(sql, dbName));
        }
        #endregion

        #region 绑定左侧树
        /// <summary>
        /// 绑定左侧树
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/BindLeftTree")]
        public IActionResult BindLeftTree()
        {
            return Ok(_dataAnalysis.BindTree());
        }
        #endregion
    }
}
