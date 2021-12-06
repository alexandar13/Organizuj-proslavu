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
    public class IzmeniOglasModel : PageModel
    {
        private readonly UserManager<WebApp1User> _userManager;
        private readonly IWebHostEnvironment _ihostingEnvironment;
        private readonly OrganizujProslavuIdentityDbContext dbContext;
        [BindProperty]
        public Oglas Oglas  {get;set;}
        [BindProperty]
        public IList<ClanBenda> ClanoviBenda  {get;set;}
        [BindProperty]
        public IList<Karakteristike> Karakteristike {get;set;}
        [BindProperty]
        public IList<SlikaOglasa> SlikeOglasa {get;set;}
        [BindProperty]
        public IList<SlikaOglasa> NoveSlike {get; set;}
        [BindProperty]
        public bool WifiIsChecked   {get; set;}
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
        public IList<string> VecPostojece {get; set;}
        [BindProperty]
        public IList<string> ListaImenaClanova {get; set;}
         [BindProperty]
        public IList<string> ListaImenaInstrumenata {get; set;}

        [BindProperty]
        public IList<Karakteristike> KarakteristikeRestorana{get;set;}
         public IzmeniOglasModel(OrganizujProslavuIdentityDbContext db, UserManager<WebApp1User> userManager,IWebHostEnvironment ihostingEnvironment)
        {
            dbContext = db;
            _userManager = userManager;
            _ihostingEnvironment=ihostingEnvironment;
            NoveSlike = new List<SlikaOglasa>();
            VecPostojece = new List<string>();
            ListaImenaClanova=new List<string>();
            ListaImenaInstrumenata=new List<string>();
        }


        public async Task OnGetAsync(int Id)
        {
            Oglas = await dbContext.Oglasi.Include(x=>x.Karakteristike).Include(x=>x.ClanoviBenda).Include(x=>x.SlikeOglasa).Where(x=>x.Id==Id).FirstOrDefaultAsync();

            IQueryable<Karakteristike> qKarakteristike=dbContext.Karakteristike.Where(x=>x.Oglas==Oglas);
            Karakteristike=qKarakteristike.ToList();

            IQueryable<SlikaOglasa> qSlikeOglasa=dbContext.SlikeOglasa.Where(x=>x.Oglas==Oglas);
            SlikeOglasa=qSlikeOglasa.ToList();

            IQueryable<ClanBenda> qClanoviBenda=dbContext.ClanoviBenda.Where(x=>x.Oglas==Oglas);
            ClanoviBenda=qClanoviBenda.ToList();

        }

        public async Task<IActionResult> OnPostSacuvajAsync(IFormFile []photos)
        {
            foreach(var photo in photos)
            { 
                //Upisuje u folder
                var pathFolder=Path.Combine(this._ihostingEnvironment.WebRootPath, "imagesOglasi");
                var uniqueName=Guid.NewGuid().ToString()+"_"+photo.FileName;
                string filePath=Path.Combine(pathFolder,uniqueName);
                var stream=new FileStream(filePath,FileMode.Create);
                await photo.CopyToAsync(stream);

                //Upisuje iz foldera u listu
                SlikaOglasa slika=new SlikaOglasa();
                slika.PhotoPath=uniqueName;
                slika.Oglas=Oglas;
                NoveSlike.Add(slika);     
            } 
            //Iz liste u bazu
            foreach(var slika in NoveSlike)
            {
                if(slika!=null)
                {
                    SlikeOglasa.Add(slika);
                    dbContext.SlikeOglasa.Add(slika);
                }
            }
            
            foreach(var Karakteristika in Karakteristike)
            {
                dbContext.Karakteristike.Remove(Karakteristika);
                Karakteristike.Remove(Karakteristika);
            }

             if(WifiIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Wifi"));
            }
            if(WifiIsChecked == false)
            {
                var k = dbContext.Karakteristike.Where(x=>x.Naziv=="Wifi" && x.Oglas == Oglas).FirstOrDefault();
                if(k!=null)
                {
                dbContext.Karakteristike.Remove(k);
                }
            }
            if(KlimaIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Klima"));
            }
            if(KlimaIsChecked == false)
            {
                var kl = dbContext.Karakteristike.Where(x=>x.Naziv=="Klima" && x.Oglas == Oglas).FirstOrDefault();
                if(kl!=null)
                {
                dbContext.Karakteristike.Remove(kl);
                }
            }
            if(GrejanjeIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Grejanje"));
            }
            if(GrejanjeIsChecked == false)
            {
                var g = dbContext.Karakteristike.Where(x=>x.Naziv=="Grejanje" && x.Oglas == Oglas).FirstOrDefault();
                if(g!=null)
                {
                dbContext.Karakteristike.Remove(g);
                }
            }
            if(BazenIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Bazen"));
            }
            if(BazenIsChecked == false)
            {
                var b = dbContext.Karakteristike.Where(x=>x.Naziv=="Bazen" && x.Oglas == Oglas).FirstOrDefault();
                if(b!=null)
                {
                dbContext.Karakteristike.Remove(b);
                }
            }
             if(PlayStationIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("PlayStation"));
            }
            if(PlayStationIsChecked == false)
            {
                var ps = dbContext.Karakteristike.Where(x=>x.Naziv=="PlayStation" && x.Oglas == Oglas).FirstOrDefault();
                if(ps!=null)
                {
                dbContext.Karakteristike.Remove(ps);
                }
            }
             if(DrustveneIgreIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("DrustveneIgre"));
            }
            if(DrustveneIgreIsChecked == false)
            {
                var i = dbContext.Karakteristike.Where(x=>x.Naziv=="DrustveneIgre" && x.Oglas == Oglas).FirstOrDefault();
                if(i!=null)
                {
                dbContext.Karakteristike.Remove(i);
                }
            }
             if(PoligoniZaIgranjeIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("PoligoniZaIgranje"));
            }
            if(PoligoniZaIgranjeIsChecked == false)
            {
                var po = dbContext.Karakteristike.Where(x=>x.Naziv=="PoligoniZaIgranje" && x.Oglas == Oglas).FirstOrDefault();
                if(po!=null)
                {
                dbContext.Karakteristike.Remove(po);
                }
            }
             if(ParkingIsChecked == true)
            {
                Karakteristike.Add(new Karakteristike("Parking"));
            }
            if(ParkingIsChecked == false)
            {
                var pk = dbContext.Karakteristike.Where(x=>x.Naziv=="Parking" && x.Oglas == Oglas).FirstOrDefault();
                if(pk!=null)
                {
                dbContext.Karakteristike.Remove(pk);
                }
            }
            foreach(string k in VecPostojece)
            {
                Karakteristike.Add(new Karakteristike(k));
            }

            foreach(var k in Karakteristike)
            {
                k.Oglas=Oglas;
                dbContext.Karakteristike.Add(k);
            }

            //CLANOVI BENDA
             foreach(var clan in ClanoviBenda)
            {
                dbContext.ClanoviBenda.Remove(clan);
                ClanoviBenda.Remove(clan);
            }

             if(ListaImenaClanova.Count > 0)
            {
                for(var clan=0;clan<ListaImenaClanova.Count;clan++)
                {
                    if(ListaImenaClanova[clan]!="" && ListaImenaInstrumenata[clan]!="")
                    {
                    ClanBenda clanBenda= new ClanBenda(); 
                    clanBenda.Ime=ListaImenaClanova[clan];
                    clanBenda.Instrument=ListaImenaInstrumenata[clan];
                    clanBenda.Oglas=Oglas;
                    ClanoviBenda.Add(clanBenda);
                    dbContext.ClanoviBenda.Add(clanBenda);
                    }
                }
            }
            try
            {
                dbContext.Attach(Oglas).State=EntityState.Modified;
                await dbContext.SaveChangesAsync();
                TempData["Izmeni"] = "Success";
                return RedirectToPage("IzmeniOglas", new {Id = Oglas.Id});
            }
            catch(DbUpdateException)
            {
                TempData["Izmeni"] = "Error";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostObrisiSlikuAsync(int IdSlike)
        {
            var Slika = await dbContext.SlikeOglasa.Where(x=> x.Id == IdSlike).FirstOrDefaultAsync();
            
            if(Slika!=null)
            {
            dbContext.SlikeOglasa.Remove(Slika);
            SlikeOglasa.Remove(Slika);

            await dbContext.SaveChangesAsync();
            return RedirectToPage("IzmeniOglas", new {Id = Oglas.Id});
            }
            return RedirectToPage("IzmeniOglas", new {Id = Oglas.Id});
        }

        public async Task<IActionResult> OnPostNaslovnaSlikaAsync(int Id)
        {
            var Slika = await dbContext.SlikeOglasa.Where(x=> x.Id == Id).FirstOrDefaultAsync();

            Oglas.NaslovnaSlika = Slika.PhotoPath;

            await dbContext.SaveChangesAsync();

            return RedirectToPage("IzmeniOglas", new {Id = Oglas.Id});
        }
         public async Task<IActionResult> OnPostObrisiClanaAsync(int IdClan)
        {
            var clan = await dbContext.ClanoviBenda.Where(x=> x.ClanBendaId == IdClan).FirstOrDefaultAsync();
            
            if(clan!=null)
            {
            dbContext.ClanoviBenda.Remove(clan);
            ClanoviBenda.Remove(clan);

            dbContext.Attach(Oglas).State=EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return RedirectToPage("IzmeniOglas", new {Id = Oglas.Id});
            }
            return RedirectToPage("IzmeniOglas", new {Id = Oglas.Id});
        }

        
    }
}
