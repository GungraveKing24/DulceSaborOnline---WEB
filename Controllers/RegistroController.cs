using DulceSaborOnline___WEB.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace DulceSaborOnline___WEB.Controllers
{
    public class RegistroController : Controller
    {
        private readonly DScontext _context;

        public RegistroController(DScontext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> registro(Usuarios usuario, IFormFile archivo)
        {
            if (ModelState.IsValid)
            {
                if (archivo != null && archivo.Length > 0)
                {
                    Stream archivoSubir = archivo.OpenReadStream();
                    string urlImagen = await SubirImagenAFirebase(archivoSubir, archivo.FileName);
                    usuario.foto = urlImagen;
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            // Si hay errores de validación, volver a la página de registro
            return View(usuario);
        }

        //Subir archivos

        public async Task<string> SubirImagenAFirebase(Stream archivoSubir, string nombreArchivo)
        {
            string email = "jorgefranciscocz@gmail.com";
            string clave = "ContraseñaXDXD";
            string ruta = "desarolloweb-7ffb8.appspot.com";
            string apikey = "AIzaSyBbIwF8pmsda6lLtldYsro7e_Aa_SCNGq0";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(apikey));
            var autentificar = await auth.SignInWithEmailAndPasswordAsync(email, clave);
            var cancellation = new CancellationTokenSource();
            var tokenuser = autentificar.FirebaseToken;

            var cargararchivo = new FirebaseStorage(ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(tokenuser),
                    ThrowOnCancel = true
                }
            ).Child("UsuarioFotos")
            .Child(nombreArchivo)
            .PutAsync(archivoSubir, cancellation.Token);

            var urlcarga = await cargararchivo;

            return urlcarga;
        }
    }
}
