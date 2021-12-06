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
    [Authorize(Roles = "Admin,Korisnik,Oglasivac")]
    public class NapraviOglasModel : PageModel
    {
        private readonly UserManager<WebApp1User> _userManager;
        private readonly IWebHostEnvironment _ihostingEnvironment;
        private readonly OrganizujProslavuIdentityDbContext dbContext;
        [BindProperty]
        public Oglas Oglas {get; set;}
        [BindProperty]
        public IList<Karakteristike> Karakteristike {get; set;}
        [BindProperty]
        public IList<ClanBenda> ClanoviBenda {get; set;}
        [BindProperty]
        public bool WifiIsChecked {get; set;}
        [BindProperty]
        public bool KlimaIsChecked {get; set;}
        [BindProperty]
        public bool GrejanjeIsChecked {get; set;}
        [BindProperty]
        public bool PlayStationIsChecked {get; set;}
        [BindProperty]
        public bool DrustveneIgreIsChecked {get; set;}
        [BindProperty]
        public bool PoligoniZaIgranjeIsChecked {get; set;}
        [BindProperty]
        public bool BazenIsChecked {get; set;}
        [BindProperty]
        public bool ParkingIsChecked {get; set;}
        [BindProperty]
        public List<SelectListItem> TipOglasa {get; set;}
        [BindProperty]
        public List<SlikaOglasa> SlikeOglasa {get; set;}

        public NapraviOglasModel(OrganizujProslavuIdentityDbContext db, 
        UserManager<WebApp1User> userManager,
        IWebHostEnvironment ihostingEnvironment)
        {
            dbContext = db;
            _userManager = userManager;
            _ihostingEnvironment = ihostingEnvironment;
        }
        public void OnGet()
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

        }

        public async Task<IActionResult> OnPostAsync(IFormFile []photos)
        {
            foreach (var photo in photos)
            {
                var pathFolder = Path.Combine(this._ihostingEnvironment.WebRootPath, "imagesOglasi"); //,photo.FileName);
                var uniqueName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(pathFolder, uniqueName);
                var stream = new FileStream(filePath, FileMode.Create);
                await photo.CopyToAsync(stream);

                SlikaOglasa slika = new SlikaOglasa();
                slika.PhotoPath = uniqueName;
                slika.Oglas = Oglas;
                SlikeOglasa.Add(slika);
            }


            foreach (var slika in SlikeOglasa)
            {
                if(slika!=null)
                {
                    dbContext.SlikeOglasa.Add(slika);
                }
            }
            if(SlikeOglasa.Count > 0)
            {
                Oglas.NaslovnaSlika = SlikeOglasa[0].PhotoPath;
            }
            else
            {
                if(Oglas.TipOglasa == "Bend")
                    Oglas.NaslovnaSlika = "bend.png";
                else
                    Oglas.NaslovnaSlika = "Objekat.png";
            }

            if(WifiIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Wifi"));
            }

            if(KlimaIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Klima"));
            }

            if(GrejanjeIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Grejanje"));
            }
            
            if(BazenIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Bazen"));
            }
            
             if(PlayStationIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("PlayStation"));
            }
            
             if(DrustveneIgreIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("DrustveneIgre"));
            }
            
             if(PoligoniZaIgranjeIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("PoligoniZaIgranje"));
            }
            
             if(ParkingIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Parking"));
            }

            if(ClanoviBenda.Count > 0)
            {
                foreach(var clan in ClanoviBenda)
                {
                    clan.Oglas = Oglas; //Svaki clan zna kojem oglasu pripada 
                    dbContext.ClanoviBenda.Add(clan);
                }
            }

            foreach(var k in Karakteristike)
            {
                k.Oglas = Oglas;
                dbContext.Karakteristike.Add(k);
            }

            var korisnik = await _userManager.GetUserAsync(User);
            //var user = await _userManager.FindByIdAsync(korisnik.Id);
            Oglas.Korisnik = korisnik;
            dbContext.Oglasi.Add(Oglas);

            var rez = await _userManager.IsInRoleAsync(korisnik,"Korisnik");
            if(rez == true)
            {
                var oduzmi = await _userManager.RemoveFromRoleAsync(korisnik,"Korisnik"); //oduzima mu je
                var dodaj = await _userManager.AddToRoleAsync(korisnik,"Oglasivac");
                if(oduzmi.Succeeded && dodaj.Succeeded)
                {
                    TempData["Oglas"] = "SuccesNapravljen";
                    return RedirectToPage("./MojiOglasi");
                }
            }

            var result = await dbContext.SaveChangesAsync();
            if(result !=0)
            {
                TempData["Oglas"] = "SuccesNapravljen";
                return RedirectToPage("./MojiOglasi");
            }
            else
                return RedirectToPage();
        }
    }
}
