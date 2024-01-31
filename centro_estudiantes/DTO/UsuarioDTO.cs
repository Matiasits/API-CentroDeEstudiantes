namespace centro_estudiantes.dto.Usuarios
{
    public class UsuarioDTO
    {   
        public int IdUsuario {get; set;}
        
        public string NombreCompleto {get; set;} = null!;
        
        public string CorreoElectronico {get; set;} = null!;
        
        public string Telefono {get; set;} = null!;
        
        public int IdRol {get; set;}
    }
}