﻿using System.ComponentModel.DataAnnotations;

namespace ToyShopAPI.Models
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
