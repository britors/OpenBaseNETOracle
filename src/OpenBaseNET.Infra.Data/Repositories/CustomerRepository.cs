using Microsoft.Extensions.Logging;
using OpenBaseNET.Domain.Entities;
using OpenBaseNET.Domain.Interfaces.Repositories;
using OpenBaseNET.Domain.QueryResults;
using OpenBaseNET.Infra.Data.Context;

namespace OpenBaseNET.Infra.Data.Repositories;

public sealed class CustomerRepository(
    DbSession dbSession,
    ILogger<RepositoryBase<Customer>> logger,
    OneBaseDataBaseContext context)
    : RepositoryBase<Customer>(dbSession, logger, context), ICustomerRepository, IDataRepository
{
    public async Task<IEnumerable<CustomerQueryResult>> 
        FindByNameAsync(string name, int pageNumber, int pageSize, CancellationToken cancellationToken)
    { 
        var query = $"""
                      SELECT
                         CLIID AS Id,
                         CLINM AS Name
                      FROM CLITAB
                      WHERE UPPER(CLINM) LIKE '%{name.ToUpper()}%'
                      ORDER BY CLIID ASC
                      OFFSET ({pageNumber}-1)*{pageSize} ROWS
                      FETCH NEXT {pageSize} ROWS ONLY
                     """;
        
        return await QueryAsync<CustomerQueryResult> (query, cancellationToken) ?? 
               throw new InvalidOperationException();
    }
}