using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.Models;


namespace GestionPrestamos2022.DAL { 

public class Contexto : DbContext
    {
        public DbSet<Ocupaciones>Ocupaciones{get; set;}
    
 
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {   
        }
        
    }    
}