﻿using System.ComponentModel.DataAnnotations;

namespace PCstore.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}