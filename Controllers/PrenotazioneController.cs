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

        public ActionResult Add()
        {
            return View();
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
                    VALUES (@idcliente, @idstanza, @anno, @numeroprenotazione, @dataprenotazione, @checkin, @checkout, @caparra, @tariffa, @pensioneomezza, @colazione)
                ", conn);

                command.Parameters.AddWithValue("@idcliente", prenotazione.IdCliente);
                command.Parameters.AddWithValue("@idstanza", prenotazione.IdStanza);
                command.Parameters.AddWithValue("@anno", prenotazione.Anno);
                command.Parameters.AddWithValue("@numeroprenotazione", prenotazione.NumeroPrenotazione);
                command.Parameters.AddWithValue("@dataprenotazione", prenotazione.DataPrenotazione);
                command.Parameters.AddWithValue("@checkin", prenotazione.CheckIn);
                command.Parameters.AddWithValue("@checkout", prenotazione.CheckOut);
                command.Parameters.AddWithValue("@caparra", prenotazione.Caparra);
                command.Parameters.AddWithValue("@tariffa", prenotazione.Tariffa);
                command.Parameters.AddWithValue("@pensioneomezza", prenotazione.PensioneOmezza);
                command.Parameters.AddWithValue("@colazione", prenotazione.Colazione);

                var prenotazioneId = command.ExecuteScalar();

                return RedirectToAction("Index", "Prenotazione", new { id = prenotazioneId });
            }

            return View(prenotazione);
        }


    }
}