using System.ComponentModel.DataAnnotations;

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
        public int Votos   { get; set; }
       // [Required(ErrorMessage ="La imagen es obligatoria")]
        public string? ImagenUrl { get; set; }

    }
}
