using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshoppingAPI.ViewModels
{
    public class LoginViewModel
    {
        public string EmailId { get; set; }

        public string Password { get; set; }
        public string? FullName { get; internal set; }
        public string? Address { get; internal set; }
        public decimal? PhoneNumber { get; internal set; }
        public string? Gender { get; internal set; }
    }
}