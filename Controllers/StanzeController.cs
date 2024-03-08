using HotelSoloRicchi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelSoloRicchi.Controllers
{
    public class StanzeController : Controller
    {
        public ActionResult Index()
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();

            var command = new SqlCommand(@"
                SELECT *
                FROM Stanze
            "
            , conn);
            var reader = command.ExecuteReader();

            List<Stanza> stanze = new List<Stanza>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var stanza = new Stanza();
                    stanza.Id = (int)reader["Id"];
                    stanza.Tipologia = (bool)reader["Tipologia"];
                    stanza.Descrizione = (string)reader["Descrizione"];
                    stanza.NumeroStanza = (int)reader["NumeroStanza"];
                    stanze.Add(stanza);
                }
            }

            return View(stanze);
        }
    }
}