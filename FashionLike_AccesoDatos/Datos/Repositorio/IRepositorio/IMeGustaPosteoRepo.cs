using FashionLike_Modelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IMeGustaPosteoRepo : IRepositorio<MeGustaPosteo>
    {
        public MeGustaPosteo BuscarPosteoGusta(int posteoid, string usuarioid);
    }
}
