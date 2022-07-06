using common.Models.Parameters;

namespace auth.Dtos.User
{
    public class UserParameters : PagedRequestParameters
    {
        public string? SearchTerm { get; set; }
    }
}
