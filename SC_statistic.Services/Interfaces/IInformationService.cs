using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Interfaces
{
    public interface IInformationService
    {
        Task UpdateInformation(ResponseWithStat response, string requestedNickname);
    }
}
