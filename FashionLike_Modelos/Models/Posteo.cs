using FashionLike_Modelos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionLike.Models
{
    public class Posteo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obiligatorio")]
        public string Nombre { get; set; } 
        [Required]
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public int? VotosPositivos { get; set; } = 0;
        public int? VotosNegativos { get; set; } = 0;
        // Relación con los usuarios que le dieron "me gusta"
        [InverseProperty("MeGustaPosteo")]
        public List<UsuarioAplicacion>? UsuariosQueLeGustan { get; set; }

        // Relación con los usuarios que le dieron "no me gusta"
        [InverseProperty("NoMegustaPosteo")]
        public List<UsuarioAplicacion>? UsuariosQueNoLeGustan { get; set; }


        // [Required(ErrorMessage ="La imagen es obligatoria")]
        public string? ImagenUrl { get; set; }

    }
}
