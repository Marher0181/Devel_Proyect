using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devel.Models
{
    public class Opcion
    {
        public int IdOpcion { get; set; }
        public int IdPreg { get; set; }
        public string Texto { get; set; }

        // Relación con Pregunta
        public Pregunta Pregunta { get; set; }
    }
}