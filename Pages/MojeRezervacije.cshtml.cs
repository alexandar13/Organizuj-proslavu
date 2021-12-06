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
    public class MojeRezervacijeModel : PageModel
    {
        private readonly UserManager<WebApp1User> _userManager;
        private readonly OrganizujProslavuIdentityDbContext dbContext;
        [BindProperty]
        public IList<Rezervacija> Rezervacije {get; set;}

        public MojeRezervacijeModel(OrganizujProslavuIdentityDbContext db, UserManager<WebApp1User> userManager)
        {
            dbContext = db;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            IQueryable<Rezervacija> qRezervacija = dbContext.Rezervacije.Include(x=>x.Oglas).Include(x=>x.Korisnik).Where(x=>x.Korisnik == user)
                                                    .OrderBy(x=>x.Datum);

            Rezervacije = qRezervacija.ToList();
        }
        public async Task<IActionResult> OnPostOtkaziRezervacijuAsync(int Id)
        {
            var rezervacija = await dbContext.Rezervacije.FindAsync(Id);
            var termin = await dbContext.Termini.Where(x=>x.ZauzetTermin == rezervacija.Datum).FirstOrDefaultAsync();
            if(rezervacija!=null && termin!=null)
            {
                dbContext.Rezervacije.Remove(rezervacija);
                dbContext.Termini.Remove(termin);
                await dbContext.SaveChangesAsync();
                TempData["MRezervacija"] = "Success";
                return RedirectToPage();
            }
            else
            {
                TempData["MRezervacija"] = "Greska";
                return RedirectToPage();
            }
        }
    }
}