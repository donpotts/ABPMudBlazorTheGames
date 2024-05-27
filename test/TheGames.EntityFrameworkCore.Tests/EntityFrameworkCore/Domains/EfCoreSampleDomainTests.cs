using TheGames.Samples;
using Xunit;

namespace TheGames.EntityFrameworkCore.Domains;

[Collection(TheGamesTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TheGamesEntityFrameworkCoreTestModule>
{

}
