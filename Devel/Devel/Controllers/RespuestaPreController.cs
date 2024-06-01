using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devel.Models;

namespace Devel.Controllers
{
    public class RespuestaPreController : Controller
    {
        static string cn = "Data Source=DESKTOP-HMS6GDC\\SQLEXPRESS;Initial Catalog=ProyectoDevel;Integrated Security=True";

        // GET: RespuestaPre
        public ActionResult Index()
        {
            List<RespuestaPre> respuestasPre = new List<RespuestaPre>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM RespuestaPre", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    RespuestaPre respuestaPre = new RespuestaPre
                    {
                        IdResPre = Convert.ToInt32(dr["IdResPre"]),
                        IdResEnc = Convert.ToInt32(dr["IdResEnc"]),
                        IdPreg = Convert.ToInt32(dr["IdPreg"]),
                        Respuesta = dr["Respuesta"].ToString(),
                        IdOpcion = dr["IdOpcion"] != DBNull.Value ? Convert.ToInt32(dr["IdOpcion"]) : (int?)null
                    };
                    respuestasPre.Add(respuestaPre);
                }
            }
            return View(respuestasPre);
        }

        // GET: RespuestaPre/Create
        public ActionResult Create()
        {
            // Here you can fetch questions and options to populate dropdowns or lists in your view
            return View();
        }

        // POST: RespuestaPre/Create
        [HttpPost]
        public ActionResult Create(RespuestaPre respuestaPre)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO RespuestaPre (IdResEnc, IdPreg, Respuesta, IdOpcion) VALUES (@IdResEnc, @IdPreg, @Respuesta, @IdOpcion)", con);
                cmd.Parameters.AddWithValue("IdResEnc", respuestaPre.IdResEnc);
                cmd.Parameters.AddWithValue("IdPreg", respuestaPre.IdPreg);
                cmd.Parameters.AddWithValue("Respuesta", respuestaPre.Respuesta);
                cmd.Parameters.AddWithValue("IdOpcion", respuestaPre.IdOpcion ?? (object)DBNull.Value); // Convert null to DBNull.Value
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: RespuestaPre/Edit/5
        public ActionResult Edit(int id)
        {
            RespuestaPre respuestaPre = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM RespuestaPre WHERE IdResPre = @IdResPre", con);
                cmd.Parameters.AddWithValue("IdResPre", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    respuestaPre = new RespuestaPre
                    {
                        IdResPre = Convert.ToInt32(dr["IdResPre"]),
                        IdResEnc = Convert.ToInt32(dr["IdResEnc"]),
                        IdPreg = Convert.ToInt32(dr["IdPreg"]),
                        Respuesta = dr["Respuesta"].ToString(),
                        IdOpcion = dr["IdOpcion"] != DBNull.Value ? Convert.ToInt32(dr["IdOpcion"]) : (int?)null
                    };
                }
            }
            if (respuestaPre == null)
            {
                return HttpNotFound();
            }
            return View(respuestaPre);
        }

        // POST: RespuestaPre/Edit/5
        [HttpPost]
        public ActionResult Edit(RespuestaPre respuestaPre)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("UPDATE RespuestaPre SET IdResEnc = @IdResEnc, IdPreg = @IdPreg, Respuesta = @Respuesta, IdOpcion = @IdOpcion WHERE IdResPre = @IdResPre", con);
                cmd.Parameters.AddWithValue("IdResEnc", respuestaPre.IdResEnc);
                cmd.Parameters.AddWithValue("IdPreg", respuestaPre.IdPreg);
                cmd.Parameters.AddWithValue("Respuesta", respuestaPre.Respuesta);
                cmd.Parameters.AddWithValue("IdOpcion", respuestaPre.IdOpcion ?? (object)DBNull.Value); // Convert null to DBNull.Value
                cmd.Parameters.AddWithValue("IdResPre", respuestaPre.IdResPre);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: RespuestaPre/Delete/5
        public ActionResult Delete(int id)
        {
            RespuestaPre respuestaPre = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM RespuestaPre WHERE IdResPre = @IdResPre", con);
                cmd.Parameters.AddWithValue("IdResPre", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    respuestaPre = new RespuestaPre
                    {
                        IdResPre = Convert.ToInt32(dr["IdResPre"]),
                        IdResEnc = Convert.ToInt32(dr["IdResEnc"]),
                        IdPreg = Convert.ToInt32(dr["IdPreg"]),
                        Respuesta = dr["Respuesta"].ToString(),
                        IdOpcion = dr["IdOpcion"] != DBNull.Value ? Convert.ToInt32(dr["IdOpcion"]) : (int?)null
                    };
                }
            }
            if (respuestaPre == null)
            {
                return HttpNotFound();
            }
            return View(respuestaPre);
        }

        // POST: RespuestaPre/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM RespuestaPre WHERE IdResPre = @IdResPre", con);
                cmd.Parameters.AddWithValue("IdResPre", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
