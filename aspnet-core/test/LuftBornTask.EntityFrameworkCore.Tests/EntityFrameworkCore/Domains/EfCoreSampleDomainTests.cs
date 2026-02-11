using LuftBornTask.Samples;
using Xunit;

namespace LuftBornTask.EntityFrameworkCore.Domains;

[Collection(LuftBornTaskTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LuftBornTaskEntityFrameworkCoreTestModule>
{

}
