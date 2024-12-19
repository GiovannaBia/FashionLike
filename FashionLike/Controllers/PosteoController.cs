using FashionLike.Models;
using FashionLike_Modelos.Models;
using FashionLike_AccesoDatos.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using FashionLike_Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using FashionLike_AccesoDatos.Datos.Repositorio.IRepositorio;  // Agregar esta referencia para el logger

namespace FashionLike.Controllers
{
    public class PosteoController : Controller
    {      
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PosteoController> _logger;  // Declara el logger
        private readonly IRepositorio<Posteo> _repo;
        private readonly IPosteoRepositorio _posteo;

        // Inyección de dependencias del logger
        public PosteoController(ApplicationDBContext db, IWebHostEnvironment webHostEnvironment, ILogger<PosteoController> logger, IRepositorio<Posteo> repo, IPosteoRepositorio posteo)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;  // Asigna el logger
            _repo = repo;
            _posteo = posteo;
        }

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Posteo> ListaPosteos = _repo.ObtenerTodos();
                // IEnumerable<Posteo> ListaPosteos = _db.Posteos;
                _logger.LogInformation("Accediendo a la lista de posteos.");
                return View(ListaPosteos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los posteos: {ex.Message}");
                return View("Error");
            }
        }

        //Get
        public IActionResult Upsert(int? Id)
        {
            try
            {
                Posteo posteo = new Posteo();
                if (Id == null)
                {
                    _logger.LogInformation("Creando un nuevo posteo.");
                    return View(posteo); //Enviamos posteo vacío para llenarlo
                }
                else
                {
                    if (Id.HasValue) 
                    {
                        posteo = _repo.Obtener(Id.Value); 
                        if (posteo == null)
                        {
                            _logger.LogWarning($"No se encontró un posteo con Id = {Id.Value}");
                            return NotFound();
                        }
                    }
                    else
                    {
                        _logger.LogWarning("El Id recibido es nulo.");
                        return NotFound();
                    }
                }
                return View(posteo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al acceder al posteo: {ex.Message}");
                return View("Error");
            }
        }

        //Crear o actualizar posteo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Posteo posteo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var files = HttpContext.Request.Form.Files;
                    string webRootPath = _webHostEnvironment.WebRootPath;

                    if (posteo.Id == 0)
                    {
                        _logger.LogInformation("Creando un nuevo posteo.");
                        // Lógica para crear un posteo
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        using (var FileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(FileStream);
                        }
                        posteo.VotosPositivos = 0;
                        posteo.VotosNegativos = 0;
                        posteo.ImagenUrl = fileName + extension;
                        posteo.Fecha = DateTime.Now;
                        _repo.Agregar(posteo);
                        //_db.Posteos.Add(posteo);
                        _logger.LogInformation($"Posteo con Id {posteo.Id} creado.");
                    }
                    else
                    {
                        _logger.LogInformation($"Actualizando el posteo con Id {posteo.Id}.");
                        var objPosteo = _posteo.ObtenerPosteoModificar(posteo.Id);
                       // var objPosteo = _db.Posteos.AsNoTracking().FirstOrDefault(p => p.Id == posteo.Id);
                        if (objPosteo == null)
                        {
                            _logger.LogWarning($"Posteo con Id {posteo.Id} no encontrado para actualizar.");
                            return NotFound();
                        }

                        if (files.Count > 0)
                        {
                            string upload = webRootPath + WC.ImagenRuta;
                            string fileName = Guid.NewGuid().ToString();
                            string extension = Path.GetExtension(files[0].FileName);

                            var anteriorFile = Path.Combine(upload, objPosteo.ImagenUrl);
                            if (System.IO.File.Exists(anteriorFile))
                            {
                                System.IO.File.Delete(anteriorFile);
                            }

                            using (var FileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                            {
                                files[0].CopyTo(FileStream);
                            }
                            posteo.ImagenUrl = fileName + extension;
                        }
                        else
                        {
                            posteo.ImagenUrl = objPosteo.ImagenUrl;
                        }
                        _repo.Actualizar(posteo);
                       // _db.Posteos.Update(posteo);
                    }
                    _repo.Guardar();
                    //_db.SaveChanges();
                    _logger.LogInformation("Posteo guardado correctamente.");
                    return RedirectToAction("Index");
                }
                else
                {                                  
                        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                        {
                            _logger.LogWarning($"Error en el modelo: {error.ErrorMessage}");
                        }
                        return View(posteo);                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar el posteo: {ex.Message}");
                return View("Error");
            }
        }

        //Eliminación de posteo
        public IActionResult Eliminar(int? Id)
        {
            try
            {          
                if (Id.HasValue)
                {
                    Posteo posteo = _repo.Obtener(Id.Value);
                    if (posteo == null)
                    {
                        _logger.LogWarning($"No se encontró un posteo con Id = {Id.Value}");
                        return NotFound();
                    }
                    return View(posteo);
                }
                else
                {
                    _logger.LogWarning("El Id recibido es nulo.");
                    return NotFound();
                }                                                   
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al acceder al posteo para eliminar: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Posteo posteo)
        {
            try
            {
                if (posteo == null)
                {
                    _logger.LogWarning("Posteo nulo al intentar eliminar.");
                    return NotFound();
                }

                string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;
                var anteriorFile = Path.Combine(upload, posteo.ImagenUrl);
                if (System.IO.File.Exists(anteriorFile))
                {
                    System.IO.File.Delete(anteriorFile);
                }
                _repo.Remover(posteo);
                //_db.Posteos.Remove(posteo);
                _repo.Guardar();
                //_db.SaveChanges();

                _logger.LogInformation($"Posteo con Id {posteo.Id} eliminado.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el posteo: {ex.Message}");
                return View("Error");
            }
        }
    }
}
