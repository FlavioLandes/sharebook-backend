﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sharebook.Jobs;
using ShareBook.Api.Filters;
using ShareBook.Helper;
using ShareBook.Service.Authorization;
using ShareBook.Service.Server;
using System;

namespace ShareBook.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class OperationsController : Controller
    {

        protected IJobExecutor _executor;
        protected string _validToken;

        public OperationsController(IJobExecutor executor, IOptions<ServerSettings> settings)
        {
            _executor = executor;
            _validToken = settings.Value.JobExecutorToken;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [AuthorizationFilter(Permissions.Permission.ApproveBook)] // adm
        [Route("ForceException")]
        public IActionResult ForceException()
        {
            var teste = 1 / Convert.ToInt32("Teste");
            return BadRequest();
        }

        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            var result = new
            {
                ServerNow = DateTime.Now,
                SaoPauloNow = DateTimeHelper.ConvertDateTimeSaoPaulo(DateTime.Now),
                ServerToday = DateTime.Today,
                SaoPauloToday = DateTimeHelper.GetTodaySaoPaulo(),
                Message = "Pong!"
            };
            return Ok(result);
        }

        [HttpGet("JobExecutor")]
        [Throttle(Name = "JobExecutor", Seconds = 5, VaryByIp = false)]
        public IActionResult Executor()
        {
            if (!_IsValidJobToken())
                return Unauthorized();
            else
                return Ok(_executor.Execute());
        }

        protected bool _IsValidJobToken() => Request.Headers["Authorization"].ToString() == _validToken;
    }
}
