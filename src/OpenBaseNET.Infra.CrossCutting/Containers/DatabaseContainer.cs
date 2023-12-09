using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenBaseNET.Infra.Data.Context;
using OpenBaseNET.Infra.Settings.ConnectionStrings;
using OpenBaseNET.Infra.Uow;
using OpenBaseNET.Infra.Uow.Interfaces;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

namespace OpenBaseNET.Infra.CrossCutting.Containers;

internal static class DatabaseContainer
{
    internal static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbConnection>(_ =>
            new OracleConnection(configuration.GetConnectionString(OneBaseConnectionStrings.OpenBaseOracle)));
        services.AddScoped<DbSession>();
        services.AddScoped<OneBaseDataBaseContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}