using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Interfaces
{
    public interface ISC_ApiService
    {
        Task<HttpResponseMessage> GetPlayerInformation(string nickname);
    }
}
