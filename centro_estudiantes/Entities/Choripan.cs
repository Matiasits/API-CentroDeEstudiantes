using System.ComponentModel.DataAnnotations;

namespace centro_estudiantes.Entities.Choripanes
{
    public class Choripan
    {   
        #region Properties
        [Key]
        public int IdChoripan {get; set;}
        
        public int IdTipoChori {get; set;}
        
        public string? Verdura {get; set;} 
        
        public string? Aderezo {get; set;}
        
        public string? Pan {get; set;}
        
        public int IdUsuario {get; set;}
        
        public DateTime FHAlta {get; set;}
        
        public DateTime? FHBaja {get; set;}
        #endregion
    }
}