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

namespace OrganizujProslavu.Pages
{
    [AllowAnonymous]
    public class ProfilOglasaModel : PageModel
    {
        private readonly UserManager<WebApp1User> _userManager;
        private readonly OrganizujProslavuIdentityDbContext dbContext;

        [BindProperty]
        public Oglas Oglas {get; set;}

        [BindProperty]
        public Termin Termin {get; set;}
        [BindProperty]
        public Rezervacija Rezervacija {get; set;}
        [BindProperty]
        public List<SelectListItem> TipProslave {get; set;}
        [BindProperty]
        public List<SlikaOglasa> SlikeOglasa {get; set;}

        public List<ClanBenda> ClanoviBenda {get;set;}

        public List<Karakteristike> Karakteristike {get; set;}
        
        public ProfilOglasaModel(OrganizujProslavuIdentityDbContext db, UserManager<WebApp1User> userManager)
        {
            dbContext = db;
            _userManager = userManager;
        }
        public async Task OnGetAsync(int Id)
        {
           //  Oglas = await dbContext.Oglasi.Where(x=>x.Id==Id).Include(x=>x.Karakteristike)
           //                     .Include(x=>x.ClanoviBenda).Include(x=>x.Korisnik).Include(x=>x.Termini)
            //                    .Include(x=>x.Rezervacije).FirstOrDefaultAsync();
            Oglas = await dbContext.Oglasi.Include(x=>x.Korisnik).Where(x=>x.Id == Id).FirstOrDefaultAsync();

            TipProslave = new List<SelectListItem>
            {
                new SelectListItem { Text="Svadba", Value = "Svadba"},
                new SelectListItem { Text="Krstenje", Value = "Krstenje"},
                new SelectListItem { Text="18.rodjendan", Value = "18.rodjendan"},
                new SelectListItem { Text="Matura", Value = "Matura"},
                new SelectListItem { Text="Zurka", Value = "Zurka"},
                new SelectListItem { Text="Deciji rodjendan", Value = "DecijiRodjendan"},
                new SelectListItem { Text="Proslava", Value = "Proslava"}
            };

            IQueryable<SlikaOglasa> qSlika = dbContext.SlikeOglasa.Where(x=>x.Oglas == Oglas);
            SlikeOglasa = qSlika.ToList();

            IQueryable<ClanBenda> qClan =  dbContext.ClanoviBenda.Where(x=>x.Oglas == Oglas);
            ClanoviBenda = qClan.ToList();

            IQueryable<Karakteristike> qKarakteristika =  dbContext.Karakteristike.Where(x=>x.Oglas == Oglas);
            Karakteristike = qKarakteristika.ToList();
        }


