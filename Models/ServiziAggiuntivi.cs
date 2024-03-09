using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelSoloRicchi.Models
{
    public class ServiziAggiuntivi
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("IDprenotazione")]
        public int IDPrenotazione { get; set; }
        public string Servizio { get; set; }
        public int Quantità { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public decimal Prezzo { get; set; }


    }
}