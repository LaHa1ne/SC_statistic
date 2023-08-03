using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Responses
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
