using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.Models.Responses
{
    public sealed class ApiOkPagedResponse<ResultList, ResultMetaData> : ApiBaseResponse
    {
        public ResultList PagedList { get; set; }
        public ResultMetaData MetaData { get; set; }
        public ApiOkPagedResponse(ResultList pagedList, ResultMetaData metadata)
            : base(true)
        {
            PagedList = pagedList;
            MetaData = metadata;
        }
    }
}
