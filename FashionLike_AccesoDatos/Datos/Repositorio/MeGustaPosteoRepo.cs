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

    public class MeGustaPosteoRepo : Repositorio<MeGustaPosteo>, IMeGustaPosteoRepo
    {
        private readonly ApplicationDBContext _db;
        public MeGustaPosteoRepo(ApplicationDBContext db) : base(db)
        {
             _db = db;
        }

        public MeGustaPosteo BuscarPosteoGusta(int posteoid, string usuarioid)
        {
            MeGustaPosteo posteomegusta = _db.MeGustaPosteo.FirstOrDefault(p => p.PosteoId == posteoid && p.UsuarioId == usuarioid);
            return posteomegusta;
        }
    }
}
