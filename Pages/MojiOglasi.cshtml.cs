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

namespace OrganizujProslavu.Pages
{
    [Authorize(Roles = "Admin,Korisnik,Oglasivac")]
    public class MojiOglasiModel : PageModel
    {
        private readonly UserManager<WebApp1User> _userManager;
        private readonly OrganizujProslavuIdentityDbContext dbContext;

        [BindProperty]
        public IList<Oglas> Oglasi {get; set;}
        [BindProperty]
        public Termin Termin {get; set;}

        public MojiOglasiModel(OrganizujProslavuIdentityDbContext db, UserManager<WebApp1User> userManager)
        {
            dbContext = db;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            IQueryable<Oglas> qOglas = dbContext.Oglasi.Include(x=>x.Karakteristike).Include(x=>x.ClanoviBenda)
                                        .Where(x=>x.Korisnik == user);

            Oglasi = qOglas.ToList();
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