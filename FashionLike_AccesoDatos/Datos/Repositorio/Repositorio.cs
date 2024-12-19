using FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio;

namespace FashionLike_AccesoDatos.Datos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        private readonly DbSet<T> _dbSet;

        public Repositorio(ApplicationDBContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public void Agregar(T entidad)
        {
            _dbSet.Add(entidad);
        }

        public void Actualizar(T entidad)
        {
            _dbSet.Update(entidad);
        }

        public T Obtener(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> ObtenerTodos()
        {
            return _dbSet.ToList();
        }

        public void Remover(T entidad)
        {
            _dbSet.Remove(entidad);
        }

        public void Guardar()
        {
            _db.SaveChanges();
        }
    }

}
