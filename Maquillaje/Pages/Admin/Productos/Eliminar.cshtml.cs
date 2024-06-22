using Maquillaje.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maquillaje.Pages.Admin.Productos
{
    public class EliminarModel : PageModel
    {
        // campo privados y solo de lectura para proporcionar información sobre el entorno web
        private readonly IWebHostEnvironment environment;
        // campo privados y solo de lectura para interactura con la bd
        private readonly MaquillajeContext context;
        
        //Constructor con dos parámetros y asigna los parámetros a los campos
        public EliminarModel(IWebHostEnvironment environment, MaquillajeContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        //responde a solicitudes HTTP GET cargando un producto específico por id
        public void OnGet(int? id)
        {
            //si no se encuentra el id vuelve a la página de productos
            if (id == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }

            //Si se encuentra un producto con el ID especificado, se devuelven los valores
            //de lo contrario, se devuelve null
            var producto = context.Productos.Find(id);
            if (producto == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }

            //ver la dirección de la imagen y eliminarla
            string imageFullPath = environment.WebRootPath + "/Products/" + producto.Imagen;
            System.IO.File.Delete(imageFullPath);

            //eliminar los datos del producto
            context.Productos.Remove(producto);
            //guardar los cambios
            context.SaveChanges();
            //volver a la página productos
            Response.Redirect("/Admin/Productos/Index");
        }
    }
}
