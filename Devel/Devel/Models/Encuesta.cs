using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devel.Models
{
    public class Encuesta
    {
        public int IdEncuesta { get; set; }
        public int IdUsuarioCre { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        // Relación con Usuario
        public Usuario UsuarioCreador { get; set; }
        // Relación con Pregunta
        public List<Pregunta> Preguntas { get; set; }
    }
}