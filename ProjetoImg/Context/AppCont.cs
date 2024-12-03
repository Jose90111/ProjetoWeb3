using Microsoft.EntityFrameworkCore;
using ProjetoImg.Models;

namespace ProjetoImg.Context
{
    public class AppCont : DbContext
    {
        public AppCont(DbContextOptions<AppCont> options) : base(options)
        {

        }

        public DbSet<Cad_cliente> Clientes { get; set; }
    }
}
