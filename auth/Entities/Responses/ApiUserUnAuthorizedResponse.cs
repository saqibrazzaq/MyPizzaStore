namespace auth.Entities.Responses
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
