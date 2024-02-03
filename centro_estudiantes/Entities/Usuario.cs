using centro_estudiantes.Entities.Choripanes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        [ForeignKey("IdUsuario")]
        public virtual ICollection<Choripan> Choripanes { get; set; }
        #endregion
    }
}