using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEncuestasBL.Models
{
    public enum Rol
    {
        ADMINISTRADOR,
        USUARIO

    }
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        public string Nombre { get; set; }
        
        [Column(TypeName = "VARCHAR")] //Se limita el campo para poder hacer un UNIQUE
        [StringLength(256)]
        public string Email { get; set; }

        public string Clave { get; set; }

        public Rol RolSeguridad  { get; set; }

        public Usuario()
        {

        }

        public Usuario(string nombre, string email, string clave)
        {
            this.Nombre = nombre;
            this.Email = email;
            this.Clave = clave;
        }
    }
}
