using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GestionPrestamos2022.DAL { 

public class Contexto : IdentityDbContext
    {
        public DbSet<Ocupaciones> Ocupaciones {get; set;}
        public DbSet<Personas> Personas {get; set;}
        public DbSet<Prestamos> Prestamos {get; set;}
        public DbSet<Pagos> Pagos {get; set;}
        public DbSet<PagosDetalle> PagosDetalle {get; set;}
 
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {   
        }

         
    }    
}