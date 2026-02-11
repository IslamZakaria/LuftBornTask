using Xunit;

namespace LuftBornTask.EntityFrameworkCore;

[CollectionDefinition(LuftBornTaskTestConsts.CollectionDefinitionName)]
public class LuftBornTaskEntityFrameworkCoreCollection : ICollectionFixture<LuftBornTaskEntityFrameworkCoreFixture>
{

}
