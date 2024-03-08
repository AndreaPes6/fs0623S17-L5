using HotelSoloRicchi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace HotelSoloRicchi.Controllers
{
    public class PrenotazioneController : Controller
    {

        public ActionResult Index()
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            // TODO: fare la paginazione
            var command = new SqlCommand(@"
        SELECT Prenotazione.*, Clienti.Nome, Clienti.Cognome
        FROM Prenotazione
        INNER JOIN Clienti ON Prenotazione.IdCliente = Clienti.Id
            "
            , conn);
            var reader = command.ExecuteReader();

            List<Prenotazione> prenotazioni = new List<Prenotazione>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var prenotazione = new Prenotazione();
                    prenotazione.Id = (int)reader["Id"];
                    prenotazione.IdCliente = (int)reader["IdCliente"];
                    prenotazione.Nome = (string)reader["Nome"];
                    prenotazione.Cognome = (string)reader["Cognome"];
                    prenotazione.IdStanza = (int)reader["IdStanza"];
                    prenotazione.Anno = (int)reader["Anno"];
                    prenotazione.NumeroPrenotazione = (int)reader["NumeroPrenotazione"];
                    prenotazione.DataPrenotazione = (DateTime)reader["DataPrenotazione"];
                    prenotazione.CheckIn = (DateTime)reader["CheckIn"];
                    prenotazione.CheckOut = (DateTime)reader["CheckOut"];
                    prenotazione.Caparra = (decimal)reader["Caparra"];
                    prenotazione.Tariffa = (decimal)reader["Tariffa"];
                    prenotazione.PensioneOmezza = (bool)reader["PensioneOmezza"];
                    prenotazione.Colazione = (bool)reader["Colazione"];
                    prenotazioni.Add(prenotazione);
                }
            }

            return View(prenotazioni);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Prenotazione
                    (IDCliente, IDStanza, Anno, NumeroPrenotazione, DataPrenotazione, CheckIn, CheckOut, Caparra, Tariffa, PensioneOmezza, Colazione)
                    OUTPUT INSERTED.ID
                    VALUES (@IDCliente, @IDStanza, @Anno, @NumeroPrenotazione, @DataPrenotazione)
                ", conn);

                command.Parameters.AddWithValue("@title", post.Title);
                command.Parameters.AddWithValue("@contents", post.Contents);
                command.Parameters.AddWithValue("@categoryId", post.CategoryId);
                command.Parameters.AddWithValue("@authorId", HttpContext.User.Identity.Name);
                var postId = command.ExecuteScalar();

                return RedirectToAction("Show", new { id = postId });
            }

            // TODO: serve anche la lista delle categorie risolvere con TempData?
            return View(post);
        }


    }
}