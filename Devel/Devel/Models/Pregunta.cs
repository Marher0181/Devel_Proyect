using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devel.Models
{
    public class Pregunta
    {
        public int IdPregunta { get; set; }
        public int IdEncuesta { get; set; }
        public string Descripcion { get; set; }
        public string Requerido { get; set; }
        public string TipoCampo { get; set; }

        // Relación con Encuesta
        public Encuesta Encuesta { get; set; }
        public List<RespuestaPre> Respuestas { get; set; }
        // Relación con Opcion
        public List<Opcion> Opciones { get; set; }
    }
}