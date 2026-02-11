using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LuftBornTask.Data;
using Volo.Abp.DependencyInjection;

namespace LuftBornTask.EntityFrameworkCore;

public class EntityFrameworkCoreLuftBornTaskDbSchemaMigrator
    : ILuftBornTaskDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLuftBornTaskDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the LuftBornTaskDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LuftBornTaskDbContext>()
            .Database
            .MigrateAsync();
    }
}
