﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace TrisGPOI.Database.User.Entities
{
    public class DBUser
    {
        [Key]
        public string Email { get; set; } = "";
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        public bool IsActive { get; set; }
        public string Status { get; set; } = "Offline";
        public string FotoProfilo { get; set; } = "Default";
        public string Description { get; set; } = "";

        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;

        public int MoneyXO { get; set; } = 0;
    }
}