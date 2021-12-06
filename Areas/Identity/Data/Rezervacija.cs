using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizujProslavu.Areas.Identity.Data
{
    public class Rezervacija
    {
        public int RezervacijaId {get; set;}
        [ForeignKey("WebApp1User")]
        public string KorisnikId {get; set;}
        public WebApp1User Korisnik { get; set; }
        [ForeignKey("Oglas")]
        public int OglasId{get; set;}
        public Oglas Oglas {get; set;}
        public DateTime Datum {get; set;}
        
        public string TipProslave {get; set;}
        public int BrojGosta {get; set;}
        public string Opis {get; set;}
       
    }
}