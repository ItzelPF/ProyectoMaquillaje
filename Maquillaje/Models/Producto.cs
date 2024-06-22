using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Maquillaje.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; } = "";
        [MaxLength(100)]
        public string Marca { get; set; } = "";
        [MaxLength(100)]
        public string Tono { get; set; } = "";
        [Precision(12,2)]
        public decimal Precio { get; set; }
        [MaxLength(100)]
        public string Imagen { get; set; } = "";
    }
}
