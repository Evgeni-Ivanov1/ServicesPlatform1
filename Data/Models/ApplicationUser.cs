using Microsoft.AspNetCore.Identity;
using System;

namespace ServicesPlatform.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
