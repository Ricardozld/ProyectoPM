using
    System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models.Recomendación
{
    public class MatrizCalificaciones
    {
        public int idUsuario { get; set; }
        public int idAtractivo { get; set; }
        public double calificacion { get; set; }
    }
}