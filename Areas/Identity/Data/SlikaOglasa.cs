using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrganizujProslavu.Areas.Identity.Data
{
    public class SlikaOglasa
    {
        public int Id {get; set;}
        public string PhotoPath {get; set;}

        public Oglas Oglas {get; set;}
    }
}
