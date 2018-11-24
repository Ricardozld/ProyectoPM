using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.BaseDatos;

namespace WebApplication4.Models
{
    public class DetallesRespuesta
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public List<atractivo> atractivos { get; set; }
        public List<string> fotos { get; set; }
    }
}