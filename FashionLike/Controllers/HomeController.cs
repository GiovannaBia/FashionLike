using FashionLike.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FashionLike_AccesoDatos.Datos;
using FashionLike_Modelos.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FashionLike_Modelos.Models;
using System.Security.Claims;
using Mono.TextTemplating;
using FashionLike_Utilidades;
using FashionLike_AccesoDatos.Datos.Repositorio;
using FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio;

namespace FashionLike.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        private readonly IRepositorio<Posteo> _repo;
        private readonly IPosteoRepositorio _posteo;
        private readonly INoMeGustaPosteoRepo _nome;
        private readonly IMeGustaPosteoRepo _me;
        public HomeController(ILogger<HomeController> logger, IRepositorio<Posteo> repo, IPosteoRepositorio posteo, INoMeGustaPosteoRepo nome, IMeGustaPosteoRepo me)
        {
            _logger = logger;         
            _repo = repo;
            _posteo = posteo;   
            _nome = nome;
            _me = me;
        }

        public IActionResult Index(int currentIndex ) 
        {
            var posteos = _posteo.ObtenerEnOrden().ToList();


            if (!posteos.Any())
            {
                return View("NoPosteos"); // Muestra una vista indicando que no hay posteos.
            }

            if (currentIndex < 0 || currentIndex >= posteos.Count)
            {
                return NoContent();
            }

            var posteoActual = posteos[currentIndex]; // Usamos currentIndex para seleccionar el posteo actual.

            var viewModel = new PosteoHomeVM
            {
                Posteos = posteos
            };

            ViewData["CurrentIndex"] = currentIndex;  // Pasamos currentIndex a la vista.

            return View(viewModel);
        }

        [Authorize]
        public IActionResult VotarGusta(int Id, int currentIndex)
        {
            var posteo = _repo.Obtener(Id); // _db.Posteos.FirstOrDefault(p => p.Id == Id);
            var posteos = _repo.ObtenerTodos().ToList(); //_db.Posteos.ToList();
            var posteoActual = posteos[currentIndex];
            if (posteo == null)
            {
                return NotFound();
            }

            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return Unauthorized();
            }
            var posteonomegusta = _nome.BuscarPosteoNoGusta(posteo.Id, usuarioId);
           // var posteonomegusta = _db.NoMeGustaPosteo.FirstOrDefault(p => p.PosteoId == Id && p.UsuarioId == usuarioId);
            if (posteonomegusta != null)
            {
                if (posteo.VotosNegativos > 0)
                {
                    posteo.VotosNegativos--;
                }
                _nome.Remover(posteonomegusta);
                //_db.NoMeGustaPosteo.Remove(posteonomegusta);
            }

            var posteomegusta = _me.BuscarPosteoGusta(posteo.Id, usuarioId);
            // var posteomegusta = _db.MeGustaPosteo.FirstOrDefault(p => p.PosteoId == Id && p.UsuarioId == usuarioId);
            if (posteomegusta != null)
            {
                TempData[WC.Error] = "Ya te gusta este posteo";
                return RedirectToAction("Index", new { currentIndex = currentIndex });
            }

            posteo.VotosPositivos++;
            TempData[WC.Exitoso] = "Te gusta este posteo";

            MeGustaPosteo meGusta = new MeGustaPosteo()
            {
                UsuarioId = usuarioId,
                PosteoId = posteo.Id,
            };
            _me.Agregar(meGusta);
            //_db.MeGustaPosteo.Add(meGusta);

            _repo.Guardar();
            //_db.SaveChanges();

            return RedirectToAction("Index", new { currentIndex = currentIndex });
        }
        [Authorize]
        public IActionResult VotarNoGusta(int Id, int currentIndex)
        {
            var posteo = _repo.Obtener(Id);
            //var posteo = _db.Posteos.FirstOrDefault(p => p.Id == Id);

            if (posteo == null)
            {
                return NotFound();
            }

            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return Unauthorized();
            }
            var posteomegusta = _me.BuscarPosteoGusta(posteo.Id, usuarioId);
            //var posteomegusta = _db.MeGustaPosteo.FirstOrDefault(p => p.PosteoId == Id && p.UsuarioId == usuarioId);

            if (posteomegusta != null)
            {
                if (posteo.VotosPositivos > 0)
                {
                    posteo.VotosPositivos--;
                }
                _me.Remover(posteomegusta);
                //_db.MeGustaPosteo.Remove(posteomegusta);
            }
            var posteoNoMeGusta = _nome.BuscarPosteoNoGusta(posteo.Id, usuarioId);
            //var posteoNoMeGusta = _db.NoMeGustaPosteo.FirstOrDefault(p => p.PosteoId == Id && p.UsuarioId == usuarioId);
            if (posteoNoMeGusta != null)
            {
                TempData[WC.Error] = "Ya le diste no me gusta anteriormente";
                return RedirectToAction("Index", new { currentIndex = currentIndex });
            }

            posteo.VotosNegativos++;
            TempData[WC.Exitoso] = "No te gusta este posteo";

            NoMeGustaPosteo noMeGusta = new NoMeGustaPosteo()
            {
                UsuarioId = usuarioId,
                PosteoId = posteo.Id,
            };

            _nome.Agregar(noMeGusta);
            //_db.NoMeGustaPosteo.Add(noMeGusta);

            _repo.Guardar();
            //_db.SaveChanges();

            return RedirectToAction("Index", new { currentIndex = currentIndex });
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
