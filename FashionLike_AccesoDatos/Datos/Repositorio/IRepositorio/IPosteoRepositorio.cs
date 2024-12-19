using System;
using FashionLike_Modelos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionLike.Models;

namespace FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IPosteoRepositorio : IRepositorio<Posteo>
    {
        public IEnumerable<Posteo> ObtenerEnOrden();
        public Posteo ObtenerPosteoModificar(int Id);
    }
}
