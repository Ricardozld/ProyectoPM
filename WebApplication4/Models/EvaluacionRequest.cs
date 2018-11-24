using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class EvaluacionRequest
    {
        public int idViajero { get; set; }
        public List<Evaluaciones> evaluaciones { get; set; }
        public double promedio { get; set; }
        public int idPuebloMagico { get; set; }

    }
}