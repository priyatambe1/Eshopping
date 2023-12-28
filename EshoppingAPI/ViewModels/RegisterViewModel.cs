using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshoppingAPI.ViewModels
{
    public class RegisterViewModel
    {

        
        public string? FullName { get; set; }
        public string EmailId { get; set; }
        public decimal? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string Password { get; set; }
        public int? IsAdmin { get; set; }
    }
}