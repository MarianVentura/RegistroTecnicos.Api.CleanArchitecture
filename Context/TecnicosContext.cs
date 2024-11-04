using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Api.CleanArchitecture.Models;

namespace RegistroTecnicos.Api.CleanArchitecture.Context;

public class TecnicosContext : DbContext
{
    public TecnicosContext(DbContextOptions<TecnicosContext> options) : base(options) { }
    public DbSet<Clientes> Clientes { get; set; }
}
