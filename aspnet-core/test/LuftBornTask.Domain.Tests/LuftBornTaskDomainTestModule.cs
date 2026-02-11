using Volo.Abp.Modularity;

namespace LuftBornTask;

[DependsOn(
    typeof(LuftBornTaskDomainModule),
    typeof(LuftBornTaskTestBaseModule)
)]
public class LuftBornTaskDomainTestModule : AbpModule
{

}
