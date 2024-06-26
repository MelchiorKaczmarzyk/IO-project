﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IOProject.CustomValidation;
using IOProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace IOProject.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly SignInManager<SystemUser> _signInManager;
        public List<Checkbox> checkboxes { get; set; }
        public IndexModel(
            UserManager<SystemUser> userManager,
            SignInManager<SystemUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [ValidationTags(ErrorMessage = "Select at least one tag")]
            public List<string> Tags;
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(SystemUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
             
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };

            var currentUser = _userManager.Users.FirstOrDefault(u => 
            u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userTags = currentUser.tags;
            checkboxes = new List<Checkbox>
        {
            new Checkbox()
            {
                isChecked = false,
                description = "Medical procedure"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Rehabilitation"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Natural disasters"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Help for refugees"
            },
            new Checkbox()
            {
                isChecked = false,
                description = "Cultural event"
            }
        };
            foreach (var checkbox in checkboxes)
            {
                if (userTags.Contains(checkbox.description))
                {
                    checkbox.isChecked = true;
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        public Task setTagsAsync(SystemUser user, List<string> Tags)
        {
            user.tags = Tags;
            return Task.CompletedTask;
        }
            public async Task<IActionResult> OnPostAsync(List<string> Tags)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (Tags.IsNullOrEmpty())
                ModelState.AddModelError(string.Empty, "Select at leas one tag");

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            await setTagsAsync(user, Tags);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.SignInAsync(user, true);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
