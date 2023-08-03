using Newtonsoft.Json;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
using SC_statistic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services
{
    public class SC_ApiService : ISC_ApiService
    {
        protected const string SC_Url_GetPlayerStat = "http://gmt.star-conflict.com/pubapi/v1/userinfo.php?nickname={0}";
        protected readonly IHttpClientFactory _httpClientFactory;

        public SC_ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetPlayerInformation(string nickname)
        {
            var endpoint = string.Format(SC_Url_GetPlayerStat, nickname);
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response;
        }
    }
}
