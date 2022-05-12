
using AutoMapper;
using SistemaEncuestasBL.Data;
using SistemaEncuestasBL.DTOs;
using SistemaEncuestasBL.Models;
using SistemaEncuestasBL.Repositories.Implements;
using SistemaEncuestasBL.Services;
using SistemaEncuestasBL.Services.Implements;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace SistemaEncuestasAPI.Controllers
{
    /// <summary>
    /// Funcionalidades de identificación y acceso a la aplicación.
    /// </summary>
    [Route("api/Cuentas")]
    public class CuentasController : ApiController
    {

        private readonly IUsuarioService usuarioService = new UsuarioService(new UsuarioRepository(SistemaEncuestasContext.Crear()));
        private readonly IMapper mapper;
        private readonly string nombreCookie = "JwtEncuestas";
        public CuentasController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Permit obtener el token de ejecución 
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [Route("api/login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginDTO loginDTO)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                Usuario usuarioEnBase = await this.usuarioService.GetUsuarioByEmail(loginDTO.Email);

                if (usuarioEnBase != null)
                {

                    if (BCrypt.Net.BCrypt.Verify(loginDTO.Clave, usuarioEnBase.Clave))
                    {
                        var token = TokenGenerator.GenerateTokenJwt(loginDTO.Email);

                        HttpCookie cookieRespuesta = new HttpCookie(this.nombreCookie);
                        cookieRespuesta.Value = token;
                        cookieRespuesta.HttpOnly = false;

                        HttpResponse response = HttpContext.Current.Response;
                        response.Cookies.Add(cookieRespuesta);

                        return Ok(token);
                    }
                }


                return Unauthorized();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        /// <summary>
        /// Permite registrar un usuario en el sistema.
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        [Authorize]
        //[AllowAnonymous]
        [HttpPost]
        [Route("api/Cuentas/registrar")]
        public async Task<IHttpActionResult> Regisitrar(UsuarioDTO usuarioDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Usuario usuarioEnBase = await this.usuarioService.GetUsuarioByEmail(usuarioDTO.Email.Trim().ToLower());

            if (usuarioEnBase == null)
            {
                Usuario nuevoUsuario = new Usuario(usuarioDTO.Nombre, usuarioDTO.Email, BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Clave));
                await this.usuarioService.Insert(nuevoUsuario);
                return Ok();
            }
            else
            {
                return BadRequest("El Usuario ya existe.");
            }


        }

        /// <summary>
        /// Permite modificar los datos de usuarios del sistema (ya registrados).
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("api/Cuentas/modificar")]
        public async Task<IHttpActionResult> Modificar(UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //Usuario usuarioEnBase = await this.usuarioService.GetUsuarioByEmail(usuarioDTO.Email.Trim().ToLower());
            Usuario usuarioEnBase = await this.usuarioService.GetById(usuarioDTO.UsuarioID);

            if (usuarioEnBase != null)
            {

                usuarioEnBase.Nombre = usuarioDTO.Nombre;
                usuarioEnBase.Email = usuarioDTO.Email;
                usuarioEnBase.Clave = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Clave);
                usuarioEnBase.RolSeguridad = usuarioDTO.RolSeguridad == "ADMINISTRADOR" ? Rol.ADMINISTRADOR : Rol.USUARIO; //"USUARIO"
                await this.usuarioService.Update(usuarioEnBase);

                return Ok();
            }
            else {
                return BadRequest("El Usuario no existe.");
            }

            

        }


        /// <summary>
        /// Obtiene el usuario dado su correo electónico.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("api/Cuentas/usuarioPorMail")]
        public async Task<IHttpActionResult> ObtenerUsuario(string email)
        {
            if (email == "") return BadRequest("Debe proporcionar un email válido.");

            Usuario usuario = await this.usuarioService.GetUsuarioByEmail(email.Trim().ToLower());

            if (usuario == null) return BadRequest("No se encontró el usuario.");

            usuario.Clave = "";

            return Ok(mapper.Map<UsuarioDTO>(usuario));

        }

        /// <summary>
        /// Realiza el logout del sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("api/Cuentas/logout")]
        public async Task<IHttpActionResult> Logout()
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Cookies.Remove(nombreCookie);

            return Ok("Logout exitoso.");
        }

        /// <summary>
        /// Returna un usuario dado su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            var usuario = await usuarioService.GetById(id);

            if (usuario == null) return NotFound();

            var usuarioDTO = mapper.Map<UsuarioDTO>(usuario);

            return Ok(usuarioDTO);
        }

    }
}


