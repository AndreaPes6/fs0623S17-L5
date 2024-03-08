using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelSoloRicchi.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(30, ErrorMessage = "Il nome deve essere lungo al massimo 30 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(30, ErrorMessage = "Il cognome deve essere lungo al massimo 30 caratteri.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "L'email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "Il formato dell'email non è valido.")]
        [StringLength(35, ErrorMessage = "L'email deve essere lunga al massimo 35 caratteri.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
        [StringLength(16, ErrorMessage = "Il codice fiscale deve essere lungo esattamente 16 caratteri.")]
        public string CF { get; set; }

        [Required(ErrorMessage = "La provincia è obbligatoria.")]
        [StringLength(20, ErrorMessage = "La provincia deve essere lunga al massimo 20 caratteri.")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "La città è obbligatoria.")]
        [StringLength(25, ErrorMessage = "La città deve essere lunga al massimo 25 caratteri.")]
        public string Città { get; set; }

        [StringLength(15, ErrorMessage = "Il numero di telefono deve essere lungo al massimo 15 caratteri.")]
        public string NumeroTelefono { get; set; }
    }
}