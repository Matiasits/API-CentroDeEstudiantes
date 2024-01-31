using System.ComponentModel.DataAnnotations;

namespace centro_estudiantes.Entities.TipoChoris
{
    public class TipoChori
    {   
        #region Properties
        [Key]
        public int IdTipoChori {get; set;}
        
        public string TipoDeChori {get; set;} = null!; 
        #endregion
    }
}