using HotelSoloRicchi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelSoloRicchi.Controllers
{
    public class ServiziAggiuntiviController : Controller
    {
        public ActionResult Index()
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand(@"
        SELECT *
        FROM ServiziAggiuntivi
    ", conn);
            var reader = command.ExecuteReader();

            List<ServiziAggiuntivi> serviziAggiuntivi = new List<ServiziAggiuntivi>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var servizioAggiuntivo = new ServiziAggiuntivi();
                    servizioAggiuntivo.ID = (int)reader["ID"];
                    servizioAggiuntivo.IDPrenotazione = (int)reader["IDPrenotazione"];
                    servizioAggiuntivo.Servizio = (string)reader["Servizio"];
                    servizioAggiuntivo.Quantità = (int)reader["Quantità"];
                    servizioAggiuntivo.Data = (DateTime)reader["Data"];
                    servizioAggiuntivo.Prezzo = (decimal)reader["Prezzo"];
                    serviziAggiuntivi.Add(servizioAggiuntivo);
                }
            }

            return View(serviziAggiuntivi);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ServiziAggiuntivi serviziaggiuntivi)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var command = new SqlCommand(@"
                INSERT INTO ServiziAggiuntivi
                (IDPrenotazione, Servizio, Quantità, Data, Prezzo)
                OUTPUT INSERTED.ID
                VALUES (@idprenotazione, @servizio, @quantita, @data, @prezzo)
            ", conn);

                    command.Parameters.AddWithValue("@idprenotazione", serviziaggiuntivi.IDPrenotazione);
                    command.Parameters.AddWithValue("@servizio", serviziaggiuntivi.Servizio);
                    command.Parameters.AddWithValue("@quantita", serviziaggiuntivi.Quantità);
                    command.Parameters.AddWithValue("@data", serviziaggiuntivi.Data);
                    command.Parameters.AddWithValue("@prezzo", serviziaggiuntivi.Prezzo);

                    var servizioId = command.ExecuteScalar();

                    return RedirectToAction("Index", "ServiziAggiuntivi", new { id = servizioId });
                }
            }

            return View(serviziaggiuntivi);
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
                    SELECT *
                    FROM ServiziAggiuntivi
                    WHERE ID = @ServizioID
                ", conn);
                command.Parameters.AddWithValue("@ServizioID", id);
                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return HttpNotFound();
                }

                var servizio = new ServiziAggiuntivi();
                while (reader.Read())
                {
                    servizio.ID = (int)reader["ID"];
                    servizio.IDPrenotazione = (int)reader["IDPrenotazione"];
                    servizio.Servizio = (string)reader["Servizio"];
                    servizio.Quantità = (int)reader["Quantità"];
                    servizio.Data = (DateTime)reader["Data"];
                    servizio.Prezzo = (decimal)reader["Prezzo"];
                }
                reader.Close();

                return View(servizio);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiziAggiuntivi serviziaggiuntivi)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    var commandUpdate = new SqlCommand(@"
                 UPDATE ServiziAggiuntivi
                 SET IDPrenotazione = @idprenotazione, Servizio = @servizio, Quantità = @quantità, Data = @data, Prezzo = @prezzo
                 WHERE ID = @ID
             ", conn);
                    commandUpdate.Parameters.AddWithValue("@idprenotazione", serviziaggiuntivi.IDPrenotazione);
                    commandUpdate.Parameters.AddWithValue("@servizio", serviziaggiuntivi.Servizio);
                    commandUpdate.Parameters.AddWithValue("@quantità", serviziaggiuntivi.Quantità);
                    commandUpdate.Parameters.AddWithValue("@data", serviziaggiuntivi.Data);
                    commandUpdate.Parameters.AddWithValue("@prezzo", serviziaggiuntivi.Prezzo);
                    commandUpdate.Parameters.AddWithValue("@ID", serviziaggiuntivi.ID);

                    commandUpdate.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            return View(serviziaggiuntivi);
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
            DELETE FROM ServiziAggiuntivi
            WHERE ID = @ServizioID
        ", conn);
                command.Parameters.AddWithValue("@ServizioID", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index");
            }
        }
    }
}