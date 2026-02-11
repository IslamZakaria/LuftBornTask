using Volo.Abp.Modularity;

namespace LuftBornTask;

public abstract class LuftBornTaskApplicationTestBase<TStartupModule> : LuftBornTaskTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
