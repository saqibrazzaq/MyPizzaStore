using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.Models.Responses
{
    public class ApiOkResponse<TResult> : ApiBaseResponse
    {
        public ApiOkResponse(TResult result) : base(true)
        {
            Data = result;
        }

        public TResult Data { get; set; }
    }
}
