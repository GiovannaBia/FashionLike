using FashionLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionLike_Modelos.Models.ViewModels
{
    public class PosteoHomeVM
    {
        public List<Posteo> Posteos { get; set; }
        public UsuarioAplicacion UsuarioAplicacion { get; set; }
    }
}
