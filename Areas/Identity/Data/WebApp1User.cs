using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OrganizujProslavu.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the WebApp1User class
    public class WebApp1User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string LastName {get; set;}
        [PersonalData]
        public string City {get; set;}
        public string PhotoPath {get; set;}
        public virtual ICollection<Oglas> Oglasi {get; set;}
        public virtual ICollection<Rezervacija> Rezervacija { get; set; }
    }
}
