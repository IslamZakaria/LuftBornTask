using Volo.Abp.Modularity;

namespace LuftBornTask;

[DependsOn(
    typeof(LuftBornTaskApplicationModule),
    typeof(LuftBornTaskDomainTestModule)
)]
public class LuftBornTaskApplicationTestModule : AbpModule
{

}
