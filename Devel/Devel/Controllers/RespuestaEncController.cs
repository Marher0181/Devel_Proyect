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
    public class RespuestaEncController : Controller
    {
        static string cn = "Data Source=DESKTOP-HMS6GDC\\SQLEXPRESS;Initial Catalog=ProyectoDevel;Integrated Security=True";

        // GET: RespuestaEnc
        public ActionResult Index()
        {
            List<RespuestaEnc> respuestasEnc = new List<RespuestaEnc>();
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM RespuestaEnc", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    RespuestaEnc respuestaEnc = new RespuestaEnc
                    {
                        IdResEn = Convert.ToInt32(dr["IdResEn"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"]),
                    };
                    respuestasEnc.Add(respuestaEnc);
                }
            }
            return View(respuestasEnc);
        }

        // GET: RespuestaEnc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RespuestaEnc/Create
        [HttpPost]
        public ActionResult Create(RespuestaEnc respuestaEnc)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO RespuestaEnc (IdEncuesta, IdUsuario) VALUES (@IdEncuesta, @IdUsuario)", con);
                cmd.Parameters.AddWithValue("IdEncuesta", respuestaEnc.IdEncuesta);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: RespuestaEnc/Edit/5
        public ActionResult Edit(int id)
        {
            RespuestaEnc respuestaEnc = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM RespuestaEnc WHERE IdResEn = @IdResEn", con);
                cmd.Parameters.AddWithValue("IdResEn", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    respuestaEnc = new RespuestaEnc
                    {
                        IdResEn = Convert.ToInt32(dr["IdResEn"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"])
                    };
                }
            }
            if (respuestaEnc == null)
            {
                return HttpNotFound();
            }
            return View(respuestaEnc);
        }

        // POST: RespuestaEnc/Edit/5
        [HttpPost]
        public ActionResult Edit(RespuestaEnc respuestaEnc)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("UPDATE RespuestaEnc SET IdEncuesta = @IdEncuesta, IdUsuario = @IdUsuario WHERE IdResEn = @IdResEn", con);
                cmd.Parameters.AddWithValue("IdEncuesta", respuestaEnc.IdEncuesta);
                cmd.Parameters.AddWithValue("IdResEn", respuestaEnc.IdResEn);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: RespuestaEnc/Delete/5
        public ActionResult Delete(int id)
        {
            RespuestaEnc respuestaEnc = null;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM RespuestaEnc WHERE IdResEn = @IdResEn", con);
                cmd.Parameters.AddWithValue("IdResEn", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    respuestaEnc = new RespuestaEnc
                    {
                        IdResEn = Convert.ToInt32(dr["IdResEn"]),
                        IdEncuesta = Convert.ToInt32(dr["IdEncuesta"])
                    };
                }
            }
            if (respuestaEnc == null)
            {
                return HttpNotFound();
            }
            return View(respuestaEnc);
        }

        // POST: RespuestaEnc/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM RespuestaEnc WHERE IdResEn = @IdResEn", con);
                cmd.Parameters.AddWithValue("IdResEn", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
