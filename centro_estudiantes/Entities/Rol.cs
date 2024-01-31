using System.ComponentModel.DataAnnotations;

namespace centro_estudiantes.Entities.Roles
{
    public class Rol
    {   
        #region Properties
        [Key]
        public int IdRol {get; set;}
        
        public string NombreRol {get; set;} = null!;
        #endregion
    }
}