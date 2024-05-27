using Xunit;

namespace TheGames.EntityFrameworkCore;

[CollectionDefinition(TheGamesTestConsts.CollectionDefinitionName)]
public class TheGamesEntityFrameworkCoreCollection : ICollectionFixture<TheGamesEntityFrameworkCoreFixture>
{

}
