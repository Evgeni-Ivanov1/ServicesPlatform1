using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ServicesPlatform.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
