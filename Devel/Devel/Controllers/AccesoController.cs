using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Devel.Models;
using System.Data.SqlClient;
using System.Data;


namespace Devel.Controllers
{
    public class AccesoController : Controller
    {
        static string cn = "Data Source=DESKTOP-HMS6GDC\\SQLEXPRESS;Initial Catalog=ProyectoDevel;Integrated Security=True";

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            bool reg;
            string men;
            if(usuario.Nombre != null && usuario.Apellido != null) {
                if (usuario.Pass.Equals(usuario.ConfirmPass))
                {
                    usuario.Pass = Encrypt(usuario.Pass);
                }
                else
                {
                    ViewData["Mensaje"] = "Las contraseñas no coinciden";
                    return View();
                }
            }
            else
            {
                ViewData["Mensaje"] = "Debe añadir nombre y apellido";
                return View();
            }
            
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("sp_Registro", con);

                cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("Email", usuario.Email);
                cmd.Parameters.AddWithValue("Pass", usuario.Pass);
                cmd.Parameters.Add("Registro", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                cmd.ExecuteNonQuery();

                reg = Convert.ToBoolean(cmd.Parameters["Registro"].Value);
                men = cmd.Parameters["Mensaje"].Value.ToString();

            }

            ViewData["Mensaje"] = men;

            if (reg)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {

            usuario.Pass = Encrypt(usuario.Pass);
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", con);

                cmd.Parameters.AddWithValue("Email", usuario.Email);
                cmd.Parameters.AddWithValue("Pass", usuario.Pass);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                usuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar());
            }

            if(usuario.IdUsuario != 0)
            {
                Session["Usuario"] = usuario;
                Session["Id"] = usuario.IdUsuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

        }

        public string Encrypt(string txt)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding en = Encoding.UTF8;
                byte[] result = hash.ComputeHash(en.GetBytes(txt));

                foreach (byte b in result) {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }

        }
    }
}