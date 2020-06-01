using Newtonsoft.Json;
using Cyf.MicroService.Core.Cluster;
using Cyf.MicroService.Core.HttpClientConsul;
using Cyf.MicroService.Core.Registry;
using Cyf.MicroService.TeamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cyf.MicroService.AggregateService.Services
{
    /// <summary>
    /// 服务调用实现
    /// </summary>
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        /*public readonly IServiceDiscovery serviceDiscovery;
        public readonly ILoadBalance loadBalance;*/
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string ServiceSchme = "https";
        private readonly string ServiceName = "teamservice"; //服务名称
        private readonly string ServiceLink = "/Teams"; //服务名称
        private readonly ConsulHttpClient consulHttpClient;
        public HttpTeamServiceClient(/*IServiceDiscovery serviceDiscovery, 
                                    ILoadBalance loadBalance,
                                    IHttpClientFactory httpClientFactory,*/
                                    ConsulHttpClient consulHttpClient)
        {
            /*this.serviceDiscovery = serviceDiscovery;
            this.loadBalance = loadBalance;*/
            //this.httpClientFactory = httpClientFactory;
            this.consulHttpClient = consulHttpClient;
        }

        public async Task<IList<Team>> GetTeams()
        {
            List<Team> teams = await consulHttpClient.GetAsync<List<Team>>(ServiceSchme, ServiceName, ServiceLink);
            return teams;
        }
    }
}
