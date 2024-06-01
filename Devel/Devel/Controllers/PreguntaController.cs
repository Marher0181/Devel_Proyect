using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Devel.Models;

namespace Devel.Controllers
{
    public class PreguntaController : Controller
    {
        static string cn = "Data Source=DESKTOP-HMS6GDC\\SQLEXPRESS;Initial Catalog=ProyectoDevel;Integrated Security=True";

        // GET: Pregunta
        public ActionResult Index()
        {
            List<Pregunta> preguntas = new List<Pregunta>();
            int idEncuesta = Convert.ToInt32(Session["IdEncuesta"]);
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pregunta WHERE IdEncuesta = @IdEncuesta", con);
                cmd.Parameters.AddWithValue("@IdEncuesta", idEncuesta);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Pregunta pregunta = new Pregunta
                    {
                        IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Requerido = Convert.ToString(dr["Requerido"]),
                        TipoCampo = dr["TipoCampo"].ToString()
                    };
                    preguntas.Add(pregunta);
                    Session["IdPregunta"] = null;
                }
            }
            return View(preguntas);
        }

        // GET: Pregunta/Create
        public ActionResult Create()
        {
            // Obtener todas las encuestas disponibles para seleccionar en la vista
            List<Encuesta> encuestas = new List<Encuesta>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Encuesta", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta
                    {
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Titulo = dr["Titulo"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    };
                    encuestas.Add(encuesta);
                }
            }

            ViewBag.Encuestas = encuestas;
            return View();
        }
        // POST: Pregunta/Create
        [HttpPost]
        public ActionResult Create(Pregunta pregunta)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cn))
                {
                    if (pregunta.Requerido != null && pregunta.TipoCampo != null)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Pregunta (IdEncuesta, Descripcion, Requerido, TipoCampo) VALUES (@IdEncuesta, @Descripcion, @Requerido, @TipoCampo)", con);
                        cmd.Parameters.AddWithValue("IdEncuesta", Session["IdEncuesta"]);
                        cmd.Parameters.AddWithValue("Descripcion", pregunta.Descripcion);
                        cmd.Parameters.AddWithValue("Requerido", pregunta.Requerido);
                        cmd.Parameters.AddWithValue("TipoCampo", pregunta.TipoCampo);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Si alguno de los campos requeridos está vacío, establece un mensaje de error
                        ViewData["ErrorMessage"] = "Todos los campos son obligatorios.";
                        return View();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Si se produce una excepción, establece un mensaje de error basado en la excepción
                ViewData["ErrorMessage"] = "Ha ocurrido un error al crear la pregunta: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Pregunta/Edit/5
        public ActionResult Edit(int id)
        {
            Pregunta pregunta = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pregunta WHERE IdPregunta = @IdPregunta", con);
                cmd.Parameters.AddWithValue("IdPregunta", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    pregunta = new Pregunta
                    {
                        IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Requerido = Convert.ToString(dr["Requerido"]),
                        TipoCampo = dr["TipoCampo"].ToString()
                    };
                }
            }
            if (pregunta == null)
            {
                return HttpNotFound();
            }

            // Obtener todas las encuestas disponibles para seleccionar en la vista
            List<Encuesta> encuestas = new List<Encuesta>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Encuesta", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta
                    {
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Titulo = dr["Titulo"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    };
                    encuestas.Add(encuesta);
                }
            }

            ViewBag.Encuestas = encuestas;
            return View(pregunta);
        }

        // POST: Pregunta/Edit/5
        [HttpPost]
        public ActionResult Edit(Pregunta pregunta)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Pregunta SET Descripcion = @Descripcion, Requerido = @Requerido, TipoCampo = @TipoCampo WHERE IdPregunta = @IdPregunta", con);
                cmd.Parameters.AddWithValue("Descripcion", pregunta.Descripcion);
                cmd.Parameters.AddWithValue("Requerido", pregunta.Requerido);
                cmd.Parameters.AddWithValue("TipoCampo", pregunta.TipoCampo);
                cmd.Parameters.AddWithValue("IdPregunta", pregunta.IdPregunta);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Pregunta/Delete/5
        public ActionResult Delete(int id)
        {
            Pregunta pregunta = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pregunta WHERE IdPregunta = @IdPregunta", con);
                cmd.Parameters.AddWithValue("IdPregunta", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    pregunta = new Pregunta
                    {
                        IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Requerido = Convert.ToString(dr["Requerido"]),
                        TipoCampo = dr["TipoCampo"].ToString()
                    };
                }
            }
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // POST: Pregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Pregunta WHERE IdPregunta = @IdPregunta", con);
                cmd.Parameters.AddWithValue("IdPregunta", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Pregunta pregunta = null;
            using (SqlConnection conn = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("Select * From Pregunta Where IdPregunta = @IdPregunta", conn);
                cmd.Parameters.AddWithValue("IdPregunta", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    pregunta = new Pregunta
                    {
                        IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Requerido = dr["Requerido"].ToString(),
                        TipoCampo = dr["TipoCampo"].ToString()
                    };
                }
            } 
            if(pregunta == null)
            {
                return HttpNotFound();
            }
            Session["IdPregunta"] = pregunta.IdPregunta;
            return View(pregunta);
        }
    }

    // GET: Encuesta/Details/5
 
    /*
             // GET: Encuesta/Details/5
        public ActionResult Details(int id)
        {
            Encuesta encuesta = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Encuesta WHERE IdEncuesta = @IdEncuesta", con);
                cmd.Parameters.AddWithValue("IdEncuesta", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    encuesta = new Encuesta
                    {
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        IdUsuarioCre = Convert.ToInt32(dr["IdUsuarioCre"]),
                        Titulo = dr["Titulo"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    };
                }
            }
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            Session["IdEncuesta"] = encuesta.IdEncuesta;
            return View(encuesta);
        }
     */
}
