using HotelSoloRicchi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var command = new SqlCommand(@"
            SELECT * FROM Prenotazione
            WHERE ID = @prenotazioneid
        ", conn);
                command.Parameters.AddWithValue("@prenotazioneid", id);
                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return HttpNotFound();
                }

                var prenotazione = new Prenotazione();
                while (reader.Read())
                {
                    prenotazione.Id = (int)reader["Id"];
                    prenotazione.IdCliente = (int)reader["IdCliente"];
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
                }
                reader.Close();

                return View(prenotazione);
            }
        }


        [HttpPost]
        public ActionResult Edit(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    var commandUpdate = new SqlCommand(@"
                UPDATE Prenotazione
                SET IDCliente = @idcliente, IDStanza = @idstanza, Anno = @anno, NumeroPrenotazione = @numeroprenotazione, DataPrenotazione = @dataprenotazione, CheckIn = @checkin, CheckOut = @checkout, Caparra = @caparra, Tariffa = @tariffa, PensioneOmezza = @pensioneomezza, Colazione = @colazione
                WHERE ID = @prenotazioneid
            ", conn);
                    commandUpdate.Parameters.AddWithValue("@idcliente", prenotazione.IdCliente);
                    commandUpdate.Parameters.AddWithValue("@idstanza", prenotazione.IdStanza);
                    commandUpdate.Parameters.AddWithValue("@anno", prenotazione.Anno);
                    commandUpdate.Parameters.AddWithValue("@numeroprenotazione", prenotazione.NumeroPrenotazione);
                    commandUpdate.Parameters.AddWithValue("@dataprenotazione", prenotazione.DataPrenotazione);
                    commandUpdate.Parameters.AddWithValue("@checkin", prenotazione.CheckIn);
                    commandUpdate.Parameters.AddWithValue("@checkout", prenotazione.CheckOut);
                    commandUpdate.Parameters.AddWithValue("@caparra", prenotazione.Caparra);
                    commandUpdate.Parameters.AddWithValue("@tariffa", prenotazione.Tariffa);
                    commandUpdate.Parameters.AddWithValue("@pensioneomezza", prenotazione.PensioneOmezza);
                    commandUpdate.Parameters.AddWithValue("@colazione", prenotazione.Colazione);
                    commandUpdate.Parameters.AddWithValue("@prenotazioneid", prenotazione.Id);

                    commandUpdate.ExecuteNonQuery();
                }

                return RedirectToAction("Index", "Prenotazione", new { id = prenotazione.Id });
            }
            return View(prenotazione);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var command = new SqlCommand(@"
            DELETE FROM Prenotazione
            WHERE ID = @prenotazioneid
        ", conn);
                command.Parameters.AddWithValue("@prenotazioneid", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Checkout(int id)
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var command = new SqlCommand(@"
            SELECT * FROM Prenotazione
            WHERE ID = @prenotazioneid
        ", conn);
                command.Parameters.AddWithValue("@prenotazioneid", id);

                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return HttpNotFound();
                }

                var prenotazione = new Prenotazione();
                while (reader.Read())
                {
                    prenotazione.Id = (int)reader["Id"];
                    prenotazione.IdCliente = (int)reader["IdCliente"];
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
                }
                reader.Close();

                decimal sommaTotale = prenotazione.Tariffa - prenotazione.Caparra;

                ViewBag.SommaTotale = sommaTotale;

                return View(prenotazione);
            }
        }





    }
}