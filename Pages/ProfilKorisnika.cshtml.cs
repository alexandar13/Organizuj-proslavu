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
    public class ProfilKorisnikaModel : PageModel
    {

        private readonly UserManager<WebApp1User> _userManager;
        private readonly OrganizujProslavuIdentityDbContext dbContext;

        [BindProperty]
        public IList<Oglas> Oglasi {get; set;}
        [BindProperty]
        public WebApp1User Korisnik {get; set;}

        public ProfilKorisnikaModel(OrganizujProslavuIdentityDbContext db, UserManager<WebApp1User> userManager)
        {
            dbContext = db;
            _userManager = userManager;
        }
        public async Task OnGetAsync(string Id)
        {
            Korisnik = await _userManager.FindByIdAsync(Id);

            IQueryable<Oglas> qOglas = dbContext.Oglasi.Include(x=>x.Karakteristike).Include(x=>x.ClanoviBenda)
                                        .Where(x=>x.Korisnik == Korisnik);

            Oglasi = qOglas.ToList();
        }
    }
}
