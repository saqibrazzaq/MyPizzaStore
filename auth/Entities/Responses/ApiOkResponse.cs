namespace auth.Entities.Responses
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
