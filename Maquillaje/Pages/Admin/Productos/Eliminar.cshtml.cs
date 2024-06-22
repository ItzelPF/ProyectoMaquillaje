using Maquillaje.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maquillaje.Pages.Admin.Productos
{
    public class EliminarModel : PageModel
    {
        // campo privados y solo de lectura para proporcionar informaci�n sobre el entorno web
        private readonly IWebHostEnvironment environment;
        // campo privados y solo de lectura para interactura con la bd
        private readonly MaquillajeContext context;
        
        //Constructor con dos par�metros y asigna los par�metros a los campos
        public EliminarModel(IWebHostEnvironment environment, MaquillajeContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        //responde a solicitudes HTTP GET cargando un producto espec�fico por id
        public void OnGet(int? id)
        {
            //si no se encuentra el id vuelve a la p�gina de productos
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

            //ver la direcci�n de la imagen y eliminarla
            string imageFullPath = environment.WebRootPath + "/Products/" + producto.Imagen;
            System.IO.File.Delete(imageFullPath);

            //eliminar los datos del producto
            context.Productos.Remove(producto);
            //guardar los cambios
            context.SaveChanges();
            //volver a la p�gina productos
            Response.Redirect("/Admin/Productos/Index");
        }
    }
}
