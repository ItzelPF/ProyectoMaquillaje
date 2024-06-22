using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Maquillaje.Models
{
    public class ProductoDto
    {
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = "";
        [Required, MaxLength(100)]
        public string Marca { get; set; } = "";
        [Required, MaxLength(100)]
        public string Tono { get; set; } = "";
        [Required]
        public decimal Precio { get; set; }
        public IFormFile? Imagen { get; set; }
    }
}
