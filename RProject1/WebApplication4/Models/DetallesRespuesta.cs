using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.BaseDatos;

namespace WebApplication4.Models
{
    public class DetallesRespuesta
    {
        public int status { get; set; }
        public string mensaje { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public List<atractivoDetalles> atractivos { get; set; }
        public List<string> fotos { get; set; }
    }
}