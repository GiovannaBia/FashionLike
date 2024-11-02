using FashionLike.Models;
using FashionLike_AccesoDatos.Datos;
using Microsoft.AspNetCore.Mvc;

namespace FashionLike.Controllers
{
    public class PosteoController : Controller
    {
        private readonly ApplicationDBContext _db;
        public PosteoController(ApplicationDBContext db)
        {
                _db = db;   
        }
        public IActionResult Index()
        {
            IEnumerable<Posteo> ListaPosteos = _db.Posteos;
            return View(ListaPosteos);
        }
        //Get
        public IActionResult Crear() 
        { 
            return View();
        }
        //Crear post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Posteo posteo) 
        {
            if (ModelState.IsValid)
            {
                _db.Posteos.Add(posteo);
                _db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }
    }
}
