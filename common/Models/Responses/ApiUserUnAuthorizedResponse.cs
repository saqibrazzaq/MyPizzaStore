using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.Models.Responses
{
    public sealed class ApiUserUnAuthorizedResponse : ApiBaseResponse
    {
        public string Message { get; set; }
        public ApiUserUnAuthorizedResponse(string message) : base(false)
        {
            Message = message;
        }
    }
}
