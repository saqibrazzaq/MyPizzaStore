using System.ComponentModel.DataAnnotations;

namespace hr.Dtos.Company
{
    public class UpdateCompanyRequestDto : ManipulateCompanyDto
    {
        [Required]
        public Guid? CompanyId { get; set; }
    }
}
