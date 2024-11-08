using FashionLike.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FashionLike_AccesoDatos.Datos;
using FashionLike_Modelos.Models.ViewModels;

namespace FashionLike.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int id = 0)
        {
            var posteos = _db.Posteos.ToList();
            if (!posteos.Any())
            {
                return View("NoPosteos"); // Muestra una vista indicando que no hay posteos si la lista está vacía.
            }

            if (id < 0) id = 0;
            if (id >= posteos.Count) id = posteos.Count - 1; // Evita índices fuera de rango.

            var viewModel = new PosteoHomeVM
            {
                Posteos = posteos // Pasamos la lista completa.
            };

            ViewData["CurrentIndex"] = id;

            return View(viewModel);
        }
        public IActionResult VotarGusta(int Id)
        {
            var posteo = _db.Posteos.FirstOrDefault(p=> p.Id == Id);
            if (posteo != null)
            {
                if(posteo.VotosNegativos > 0)
                {
                    posteo.VotosNegativos--;
                }
                posteo.VotosPositivos++;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id });

        }
        public IActionResult VotarNoGusta(int Id)
        {
            var posteo = _db.Posteos.FirstOrDefault(p => p.Id == Id);
            if (posteo != null)
            {
                if (posteo.VotosPositivos > 0)
                {
                    posteo.VotosPositivos--;
                }
                posteo.VotosNegativos++;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id });

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
