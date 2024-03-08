using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSoloRicchi.Models
{
    public class Prenotazione
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Clienti")]
        public int IdCliente { get; set; }

        [ForeignKey("Stanze")]
        public int IdStanza { get; set; }

        public int Anno { get; set; }

        public int NumeroPrenotazione { get; set; }

        [Display(Name = "Data Prenotazione")]
        [DataType(DataType.Date)]
        public DateTime DataPrenotazione { get; set; }

        [Display(Name = "Check-in")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check-out")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public decimal Caparra { get; set; }

        public decimal Tariffa { get; set; }

        [Display(Name = "Pensione o Mezza")]
        public bool PensioneOmezza { get; set; }

        public bool Colazione { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public int NumeroStanza { get; set; }
    }
}