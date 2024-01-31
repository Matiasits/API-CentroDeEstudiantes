using Microsoft.EntityFrameworkCore;
using centro_estudiantes.Entities.Choripanes;
using System.Diagnostics.CodeAnalysis;
using centro_estudiantes.Entities.TipoChoris;
using centro_estudiantes.Entities.Roles;
using centro_estudiantes.Entities.Usuarios;

namespace centro_estudiantes.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    [NotNull]
    public DbSet<Choripan>? Choripan { get; set; }
    public DbSet<Rol>? Rol { get; set; }
    public DbSet<Usuario>? Usuario { get; set; }
    public DbSet<TipoChori> TipoChori { get; set; }
    
}