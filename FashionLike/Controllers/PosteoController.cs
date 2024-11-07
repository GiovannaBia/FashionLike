using FashionLike.Models;
using FashionLike_AccesoDatos.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using FashionLike_Utilidades;
using Ganss;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;

namespace FashionLike.Controllers
{
    public class PosteoController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PosteoController(ApplicationDBContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Posteo> ListaPosteos = _db.Posteos;
            return View(ListaPosteos);
        }
        //Get
        public IActionResult Upsert(int? Id)
        {
            Posteo posteo = new Posteo();
            if (Id == null)
            {
                //Creamos un nuevo posteo
                return View(posteo); //Enviamos posteo vacío para llenarlo
            }
            else
            { //Buscamos el posteo por el id y enviamos a la vista
                posteo = _db.Posteos.Find(Id);
                if (posteo == null)
                {
                    return NotFound();
                }

                return View(posteo);
            }
        }
        //Crear post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Posteo posteo)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files; //Obtengo la img desde el formulario
                string webRootPath = _webHostEnvironment.WebRootPath; //Obtengo la ruta fisica de la carpeta wwwroot

                var sanitizier = new HtmlSanitizer();
                sanitizier.AllowedTags.Add("strong"); // Permitir <strong> (negritas)
                sanitizier.AllowedTags.Add("em");    // Permitir <em> (cursiva)
                posteo.Descripcion = sanitizier.Sanitize(posteo.Descripcion);

                if (posteo.Id == 0)
                {
                    //Creo un posteo
                    string upload = webRootPath + WC.ImagenRuta; //Ruta donde guardaremos la imagen
                    string fileName = Guid.NewGuid().ToString(); //Para que le asigne un id a la img que se guardara
                    string extension = Path.GetExtension(files[0].FileName);
                    //Llamo a filestream para que cree el archivo subido en la ruta especifidada
                    using (var FileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(FileStream);
                    }
                    posteo.ImagenUrl = fileName + extension;
                    posteo.Fecha = DateTime.Now;
                    _db.Posteos.Add(posteo);                    
                }
                else
                {
                    //Actualizar posteo
                    var objPosteo = _db.Posteos.AsNoTracking().FirstOrDefault(p => p.Id == posteo.Id);
                    if (objPosteo == null)
                    {
                        return NotFound();
                    }
                    if(files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        //borrar la imagen
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
                    _db.Posteos.Update(posteo);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(posteo);
            }
                   
        }
    }
}
