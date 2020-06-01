using Microsoft.EntityFrameworkCore;
using Cyf.MicroService.TeamService.Context;
using Cyf.MicroService.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyf.MicroService.TeamService.Repositories
{
    /// <summary>
    /// 团队仓储实现
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
       // public TeamContext teamContext;
        //public TeamRepository(TeamContext teamContext)
        //{
        //    this.teamContext = teamContext;
        //}
        public void Create(Team team)
        {
            //teamContext.Teams.Add(team);
            //teamContext.SaveChanges();
        }

        public void Delete(Team team)
        {
            //teamContext.Teams.Remove(team);
            //teamContext.SaveChanges();
        }

        public Team GetTeamById(int id)
        {
            return new Team()
            {
                Id = 1,
                Name = "小疯疯"
            };
            //return teamContext.Teams.Find(id);
        }

        public IEnumerable<Team> GetTeams()
        {
            return new List<Team>()
            {
                new Team()
                {
                    Id = 1,
                    Name = "小疯疯"
                },
                new Team()
                {
                    Id = 2,
                    Name = "茂茂"
                }
            };
            //return teamContext.Teams.ToList();
        }

        public void Update(Team team)
        {
            //teamContext.Teams.Update(team);
            //teamContext.SaveChanges();
        }
        public bool TeamExists(int id)
        {
            return true;
            //return teamContext.Teams.Any(e => e.Id == id);
        }
    }
}
