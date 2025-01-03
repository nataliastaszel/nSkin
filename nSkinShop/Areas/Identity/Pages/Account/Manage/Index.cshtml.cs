// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nSkinShop.Models;

namespace nSkinShop.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [DisplayName("Email")]
        public string Username { get; set; }
        
        [TempData]
        public string StatusMessage { get; set; }
        
        [BindProperty]
        public InputModel Input { get; set; }
        
        public class InputModel
        {
            [Phone]
            [DisplayName("Phone number")]
            public string PhoneNumber { get; set; }
            
            public string? Name { get; set; }
            
            [DisplayName("Street address")]
            public string? StreetAddress { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            
            [Display(Name = "Postal code")]
            public string? ZipCode { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;


            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Name = user.Name,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                ZipCode = user.PostalCode
                
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User) as ApplicationUser;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User) as ApplicationUser;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            
            user.PhoneNumber = Input.PhoneNumber;
            user.Name = Input.Name;
            user.StreetAddress = Input.StreetAddress;
            user.City = Input.City;
            user.State = Input.State;
            user.PostalCode = Input.ZipCode;
            
            
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update user.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
        
    }
}