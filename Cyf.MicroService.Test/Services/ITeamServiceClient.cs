using Cyf.MicroService.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyf.MicroService.AggregateService.Services
{
    /// <summary>
    /// 服务调用
    /// </summary>
    public interface ITeamServiceClient
    {
        /// <summary>
        /// 服务调用
        /// </summary>
        /// <returns></returns>
        Task<ActionResult> GetTeams();
    }
}
