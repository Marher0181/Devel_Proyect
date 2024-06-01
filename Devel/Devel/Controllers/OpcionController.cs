using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devel.Models;
using System.Data.SqlClient;
using System.Data;

namespace Devel.Controllers
{
    public class OpcionController : Controller
    {
        static string cn = "Data Source=DESKTOP-HMS6GDC\\SQLEXPRESS;Initial Catalog=ProyectoDevel;Integrated Security=True";

        // GET: Opcion
        public ActionResult Index()
        {
            List<Opcion> opciones = new List<Opcion>();
            int idPregunta = Convert.ToInt32(Session["IdPregunta"]);
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Opcion WHERE IdPreg = @IdPregunta", con);
                cmd.Parameters.AddWithValue("@IdPregunta", idPregunta);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Opcion opcion = new Opcion
                    {
                        IdOpcion = Convert.ToInt32(dr["IdOpcion"]),
                        IdPreg = Convert.ToInt32(dr["IdPreg"]),
                        Texto = dr["Texto"].ToString()
                    };
                    opciones.Add(opcion);
                }
            }
            return View(opciones);
        }

        // GET: Opcion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opcion/Create
        [HttpPost]
        public ActionResult Create(Opcion opcion)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Opcion (IdPreg, Texto) VALUES (@IdPreg, @Texto)", con);
                cmd.Parameters.AddWithValue("IdPreg", Session["IdPregunta"]);
                cmd.Parameters.AddWithValue("Texto", opcion.Texto);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Opcion/Edit/5
        public ActionResult Edit(int id)
        {
            Opcion opcion = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Opcion WHERE IdOpcion = @IdOpcion", con);
                cmd.Parameters.AddWithValue("IdOpcion", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    opcion = new Opcion
                    {
                        IdOpcion = Convert.ToInt32(dr["IdOpcion"]),
                        IdPreg = Convert.ToInt32(dr["IdPreg"]),
                        Texto = dr["Texto"].ToString()
                    };
                }
            }
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // POST: Opcion/Edit/5
        [HttpPost]
        public ActionResult Edit(Opcion opcion)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Opcion SET IdPreg = @IdPreg, Texto = @Texto WHERE IdOpcion = @IdOpcion", con);
                cmd.Parameters.AddWithValue("IdPreg", opcion.IdPreg);
                cmd.Parameters.AddWithValue("Texto", opcion.Texto);
                cmd.Parameters.AddWithValue("IdOpcion", opcion.IdOpcion);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Opcion/Delete/5
        public ActionResult Delete(int id)
        {
            Opcion opcion = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Opcion WHERE IdOpcion = @IdOpcion", con);
                cmd.Parameters.AddWithValue("IdOpcion", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    opcion = new Opcion
                    {
                        IdOpcion = Convert.ToInt32(dr["IdOpcion"]),
                        IdPreg = Convert.ToInt32(dr["IdPreg"]),
                        Texto = dr["Texto"].ToString()
                    };
                }
            }
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // POST: Opcion/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Opcion WHERE IdOpcion = @IdOpcion", con);
                cmd.Parameters.AddWithValue("IdOpcion", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
