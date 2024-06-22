using Maquillaje.Models;
using Maquillaje.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maquillaje.Pages.Admin.Productos
{
    public class IndexModel : PageModel
    {
        // utilizamos esto para leer los productos de la bd
        private readonly MaquillajeContext context;
        //lista de objetos de tipo Producto y se inicializa con una nueva instancia de
        //List<Producto>
        public List<Producto> Productos { get; set; } = new List<Producto>();
        //toma un parámetro context de tipo MaquillajeContext y
        //lo asigna a un campo privado de la clase. 
        public IndexModel(MaquillajeContext context) 
        {
            this.context = context;
        }
        //metodo para leer los productos de la bd
        public void OnGet()
        {
            Productos=context.Productos.OrderByDescending(p=>p.Id).ToList();
        }
    }
}
