﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4.BaseDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class sistemarecomendacionEntities : DbContext
    {
        public sistemarecomendacionEntities()
            : base("name=sistemarecomendacionEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<atractivo> atractivo { get; set; }
        public virtual DbSet<atractivo_has_criterios_evaluacion> atractivo_has_criterios_evaluacion { get; set; }
        public virtual DbSet<calificacion> calificacion { get; set; }
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<criterios_evaluacion> criterios_evaluacion { get; set; }
        public virtual DbSet<entidad_federativa> entidad_federativa { get; set; }
        public virtual DbSet<fotos> fotos { get; set; }
        public virtual DbSet<historial_visitas> historial_visitas { get; set; }
        public virtual DbSet<hstorial_atractivos> hstorial_atractivos { get; set; }
        public virtual DbSet<pueblo_magico> pueblo_magico { get; set; }
        public virtual DbSet<tipo_atractivo> tipo_atractivo { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
    }
}
