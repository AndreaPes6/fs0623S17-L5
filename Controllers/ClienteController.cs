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
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();

            var command = new SqlCommand(@"
                SELECT *
                FROM Clienti
            ", conn);
            var reader = command.ExecuteReader();

            List<Cliente> clienti = new List<Cliente>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var cliente = new Cliente();
                    cliente.Id = (int)reader["Id"];
                    cliente.Nome = (string)reader["Nome"];
                    cliente.Cognome = (string)reader["Cognome"];
                    cliente.Email = (string)reader["Email"];
                    cliente.CF = (string)reader["CF"];
                    cliente.Provincia = (string)reader["Provincia"];
                    cliente.Città = (string)reader["Città"];
                    cliente.NumeroTelefono = (string)reader["NumeroTelefono"];
                    clienti.Add(cliente);
                }
            }

            return View(clienti);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Clienti
                    (Nome, Cognome, Email, CF, Provincia, Città, NumeroTelefono)
                    OUTPUT INSERTED.ID
                    VALUES (@nome, @cognome, @email, @cf, @provincia, @città, @numerotelefono)
                ", conn);

                command.Parameters.AddWithValue("@nome", cliente.Nome);
                command.Parameters.AddWithValue("@cognome", cliente.Cognome);
                command.Parameters.AddWithValue("@email", cliente.Email);
                command.Parameters.AddWithValue("@cf", cliente.CF);
                command.Parameters.AddWithValue("@provincia", cliente.Provincia);
                command.Parameters.AddWithValue("@città", cliente.Città);
                command.Parameters.AddWithValue("@numerotelefono", cliente.NumeroTelefono);

                var clienteId = command.ExecuteScalar();

                return RedirectToAction("Index", "Cliente", new { id = clienteId });
            }

            return View(cliente);
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
                    FROM Clienti
                    WHERE Id = @clienteid
                ", conn);
                command.Parameters.AddWithValue("@clienteid", id);
                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return HttpNotFound();
                }

                var cliente = new Cliente();
                while (reader.Read())
                {
                    cliente.Id = (int)reader["Id"];
                    cliente.Nome = (string)reader["Nome"];
                    cliente.Cognome = (string)reader["Cognome"];
                    cliente.Email = (string)reader["Email"];
                    cliente.CF = (string)reader["CF"];
                    cliente.Provincia = (string)reader["Provincia"];
                    cliente.Città = (string)reader["Città"];
                    cliente.NumeroTelefono = (string)reader["NumeroTelefono"];
                }
                reader.Close();

                return View(cliente);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    var commandUpdate = new SqlCommand(@"
                        UPDATE Clienti
                        SET Nome = @nome, Cognome = @cognome, Email = @email,
                            CF = @cf, Provincia = @provincia, Città = @città, NumeroTelefono = @numerotelefono
                        WHERE Id = @clienteid
                    ", conn);
                    commandUpdate.Parameters.AddWithValue("@nome", cliente.Nome);
                    commandUpdate.Parameters.AddWithValue("@cognome", cliente.Cognome);
                    commandUpdate.Parameters.AddWithValue("@email", cliente.Email);
                    commandUpdate.Parameters.AddWithValue("@cf", cliente.CF);
                    commandUpdate.Parameters.AddWithValue("@provincia", cliente.Provincia);
                    commandUpdate.Parameters.AddWithValue("@città", cliente.Città);
                    commandUpdate.Parameters.AddWithValue("@numerotelefono", cliente.NumeroTelefono);
                    commandUpdate.Parameters.AddWithValue("@clienteid", cliente.Id);

                    commandUpdate.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            return View(cliente);
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
            DELETE FROM Clienti
            WHERE Id = @clienteid
        ", conn);
                command.Parameters.AddWithValue("@clienteid", id);

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
