using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace hr.Dtos.Company
{
    public class ManipulateCompanyDto : RequestDto
    {
        [Required, MaxLength(500)]
        public string? Name { get; set; }
        [MaxLength(500)]
        public string? Address1 { get; set; }
        [MaxLength(500)]
        public string? Address2 { get; set; }
        // Microservice Api keys
        public Guid? CityId { get; set; }
    }
}
