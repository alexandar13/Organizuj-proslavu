using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OrganizujProslavu.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;


namespace OrganizujProslavu.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin,Korisnik,Oglasivac")]
    public class IzmeniModel : PageModel
    {
        private readonly UserManager<WebApp1User> _userManager;
        private readonly SignInManager<WebApp1User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public WebApp1User Korisnik {get; set;}

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public IFormFile Photo {get; set;}

        [BindProperty]
        public string TrenutnaLozinka {get; set;}
        [BindProperty]
        public string NovaLozinka {get; set;}
        [BindProperty]
        public string PonovoNovaLozinka {get; set;}
    
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
        public string Username { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Email")]
        public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Ime")]
            [DataType(DataType.Text)]
            public string Name { get; set; }

            [Display(Name = "Prezime")]
            [DataType(DataType.Text)]
            public string LastName {get; set;}

            [Display(Name = "Grad")]
            [DataType(DataType.Text)]
            public string City { get; set;}

            public string PhotoPath{get; set;}
        }

        public IzmeniModel(
            UserManager<WebApp1User> userManager,
            SignInManager<WebApp1User> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task OnGetAsync(string Id)
        {
            Korisnik = await _userManager.FindByIdAsync(Id);

            Input = new InputModel
            {
                Username = Korisnik.UserName,
                Email = Korisnik.Email,
                PhoneNumber = Korisnik.PhoneNumber,
                Name = Korisnik.Name,
                LastName = Korisnik.LastName,
                City = Korisnik.City,
                PhotoPath = Korisnik.PhotoPath
            };
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(Korisnik.Id);

            if(Photo!=null)
            {
                if(Input.PhotoPath!=null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Input.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                Input.PhotoPath = ProcessUploadedFile();
            }

            Input.Username = await _userManager.GetUserNameAsync(user);

            user.Name = Input.Name;
            user.LastName = Input.LastName;
            user.City = Input.City;
            user.PhotoPath = Input.PhotoPath;

            var mejl = await _userManager.GetEmailAsync(user);
            if(Input.Email != mejl)
            {
                var setEmail = await _userManager.SetEmailAsync(user, Input.Email);
            }
            
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            }
            if(TrenutnaLozinka!=null)
            {
                var tacno = await _userManager.CheckPasswordAsync(user, TrenutnaLozinka);
                if(tacno)
                {
                    if(NovaLozinka == PonovoNovaLozinka)
                    {
                        await _userManager.ChangePasswordAsync(user,TrenutnaLozinka,NovaLozinka);
                    }
                    else
                    {
                        TempData["MessageE"] = "Nove lozinke se ne poklapaju!";
                        TempData["TipE"] = "Error";
                        return RedirectToPage("Izmeni", new {Id = user.Id});
                    }
                }
                else
                {
                    TempData["MessageE"] = "Trenutna lozinka nije tacna!";
                    TempData["TipE"] = "Error";
                    return RedirectToPage("Izmeni", new {Id = user.Id}); 
                   
                }
            }
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                TempData["TipE"] = "Success";
                return RedirectToPage("Izmeni", new {Id = user.Id});
            }
            return RedirectToPage("Izmeni", new {Id = user.Id});
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if(Photo!=null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}