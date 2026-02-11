using LuftBornTask.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LuftBornTask.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LuftBornTaskEntityFrameworkCoreModule),
    typeof(LuftBornTaskApplicationContractsModule)
    )]
public class LuftBornTaskDbMigratorModule : AbpModule
{
}
