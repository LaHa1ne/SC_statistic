using AutoMapper;
using Newtonsoft.Json;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataLayer.DTO.Corporation;
using SC_statistic.DataLayer.DTO.PlayerHistory;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Responses;
using SC_statistic.Services.Interfaces;
using SC_statistic.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services
{
    public class CorporationService : ICorporationService
    {
        protected readonly IMapper _mapper;
        protected readonly ICorporationRepository _corporationRepository;

        public CorporationService(IMapper mapper, ICorporationRepository corporationRepository)
        {
            _mapper = mapper;
            _corporationRepository = corporationRepository;
        }

        public async Task<BaseResponse<CorporationDTO>> GetCorporationInfo(string? name, string? tag)
        {
            try
            {
                var corporation = name != null ? await _corporationRepository.GetByName(name) : await _corporationRepository.GetByTag(tag!);
                if (corporation != null)
                {
                    return new BaseResponse<CorporationDTO>()
                    {
                        Description = "Информация о корпорации получена",
                        Data = _mapper.Map<CorporationDTO>(corporation),
                        StatusCode = HttpStatusCode.OK
                    };
                }
                return new BaseResponse<CorporationDTO>()
                {
                    Description = "Корпорация не найдена",
                    StatusCode = HttpStatusCode.NotFound,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CorporationDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

    }
}