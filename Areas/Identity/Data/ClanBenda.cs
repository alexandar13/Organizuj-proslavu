using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrganizujProslavu.Areas.Identity.Data
{
    public class ClanBenda 
    {
        public int ClanBendaId {get; set;}
        public string Ime {get; set;}
        public string Instrument {get; set;}
        public Oglas Oglas {get; set;}

        public ClanBenda()
        {

        }

        public ClanBenda(string ime, string instrument, Oglas oglas)
        {
            Ime = ime;
            Instrument = instrument;
            Oglas = oglas;
        }

    }
}