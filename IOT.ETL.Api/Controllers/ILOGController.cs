﻿using IOT.ETL.IRepository.ILOGIRepository;
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
        public int AddILOG([FromForm]IOT.ETL.Model.etl_data_engine a)
        {
            int i = _iLOGIRepository.AddILOG(a);
            return i;
        }

        //显示
        [Route("/api/ShowILOG")]
        [HttpGet]
        public IActionResult ShowILOG(string nm = "")
        {
            //获取全部数据
            var ls = _iLOGIRepository.ShowILOG();
            if (!string.IsNullOrEmpty(nm))
            {
                ls = ls.Where(x => x.engine_name.Contains(nm)).ToList();
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
        [HttpPost]
        public int DelILOG(string id)
        {
            int i = _iLOGIRepository.DelILOG(id);
            return i;
        }

        //修改
        [Route("/api/UptILOG")]
        [HttpPost]
        public int UptILOG([FromForm] IOT.ETL.Model.etl_data_engine a)
        {
            int i = _iLOGIRepository.UptILOG(a);
            return i;
        }
    }
}
