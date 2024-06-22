using Maquillaje.Models;
using Maquillaje.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maquillaje.Pages.Admin.Productos
{
    public class CreateModel : PageModel
    {
        // campo privados y solo de lectura para proporcionar informaci�n sobre el entorno web
        private readonly IWebHostEnvironment environment;
        // campo privados y solo de lectura para interactura con la bd
        private readonly MaquillajeContext context;

        //se utiliza para especificar que una propiedad de un modelo de p�gina debe ser
        //enlazada autom�ticamente a los datos enviados en una solicitud HTTP.
        [BindProperty]

        //Define una propuedad en una clase ProductoDto ,
        //inicializada con nuevas instancias de sus respectivos tipos. 
        public ProductoDto ProductoDto { get; set; } = new ProductoDto();

        //Constructor con dos par�metros y asigna los par�metros a los campos
        public CreateModel(IWebHostEnvironment environment, MaquillajeContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";

        //manejar las solicitudes HTTP POST enviadas a la p�gina.
        public void OnPost() 
        {
            
            if (ProductoDto.Imagen == null)
            {
                ModelState.AddModelError("ProductoDbto.Imagen", "La imagen no es requerido");
            }

            //si no se han completado todos los campos nos muestra un error
            if (!ModelState.IsValid)
            {
                errorMessage = "Por favor llena todos los campos";
                return;
            }

            //guardar imagen
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductoDto.Imagen!.FileName);

            string imageFullPath = environment.WebRootPath + "/Products/" + newFileName;
            using (var stream= System.IO.File.Create(imageFullPath))
            {
                ProductoDto.Imagen.CopyTo(stream);
            }

            // guardar producto en la bd
            Producto producto = new Producto()
            {
                Nombre=ProductoDto.Nombre,
                Marca=ProductoDto.Marca,
                Tono=ProductoDto.Tono,
                Precio=ProductoDto.Precio,
                Imagen=newFileName,
            };
            //a�adimos el producto
            context.Productos.Add(producto);
            context.SaveChanges();

            //limpiar el formulario
            ProductoDto.Nombre = "";
            ProductoDto.Marca = "";
            ProductoDto.Tono = "";
            ProductoDto.Precio = 0;
            ProductoDto.Imagen = null;

            ModelState.Clear();
            successMessage = "Producto creado correctamente";
            Response.Redirect("/Admin/Productos/Index");
        }
    }
}
