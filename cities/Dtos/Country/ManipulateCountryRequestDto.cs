﻿using System.ComponentModel.DataAnnotations;

namespace cities.Dtos.Country
{
    public class ManipulateCountryRequestDto
    {
        public string? CountryCode { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? PhoneCode { get; set; }
        public string? Capital { get; set; }
        public string? Currency { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencySymbol { get; set; }
        public string? NativeName { get; set; }
        public string? Region { get; set; }
        public string? SubRegion { get; set; }
    }
}