        public async Task<IActionResult> OnPostDodajTerminAsync(int Id)
        {
            Oglas Oglas = await dbContext.Oglasi.FindAsync(Id);
            var user = await _userManager.GetUserAsync(User);
            var sviTermini = dbContext.Termini.Include(x=>x.Oglas).Where(x=>x.Oglas == Oglas).ToList();
            if(Termin.ZauzetTermin >= System.DateTime.Now.AddDays(1)) //minimalno 24h unapred
            {
                foreach(var termin in sviTermini)
                {
                    if(termin.ZauzetTermin < Termin.ZauzetTermin && termin.ZauzetTermin.AddHours(6) > Termin.ZauzetTermin)
                    {
                        TempData["Rezervacija"] = "GreskaT";
                        return RedirectToPage("ProfilOglasa", new {Id = Oglas.Id});
                    }
                }
                Termin.Oglas = Oglas;
                Termin.KorisnikImePrezime = "Samostalni termin";
                Termin.KorisnikBroj = user.PhoneNumber;

                dbContext.Termini.Add(Termin);
                await dbContext.SaveChangesAsync();
                TempData["Rezervacija"] = "Success";
                return RedirectToPage("/ProfilOglasa", new {Id = Oglas.Id});
            }
           else
           {
               TempData["Rezervacija"] = "GreskaProslost";
               return RedirectToPage("/ProfilOglasa", new {Id = Oglas.Id});
           }

        }
        public async Task<IActionResult> OnPostRezervisiAsync(int Id)
        {
            Oglas Oglas = await dbContext.Oglasi.FindAsync(Id);
            var user = await _userManager.GetUserAsync(User);
            if(user.Name == null || user.LastName == null || user.PhoneNumber == null) //ne moze da rezervise oglas ako ne unese informacije
            {
                TempData["Rezervacija"] = "GreskaKorisnik";
                return RedirectToPage( "ProfilOglasa", new {Id = Oglas.Id});
            }
            else
            {
                var sviTermini = dbContext.Termini.Include(x=>x.Oglas).Where(x=>x.Oglas == Oglas).ToList();
                if(Rezervacija.Datum >= System.DateTime.Now.AddDays(1))
                {
                    foreach(var termin in sviTermini)
                    {

                        if(termin.ZauzetTermin < Rezervacija.Datum && termin.ZauzetTermin.AddHours(6) > Rezervacija.Datum)
                        {
                            TempData["Rezervacija"] = "GreskaT";
                            return RedirectToPage( "ProfilOglasa", new {Id = Oglas.Id});
                        }
                    }
                
                    Rezervacija.Oglas = Oglas;
                    Rezervacija.Korisnik = user;
                    
                    Termin.Oglas = Oglas;
                    Termin.ZauzetTermin = Rezervacija.Datum;
                    Termin.Opis = Rezervacija.Opis;
                    Termin.TipProslave = Rezervacija.TipProslave;
                    Termin.BrojGosta = Rezervacija.BrojGosta;
                    Termin.KorisnikImePrezime = user.Name+" "+user.LastName;
                    Termin.KorisnikBroj = user.PhoneNumber;
                    dbContext.Rezervacije.Add(Rezervacija);
                    dbContext.Termini.Add(Termin);
                    await dbContext.SaveChangesAsync();
                    TempData["Rezervacija"] = "Success";
                    return RedirectToPage("/ProfilOglasa", new {Id = Oglas.Id});
                }
                else
                {
                TempData["Rezervacija"] = "GreskaProslost";
                return RedirectToPage("/ProfilOglasa", new {Id = Oglas.Id});
                }
            }
        }
        public async Task<IActionResult> OnPostOtkaziRezervacijuAsync(int IdOglas, int IdRez) //nisu svi termini u rezervacije (ovi privatni)
        {
            Oglas Oglas = await dbContext.Oglasi.FindAsync(IdOglas);
            var termin = await dbContext.Termini.FindAsync(IdRez);
            var rezervacija = await dbContext.Rezervacije.Where(x=>x.Datum == termin.ZauzetTermin && x.Oglas.Id == IdOglas).FirstOrDefaultAsync();
            if(termin!=null)
            {
                if(rezervacija!=null)
                {
                dbContext.Rezervacije.Remove(rezervacija);
                }
                dbContext.Termini.Remove(termin);
                await dbContext.SaveChangesAsync();
                TempData["Rezervacija"] = "SuccessO";
                return RedirectToPage( "ProfilOglasa", new {Id = Oglas.Id});
           }
           else
            {
                TempData["Rezervacija"] = "Greska";
                return RedirectToPage( "ProfilOglasa", new {Id = Oglas.Id});
            }
        }

        public IActionResult OnGetNadjiSveTermine(int Id)
        {
            var sviTermini = dbContext.Termini.Where(x=>x.Oglas.Id == Id).ToList();
            var events = sviTermini.Select(e=> new
            {
                title = "Rezervisano!",
                start = e.ZauzetTermin,
                description = e.Opis,
                brojgosta= e.BrojGosta,
                tipproslave = e.TipProslave,
                vlasnik = e.KorisnikImePrezime,
                broj = e.KorisnikBroj,
                id = e.TerminId
            }).ToList();

            return new JsonResult(events);
        }
    }


}
