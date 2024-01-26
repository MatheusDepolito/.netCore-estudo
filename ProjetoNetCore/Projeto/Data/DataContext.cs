using Microsoft.EntityFrameworkCore;
using Projeto.Controllers.Entities;

namespace Projeto.Data
{

    public class DataContext : DbContext
    { 
    
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        
        }    
    
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
