using IOT.ETL.IRepository.IDataAnalysisRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAnalysisController : ControllerBase
    {
        private readonly IDataAnalysisRepository _dataAnalysis;
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化缓存日志
        public DataAnalysisController(IDataAnalysisRepository dataAnalysis)
        {
            _dataAnalysis = dataAnalysis;
        }

        #region 根据不同的数据库显示不同的表数据
        // 根据不同的数据库显示不同的表数据
        [HttpGet("/api/GetTable")]
        public async Task<IActionResult> GetTable(string sql, string dbName, string code)
        {
            try
            {
                var json = await _dataAnalysis.GetDateTable(sql, dbName, code);
                return Ok(new { json = json.Json, time = json.Time });
            }
            catch (Exception ex)  //捕捉异常
            {
                logger.Debug(ex.Message);
                throw;
            }

        }
        #endregion

        #region 绑定左侧树
        /// <summary>
        /// 绑定左侧树
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/BindLeftTree")]
        public async Task<IActionResult> BindLeftTree()
        {
            var ss = await _dataAnalysis.BindTree();
            return Ok(ss);
        }
        #endregion
    }
}