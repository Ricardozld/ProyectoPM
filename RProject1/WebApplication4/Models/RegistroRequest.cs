using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class RegistroRequest
    {
        public string fechaNacimiento { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public List<int> gustos { get; set; }
    }
}