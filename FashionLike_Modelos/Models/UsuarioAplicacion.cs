using FashionLike.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionLike_Modelos.Models
{
    public class UsuarioAplicacion : IdentityUser
    {
        public string NombreCompleto { get; set; }

        // Relación con los posteos que le gustan
        [InverseProperty("UsuariosQueLeGustan")]
        public List<Posteo> MeGustaPosteo { get; set; }

        // Relación con los posteos que no le gustan
        [InverseProperty("UsuariosQueNoLeGustan")]
        public List<Posteo> NoMegustaPosteo { get; set; }
    }
}
