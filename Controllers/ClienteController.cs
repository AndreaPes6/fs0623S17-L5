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
            "
            , conn);
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
    }
}