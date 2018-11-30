using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class atractivoDetalles
    {
        public int idAtractivo { get; set; }
        public string nombre { get; set; }
        public int categoria { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }
        
       
    }
}