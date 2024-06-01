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
    public class EncuestaController : Controller
    {
        static string cn = "Data Source=DESKTOP-HMS6GDC\\SQLEXPRESS;Initial Catalog=ProyectoDevel;Integrated Security=True";

        // GET: Encuesta
        public ActionResult Index()
        {
            List<Encuesta> encuestas = new List<Encuesta>();
            int idUsuarioCre = Convert.ToInt32(Session["Id"]); // Obtén el IdUsuario de la sesión

            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Encuesta WHERE IdUsuarioCre = @IdUsuarioCre", con);
                cmd.Parameters.AddWithValue("@IdUsuarioCre", idUsuarioCre); // Añade el parámetro para filtrar por IdUsuarioCre
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta
                    {
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        IdUsuarioCre = Convert.ToInt32(dr["IdUsuarioCre"]),
                        Titulo = dr["Titulo"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    };
                    encuestas.Add(encuesta);
                }
            }
            Session["IdEncuesta"] = null;
            return View(encuestas);
        }

        // GET: Encuesta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encuesta/Create
        [HttpPost]
        public ActionResult Create(Encuesta encuesta)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Encuesta (IdUsuarioCre, Titulo, Descripcion) VALUES (@IdUsuarioCre, @Titulo, @Descripcion)", con);
                cmd.Parameters.AddWithValue("IdUsuarioCre", Session["Id"]);
                cmd.Parameters.AddWithValue("Titulo", encuesta.Titulo);
                cmd.Parameters.AddWithValue("Descripcion", encuesta.Descripcion);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Encuesta/Edit/5
        public ActionResult Edit(int id)
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
            return View(encuesta);
        }

        // POST: Encuesta/Edit/5
        [HttpPost]
        public ActionResult Edit(Encuesta encuesta)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Encuesta SET Titulo = @Titulo, Descripcion = @Descripcion WHERE IdEncuesta = @IdEncuesta", con);
                cmd.Parameters.AddWithValue("Titulo", encuesta.Titulo);
                cmd.Parameters.AddWithValue("Descripcion", encuesta.Descripcion);
                cmd.Parameters.AddWithValue("IdEncuesta", encuesta.IdEncuesta);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Encuesta/Delete/5
        public ActionResult Delete(int id)
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
            return View(encuesta);
        }

        // POST: Encuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Encuesta WHERE IdEncuesta = @IdEncuesta", con);
                cmd.Parameters.AddWithValue("IdEncuesta", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

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



        public ActionResult Compartir(int id)
        {
            var url = Url.Action("Responder", "Encuesta", new { id }, protocol: Request.Url.Scheme);
            ViewData["Link"] = url;
            return View();
        }

        private List<Pregunta> ObtenerPreguntas(int idEncuesta)
        {
            List<Pregunta> preguntas = new List<Pregunta>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pregunta WHERE IdEncuesta = @IdEncuesta", con);
                cmd.Parameters.AddWithValue("IdEncuesta", idEncuesta);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Pregunta pregunta = new Pregunta
                    {
                        IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Requerido = dr["Requerido"].ToString(),
                        TipoCampo = dr["TipoCampo"].ToString()
                    };
                    preguntas.Add(pregunta);
                }
            }
            return preguntas;
        }

        [HttpPost]
        public ActionResult GuardarRespuestas(int idEncuesta, List<RespuestaPre> respuestas, List<int> idPreguntas)
        {
            int idResEnc = 0;

            using (SqlConnection con = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmdEnc = new SqlCommand("INSERT INTO RespuestaEnc (IdEncuesta) OUTPUT INSERTED.IdResEn VALUES (@IdEncuesta)", con);
                    cmdEnc.Parameters.AddWithValue("@IdEncuesta", idEncuesta);
                    con.Open();

                    idResEnc = (int)cmdEnc.ExecuteScalar();

                    for (int i = 0; i < respuestas.Count; i++)
                    {
                        RespuestaPre respuesta = respuestas[i];
                        int idPregunta = idPreguntas[i]; // Obtener el ID de la pregunta correspondiente

                        SqlCommand cmdPre = new SqlCommand("INSERT INTO RespuestaPre (IdResEnc, IdPreg, Respuesta) VALUES (@IdResEnc, @IdPreg, @Respuesta)", con);
                        cmdPre.Parameters.AddWithValue("@IdResEnc", idResEnc);
                        cmdPre.Parameters.AddWithValue("@IdPreg", idPregunta); // Utilizar el ID de pregunta correspondiente
                        cmdPre.Parameters.AddWithValue("@Respuesta", respuesta.Respuesta ?? (object)DBNull.Value);
                        cmdPre.ExecuteNonQuery();
                    }

                    con.Close();

                    return RedirectToAction("Gracias");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            }
        }


        public ActionResult Responder(int id)
        {
            List<Pregunta> preguntas = new List<Pregunta>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pregunta WHERE IdEncuesta = @IdEncuesta", con);
                cmd.Parameters.AddWithValue("IdEncuesta", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Pregunta pregunta = new Pregunta
                    {
                        IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                        Descripcion = dr["Descripcion"].ToString(),
                        Requerido = dr["Requerido"].ToString(),
                        TipoCampo = dr["TipoCampo"].ToString()
                    };
                    preguntas.Add(pregunta);
                }
            }

            // Obtener las opciones para cada pregunta
            foreach (var pregunta in preguntas)
            {
                pregunta.Opciones = ObtenerOpciones(pregunta.IdPregunta);
            }

            // Pasa el IdEncuesta a través de ViewBag
            ViewBag.IdEncuesta = id;
            return View(preguntas);
        }

        private List<Opcion> ObtenerOpciones(int idPregunta)
        {
            List<Opcion> opciones = new List<Opcion>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Opcion WHERE IdPreg = @IdPreg", con);
                cmd.Parameters.AddWithValue("IdPreg", idPregunta);
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
            return opciones;
        }

        public ActionResult Gracias()
        {
            return View();
        }

        public ActionResult Ver(int idEncuesta)
        {
            List<Pregunta> preguntas = new List<Pregunta>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                // Obtener preguntas
                SqlCommand cmdPreguntas = new SqlCommand("SELECT * FROM Pregunta WHERE IdEncuesta = @IdEncuesta", con);
                cmdPreguntas.Parameters.AddWithValue("@IdEncuesta", idEncuesta);
                con.Open();
                SqlDataReader drPreguntas = cmdPreguntas.ExecuteReader();
                while (drPreguntas.Read())
                {
                    Pregunta pregunta = new Pregunta
                    {
                        IdPregunta = Convert.ToInt32(drPreguntas["IdPregunta"]),
                        IdEncuesta = Convert.ToInt32(drPreguntas["IdEncuesta"]),
                        Descripcion = drPreguntas["Descripcion"].ToString(),
                        Requerido = Convert.ToString(drPreguntas["Requerido"]),
                        TipoCampo = drPreguntas["TipoCampo"].ToString(),
                        Respuestas = new List<RespuestaPre>() // Inicializa la lista de respuestas
                    };
                    preguntas.Add(pregunta);
                }
                drPreguntas.Close();

                // Obtener respuestas y asociarlas a las preguntas
                SqlCommand cmdRespuestas = new SqlCommand(
                    "SELECT rp.IdResPre, rp.IdResEnc, rp.IdPreg, rp.Respuesta " +
                    "FROM RespuestaPre rp " +
                    "JOIN Pregunta p ON rp.IdPreg = p.IdPregunta " +
                    "WHERE p.IdEncuesta = @IdEncuesta", con);
                cmdRespuestas.Parameters.AddWithValue("@IdEncuesta", idEncuesta);
                SqlDataReader drRespuestas = cmdRespuestas.ExecuteReader();
                while (drRespuestas.Read())
                {
                    RespuestaPre respuesta = new RespuestaPre
                    {
                        IdResPre = Convert.ToInt32(drRespuestas["IdResPre"]),
                        IdResEnc = Convert.ToInt32(drRespuestas["IdResEnc"]),
                        IdPreg = Convert.ToInt32(drRespuestas["IdPreg"]),
                        Respuesta = drRespuestas["Respuesta"].ToString()
                    };

                    // Encuentra la pregunta correspondiente y añade la respuesta
                    var pregunta = preguntas.FirstOrDefault(p => p.IdPregunta == respuesta.IdPreg);
                    if (pregunta != null)
                    {
                        pregunta.Respuestas.Add(respuesta);
                    }
                }
            }
            return View(preguntas);
        }



    }
}
