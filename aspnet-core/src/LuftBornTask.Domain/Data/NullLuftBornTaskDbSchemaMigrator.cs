using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LuftBornTask.Data;

/* This is used if database provider does't define
 * ILuftBornTaskDbSchemaMigrator implementation.
 */
public class NullLuftBornTaskDbSchemaMigrator : ILuftBornTaskDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
