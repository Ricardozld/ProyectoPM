using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class ActualizarRequest
    {
        public int idViajero { get; set; }
        public string fechaNacimiento { get; set; }
        public string nombre { get; set; }
        public List<int> idAtractivos { get; set; }
    }
}