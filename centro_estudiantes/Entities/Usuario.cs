using System.ComponentModel.DataAnnotations;

namespace centro_estudiantes.Entities.Usuarios
{
    public class Usuario
    {   
        #region Properties
        [Key]
        public int IdUsuario {get; set;}
        
        public string NombreCompleto {get; set;} = null!;
        
        public string CorreoElectronico {get; set;} = null!;
        
        public string Telefono {get; set;} = null!;
        public int IdRol {get; set;}
        public DateTime FHAlta {get; set;}
        
        public DateTime? FHBaja {get; set;}
        #endregion
    }
}