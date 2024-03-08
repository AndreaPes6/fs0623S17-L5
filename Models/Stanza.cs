using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelSoloRicchi.Models
{
    public class Stanza
    {
        [Key]
        public int Id { get; set; }

        public bool Tipologia { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        [StringLength(300, ErrorMessage = "La descrizione deve essere lunga al massimo 300 caratteri.")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Il numero stanza è obbligatorio.")]
        public int NumeroStanza { get; set; }
    }
}