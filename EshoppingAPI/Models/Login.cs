using System;
using System.Collections.Generic;

#nullable disable

namespace EshoppingAPI.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string EmailId { get; set; }
        public decimal? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string Password { get; set; }
        public int? IsAdmin { get; set; }
    }
}