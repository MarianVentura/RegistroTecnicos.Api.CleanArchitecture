﻿using Microsoft.EntityFrameworkCore;
using Tecnicos.Data.Models;

namespace Tecnicos.Data.Context;

public class TecnicosContext : DbContext
{
    public TecnicosContext(DbContextOptions<TecnicosContext> options) : base(options) { }
    public DbSet<Clientes> Clientes { get; set; }
}



