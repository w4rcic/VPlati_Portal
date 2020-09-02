using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using VPlati.Models;

namespace VPlati.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Plezalec> _signInManager;
        private readonly UserManager<Plezalec> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Plezalec> userManager,
            SignInManager<Plezalec> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Email je obvezen podatek.")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Uporabniško ime je obvezen podatek.")]
            [Display(Name = "Uporabniško ime")]
            [StringLength(15, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 3)]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Ime je obvezen podatek.")]
            [Display(Name = "Ime")]
            [StringLength(15, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 3)]
            public string Ime { get; set; }

            [Required(ErrorMessage = "Priimek je obvezen podatek.")]
            [Display(Name = "Priimek")]
            [StringLength(15, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 3)]
            public string Priimek { get; set; }

            [Required(ErrorMessage = "Vnesite geslo.")]
            [StringLength(100, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Geslo")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Izberite datum rojstva")]
            public DateTime DatumRojstva { get; set; }

            [Display(Name = "O meni")]
            [StringLength(300, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 15)]
            public string OMeni { get; set; }

            [Required(ErrorMessage = "Vnesite potrditveno geslo.")]
            [DataType(DataType.Password)]
            [Display(Name = "Potrdi geslo")]
            [Compare("Password", ErrorMessage = "Gesli se ne ujemata")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Plezalec 
                { 
                    UserName = Input.UserName, 
                    Email = Input.Email, 
                    EmailConfirmed = true, 
                    DatumRegistracije=DateTime.UtcNow, 
                    DatumRojstva=Input.DatumRojstva, 
                    PlezalecInfo=Input.OMeni, 
                    Ime=Input.Ime, 
                    Priimek=Input.Priimek,
                    SlikaPlezalca= "images/DefaultUser.jpg"
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "user");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
