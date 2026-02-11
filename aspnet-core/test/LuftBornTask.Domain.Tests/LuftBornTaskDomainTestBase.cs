using Volo.Abp.Modularity;

namespace LuftBornTask;

/* Inherit from this class for your domain layer tests. */
public abstract class LuftBornTaskDomainTestBase<TStartupModule> : LuftBornTaskTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
