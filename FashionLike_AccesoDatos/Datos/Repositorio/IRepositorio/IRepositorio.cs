using FashionLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        T Obtener(int id);
        IEnumerable<T> ObtenerTodos();
        void Agregar(T entidad);
        void Remover(T entidad);
        void Guardar();
        void Actualizar(T entidad);
    }

}
