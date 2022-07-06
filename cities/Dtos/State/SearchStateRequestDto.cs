using common.Models.Parameters;

namespace cities.Dtos.State
{
    public class SearchStateRequestDto : PagedRequestParametersPublic
    {
        public Guid? CountryId { get; set; }
        public Guid? StateId { get; set; }
    }
}
