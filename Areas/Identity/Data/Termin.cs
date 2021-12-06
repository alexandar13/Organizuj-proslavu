using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OrganizujProslavu.Areas.Identity.Data
{
    //visevrednosni atribut se mapira kao nova tabelica, zbog toga ovo
    public class Termin
    {
        public int TerminId {get; set;}
        public virtual Oglas Oglas {get; set;}

        public string KorisnikImePrezime {get; set;}
        public string KorisnikBroj {get; set;}
        public DateTime ZauzetTermin {get; set;}

        public string TipProslave {get; set;}

        public int BrojGosta {get; set;}
        public string Opis {get; set;}
    }
}