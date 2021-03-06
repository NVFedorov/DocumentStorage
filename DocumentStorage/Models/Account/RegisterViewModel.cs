﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the RegisterViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// The register view model.
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}