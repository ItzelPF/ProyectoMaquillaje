using Maquillaje.Models;
using Maquillaje.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace Maquillaje.Pages.Admin.Productos
{
    public class EditarModel : PageModel

    {
        // campo privados y solo de lectura para proporcionar información sobre el entorno web
        private readonly IWebHostEnvironment environment;
        // campo privados y solo de lectura para interactura con la bd
        private readonly MaquillajeContext context;

        //se utiliza para especificar que una propiedad de un modelo de página debe ser
        //enlazada automáticamente a los datos enviados en una solicitud HTTP. 
        [BindProperty]

        //Define dos propiedades en una clase, ProductoDto y Producto,
        //ambas inicializadas con nuevas instancias de sus respectivos tipos. 
        public ProductoDto ProductoDto { get; set; } = new ProductoDto();
        public Producto Producto { get; set; } = new Producto();
        
        // declaramos mensajes
        public string errorMessage = "";
        public string successMessage = "";

        //Constructor con dos parámetros y asigna los parámetros a los campos
        public EditarModel(IWebHostEnvironment environment, MaquillajeContext context)
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
            var product = context.Productos.Find(id);
            if (product == null) {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }

            //Asignamos valores al objeto
            ProductoDto.Nombre = product.Nombre;
            ProductoDto.Marca = product.Marca;
            ProductoDto.Tono = product.Tono;
            ProductoDto.Precio = product.Precio;

            //Asignamos al objeto los nuevos datos ingresados
            Producto = product;
 
        }

        //manejar las solicitudes HTTP POST enviadas a la página.
        public void OnPost(int? id)
        {
            //si no se encuentra el id vuelve a la página de productos
            if (id == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }
            //si no se han completado todos los campos nos muestra un error
            if (!ModelState.IsValid)
            {
                errorMessage = "Por favor llene todos los campos";
                return;
            }

            //Si se encuentra un producto con el ID especificado, se devuelven los valores
            //de lo contrario, se devuelve null
            var product = context.Productos.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }

            //actualizar la imagen si tenemos una nueva imagen
            string newFileName = product.Imagen;
            if (ProductoDto.Imagen != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(ProductoDto.Imagen.FileName);
                string imageFullPath = environment.WebRootPath + "/Products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductoDto.Imagen.CopyTo(stream);
                }

                //eliminar imagen vieja
                string oldImageFullPath = environment.WebRootPath + "/Products/" + product.Imagen;
                System.IO.File.Delete(oldImageFullPath);
            }

            //actualizar los datos en la bd
            product.Nombre = ProductoDto.Nombre;
            product.Marca = ProductoDto.Marca;
            product.Tono = ProductoDto.Tono;
            product.Precio = ProductoDto.Precio;
            product.Imagen = newFileName;

            //guarda los cambios
            context.SaveChanges();

            //Asignamos al objeto los nuevos datos ingresados
            Producto = product;
            successMessage = "Producto actualizado correctamente";
            //regresamos a la página de productos
            Response.Redirect("/Admin/Productos/Index");

        }
    }
}
