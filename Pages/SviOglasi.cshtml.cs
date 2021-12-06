using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OrganizujProslavu.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace OrganizujProslavu.Pages
{
    public class SviOglasiModel : PageModel
    {
        private readonly OrganizujProslavuIdentityDbContext dbContext;

        [BindProperty]
        public IList<Oglas> Oglasi {get; set;}

        [BindProperty]
        public List<SelectListItem> TipOglasa {get; set;}

        [BindProperty(SupportsGet=true)]
        public string TipOglasaFilter {get; set;}

        [BindProperty(SupportsGet=true)]
        public string FilterIme {get; set;}

        public SviOglasiModel(OrganizujProslavuIdentityDbContext db)
        {
            dbContext = db;
        }
        public async Task OnGetAsync()
        {
            TipOglasa = new List<SelectListItem>
            {
                new SelectListItem { Text="Bend", Value = "Bend"},
                new SelectListItem { Text="Restoran", Value = "Restoran"},
                new SelectListItem { Text="Kafic", Value = "Kafic"},
                new SelectListItem { Text="Kafana", Value = "Kafana"},
                new SelectListItem { Text="Igraonica", Value = "Igraonica"},
                new SelectListItem { Text="Bar", Value = "Bar"}
            };

            IQueryable<Oglas> qOglas = dbContext.Oglasi.Include(x=>x.SlikeOglasa);

            if(!string.IsNullOrEmpty(TipOglasaFilter))
            {
                qOglas = dbContext.Oglasi.Where(x=>x.TipOglasa == TipOglasaFilter);
            }

            if(!string.IsNullOrEmpty(FilterIme))
            {
                qOglas = dbContext.Oglasi.Include(x=>x.SlikeOglasa).Where(x=>x.Naziv.Contains(FilterIme));
            }

            if(!string.IsNullOrEmpty(FilterIme) && !string.IsNullOrEmpty(TipOglasaFilter))
            {
                qOglas = dbContext.Oglasi.Include(x=>x.SlikeOglasa).Where(x=>x.Naziv.Contains(FilterIme) && x.TipOglasa == TipOglasaFilter);
            }


            Oglasi = await qOglas.Include(x=>x.SlikeOglasa).ToListAsync();
        }

         public async Task<IActionResult> OnPostObrisiOglasAsync(int Id)
        {
            var Oglas = await dbContext.Oglasi.FindAsync(Id);

            var sviTermini = dbContext.Termini.Where(x=>x.Oglas.Id == Id).ToList();
            var sviClanovi = dbContext.ClanoviBenda.Where(x=>x.Oglas.Id == Id).ToList();
            var sveKarakteristike = dbContext.Karakteristike.Where(x=>x.Oglas.Id == Id).ToList();
            var sveRezervacije = dbContext.Rezervacije.Where(x=>x.Oglas.Id == Id).ToList();
            var slike = dbContext.SlikeOglasa.Where(x=>x.Oglas.Id ==Id).ToList();
            if(Oglas!=null)
            {
                if(sviTermini!=null)
                {
                    foreach (var termin in sviTermini)
                    {
                    dbContext.Termini.Remove(termin);   
                    }
                }
                if(sviClanovi!=null)
                {
                    foreach (var clan in sviClanovi)
                    {
                    dbContext.ClanoviBenda.Remove(clan);   
                    }
                }
                if(sveKarakteristike!=null)
                {
                    foreach (var karakteristika in sveKarakteristike)
                    {
                    dbContext.Karakteristike.Remove(karakteristika);   
                    }
                }
                if(sveRezervacije!=null)
                {
                    foreach (var rezervacija in sveRezervacije)
                    {
                    dbContext.Rezervacije.Remove(rezervacija);   
                    }
                }
                if(slike !=null)
                {
                    foreach (var s in slike)
                    {
                    dbContext.SlikeOglasa.Remove(s);   
                    }
                }

                dbContext.Oglasi.Remove(Oglas);
                await dbContext.SaveChangesAsync();
                TempData["Oglas"] = "Success";
                return RedirectToPage();
            }
            TempData["Oglas"] = "Error";
            return Page();
        }
    }
}
