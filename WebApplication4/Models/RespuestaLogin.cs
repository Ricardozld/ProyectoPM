using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class RespuestaLogin
    {
        public int status { get; set; }
        public string mensaje { get; set; }
        public int idViajero { get; set; }
        public string nombre { get; set; }
        public string fechaNacimiento { get; set; }
        public string correo { get; set; }
        public List<int> gustos { get; set; }
    }
}