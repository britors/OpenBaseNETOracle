using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenBaseNET.Domain.Entities;
using OpenBaseNET.Infra.Settings.ConnectionStrings;

namespace OpenBaseNET.Infra.Data.Context;

public class OneBaseDataBaseContext(DbSession session, IConfiguration configuration) : DbContext
{
    public virtual required DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseOracle(
                configuration.GetConnectionString(OneBaseConnectionStrings.OneBaseOracle));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

}