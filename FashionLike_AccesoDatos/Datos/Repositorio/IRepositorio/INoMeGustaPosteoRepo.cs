using FashionLike_Modelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface INoMeGustaPosteoRepo : IRepositorio<NoMeGustaPosteo>
    {
        public NoMeGustaPosteo BuscarPosteoNoGusta(int posteoid, string usuarioid); 
    }
}
