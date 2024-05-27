using TheGames.Samples;
using Xunit;

namespace TheGames.EntityFrameworkCore.Applications;

[Collection(TheGamesTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TheGamesEntityFrameworkCoreTestModule>
{

}
