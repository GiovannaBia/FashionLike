using FashionLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace FashionLike_AccesoDatos.Datos.Repositorio
{
    public class PosteoRepositorio : Repositorio<Posteo>, IPosteoRepositorio
    {
        private readonly ApplicationDBContext _db;
        public PosteoRepositorio(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Posteo> ObtenerEnOrden()
        {
            return _db.Posteos.OrderByDescending(p => p.Fecha);
        }

        public Posteo ObtenerPosteoModificar(int Id)
        {
            return _db.Posteos.AsNoTracking().FirstOrDefault(p=>p.Id == Id);          
        }
    }
}

