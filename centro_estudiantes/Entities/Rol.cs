using System.ComponentModel.DataAnnotations;

namespace centro_estudiantes.Entities.Rol
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