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

        /// <summary>
        /// 显示查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("/api/GetTable")]
        public IActionResult GetTable(string sql,string dbName,int code=1)
        {
            return Ok(_dataAnalysis.GetTable(sql, dbName));
        }
    }
}
