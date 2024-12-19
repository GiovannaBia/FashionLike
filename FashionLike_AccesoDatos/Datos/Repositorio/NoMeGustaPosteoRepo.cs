using FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio;
using FashionLike_AccesoDatos.Migrations;
using FashionLike_Modelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionLike_AccesoDatos.Datos.Repositorio
{
  
    public class NoMeGustaPosteoRepo : Repositorio<NoMeGustaPosteo>, INoMeGustaPosteoRepo
    {
        private readonly ApplicationDBContext _db;
        public NoMeGustaPosteoRepo(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

   

        public NoMeGustaPosteo BuscarPosteoNoGusta(int posteoid, string usuarioid)
        {
            NoMeGustaPosteo posteo = _db.NoMeGustaPosteo.FirstOrDefault(p => p.PosteoId == posteoid && p.UsuarioId == usuarioid);
            return posteo;
            //_db.NoMeGustaPosteo.FirstOrDefault(p => p.PosteoId == Id && p.UsuarioId == usuarioId);
        }
    }
}
