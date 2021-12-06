using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrganizujProslavu.Areas.Identity.Data
{
    public class Oglas
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? BrojClanova {get; set;} //moze da bude null
        public int? BrojGosta {get; set;}
        public int? BrojSala {get; set;}
        public int? BrojDece {get; set;}
        public string Opis {get; set;}
        public string TipOglasa {get; set;}
        public string VrstaMuzike {get; set;}
        public string Adresa {get; set;}
        public string Grad { get; set; }
        public string BrojTelefona {get; set;}
        public string NaslovnaSlika {get; set;}
        public WebApp1User Korisnik {get; set;}


        public virtual ICollection<Karakteristike> Karakteristike {get; set;} //Ako stavimo IList nece da napravi bazu, dok ICollection hoce
        public virtual ICollection<Termin> Termini {get; set;}
        public virtual ICollection<Rezervacija> Rezervacije { get; set; }
        public virtual ICollection<ClanBenda> ClanoviBenda {get; set;}
        public virtual ICollection<SlikaOglasa> SlikeOglasa {get; set;}

    }
}