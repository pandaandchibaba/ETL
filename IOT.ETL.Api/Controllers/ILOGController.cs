using IOT.ETL.IRepository.ILOGIRepository;
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
    public class ILOGController : ControllerBase
    {
        private readonly ILOGIRepository _iLOGIRepository;
        public ILOGController(ILOGIRepository iOGIRepository)
        {
            _iLOGIRepository = iOGIRepository;
        }

        //添加
        [Route("/api/AddILOG")]
        [HttpPost]
        public async Task<int> AddILOG([FromForm]IOT.ETL.Model.etl_data_engine a)
        {
            int i = await _iLOGIRepository.AddILOG(a);
            return i;
        }

        //显示
        [Route("/api/ShowILOG")]
        [HttpGet]
        public async Task<IActionResult> ShowILOG(string nm1 = "", string nm2 = "", string nm3 = "")
        {
            //获取全部数据
            var ls = await _iLOGIRepository.ShowILOG();
            if (!string.IsNullOrEmpty(nm1))
            {
                ls = ls.Where(x => x.engine_name.Contains(nm1)).ToList();
            }
            if (!string.IsNullOrEmpty(nm2))
            {
                ls = ls.Where(x => x.code_type.Contains(nm2)).ToList();
            }
            if (!string.IsNullOrEmpty(nm3))
            {
                ls = ls.Where(x => x.cl_name.Contains(nm3)).ToList();
            }
            return Ok(new
            {
                msg = "",
                code = 0,
                data = ls
            });
        }

       

        //删除
        [Route("/api/DelILOG")]
        [HttpGet]
        public async Task<int> DelILOG(string id)
        {
            int i = await _iLOGIRepository.DelILOG(id);
            return i;
        }

        //修改
        [Route("/api/UptILOG")]
        [HttpPost]
        public async Task<int> UptILOG([FromForm] IOT.ETL.Model.etl_data_engine a)
        {
            int i = await _iLOGIRepository.UptILOG(a);
            return i;
        }
    }
}
