using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cyf.MicroService.AggregateService.Services;
using Cyf.MicroService.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cyf.MicroService.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggregateController : ControllerBase
    {
        private readonly ITeamServiceClient teamServiceClient;

        public AggregateController(ITeamServiceClient teamServiceClient)
        {
            this.teamServiceClient = teamServiceClient;
        }

        // GET: api/Aggregate
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            /*// 1、查询团队
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:5001/Teams");
            
            string json = await response.Content.ReadAsStringAsync();
            IList<Team> teams = JsonConvert.DeserializeObject<List<Team>>(json);*/
            return await teamServiceClient.GetTeams();

            /*// 2、查询团队成员
            foreach (var team in teams)
            {
                HttpResponseMessage response1 = await httpClient.GetAsync($"https://localhost:5002/Members?teamId={team.Id}");
                string json1 = await response1.Content.ReadAsStringAsync();

                List<Member> members = JsonConvert.DeserializeObject<List<Member>>(json1);

                team.Members = members;
            }*/

            //return Ok(teams);
        }
    }
}
