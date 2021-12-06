using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrganizujProslavu.Areas.Identity.Data
{
    public class Karakteristike
    {
        public int KarakteristikeId {get; set;}
        public string Naziv {get; set;}
        public bool IsChecked {get; set;}
        public Oglas Oglas {get; set;}

        public Karakteristike()
        {
            
        }
        public Karakteristike(string Ime)
        {
            Naziv = Ime;
        }
    }
}