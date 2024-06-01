using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devel.Models
{
    public class RespuestaPre
    {
        public int IdResPre { get; set; }
        public int IdResEnc { get; set; }
        public int IdPreg { get; set; }
        public string Respuesta { get; set; }
        public int? IdOpcion { get; set; } // Nullable as it might not always be set

        // Relación con RespuestaEnc
        public RespuestaEnc RespuestaEnc { get; set; }
        // Relación con Pregunta
        public Pregunta Pregunta { get; set; }
        // Relación con Opcion
        public Opcion Opcion { get; set; }
    }
}