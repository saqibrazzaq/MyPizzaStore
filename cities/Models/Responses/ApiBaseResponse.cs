namespace cities.Models.Responses
{
    public class ApiBaseResponse
    {
        public bool Success { get; set; }
        public ApiBaseResponse(bool success)
        {
            Success = success;
        }
    }
}
