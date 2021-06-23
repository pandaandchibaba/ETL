using IOT.ETL.IRepository.IDataAnalysisRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
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
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化缓存日志
        public DataAnalysisController(IDataAnalysisRepository dataAnalysis)
        {
            _dataAnalysis = dataAnalysis;
        }

        #region 根据不同的数据库显示不同的表数据
        /// <summary>
        /// 根据不同的数据库显示不同的表数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("/api/GetTable")]
        public async Task<string> GetTable(string sql, string dbName, string code)
        {
            try
            {
                return await _dataAnalysis.GetDateTable(sql, dbName, code);
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
        public IActionResult BindLeftTree()
        {
            return Ok(_dataAnalysis.BindTree());
        }
        #endregion
    }
}