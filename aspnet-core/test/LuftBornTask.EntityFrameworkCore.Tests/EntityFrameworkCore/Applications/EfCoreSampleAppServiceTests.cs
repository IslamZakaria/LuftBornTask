using LuftBornTask.Samples;
using Xunit;

namespace LuftBornTask.EntityFrameworkCore.Applications;

[Collection(LuftBornTaskTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LuftBornTaskEntityFrameworkCoreTestModule>
{

}
