using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devel.Models
{
    public class RespuestaEnc
    {

        public int IdResEn { get; set; }
        public int IdEncuesta { get; set; }

        // Relación con Encuesta
        public Encuesta Encuesta { get; set; }
        // Relación con RespuestaPre
        public List<RespuestaPre> Respuestas { get; set; }
    }
}