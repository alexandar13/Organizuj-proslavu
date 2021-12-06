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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OrganizujProslavu.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelModel : PageModel
    {
        private readonly UserManager<WebApp1User> _userManager;
        private readonly SignInManager<WebApp1User> _signInManager;
        
        [BindProperty]
        public IList<WebApp1User> Admini {get; set;}
        [BindProperty]
        public IList<WebApp1User> Oglasivaci {get; set;}
        [BindProperty]
        public IList<WebApp1User> Korisnici {get; set;}
        

        public AdminPanelModel(SignInManager<WebApp1User> signInManager, 
            UserManager<WebApp1User> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task OnGetAsync()
        {
            Admini =  await _userManager.GetUsersInRoleAsync("Admin");
            Oglasivaci = await _userManager.GetUsersInRoleAsync("Oglasivac");
            Korisnici =  await _userManager.GetUsersInRoleAsync("Korisnik");
        }
        public async Task<IActionResult> OnPostObrisiAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                TempData["Message"] = "Korisnik nije pronadjen!";
                TempData["Tip"] = "Error";
                return Page();
            }
            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                TempData["Tip"] = "Success";
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDodajAdminaAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                TempData["Message"] = "Korisnik nije pronadjen!";
                TempData["Tip"] = "Error";
                return Page();
            }
            IList<string> uloge = await _userManager.GetRolesAsync(user); //trazi trenutnu ulogu
            await _userManager.RemoveFromRoleAsync(user,uloge[0]); //oduzima mu je

            var result = await _userManager.AddToRoleAsync(user,"Admin"); //dodeljuje admina
            if(result.Succeeded)
            {
                TempData["Tip"] = "SuccessA";
                return RedirectToPage();
            }
            return Page();

        }

        public async Task<IActionResult> OnPostUkloniAdminaAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                TempData["Message"] = "Korisnik nije pronadjen!";
                TempData["Tip"] = "Error";
                return Page();
            }
            await _userManager.RemoveFromRoleAsync(user,"Admin");
            var result = await _userManager.AddToRoleAsync(user,"Korisnik");
            if(result.Succeeded)
            {
                TempData["Tip"] = "SuccessU";
                return RedirectToPage();
            }
            return Page();

        }
    }
}
