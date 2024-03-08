using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelSoloRicchi.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Min 3, max 20 caratteri")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(30, MinimumLength = 12, ErrorMessage = "Min 8, max 15 caratteri")]

        public string Password { get; set; }

    }
}