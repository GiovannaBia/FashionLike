using FashionLike.Models;
using FashionLike_Modelos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FashionLike_AccesoDatos.Datos
{
    public class ApplicationDBContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
                
        }

        public DbSet<Posteo> Posteos { get; set; }
        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        public DbSet<MeGustaPosteo> MeGustaPosteo { get; set; } 
        public DbSet<NoMeGustaPosteo> NoMeGustaPosteo { get; set; }
    }
}
