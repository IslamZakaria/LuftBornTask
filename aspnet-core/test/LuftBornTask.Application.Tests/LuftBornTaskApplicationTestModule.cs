using Volo.Abp.Modularity;
using LuftBornTask.EntityFrameworkCore;

namespace LuftBornTask;

[DependsOn(
    typeof(LuftBornTaskApplicationModule),
    typeof(LuftBornTaskDomainTestModule),
    typeof(LuftBornTaskEntityFrameworkCoreTestModule)
)]
public class LuftBornTaskApplicationTestModule : AbpModule
{

}
