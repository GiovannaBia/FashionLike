using FashionLike.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionLike_Modelos.Models
{
    public class MeGustaPosteo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [Required]
        public int PosteoId { get; set; }

        // Clave foránea a la entidad UsuarioAplicacion
        [ForeignKey("UsuarioId")]
        public UsuarioAplicacion Usuario { get; set; }

        // Clave foránea a la entidad Posteo
        [ForeignKey("PosteoId")]
        public Posteo Posteo { get; set; }
    }
}
