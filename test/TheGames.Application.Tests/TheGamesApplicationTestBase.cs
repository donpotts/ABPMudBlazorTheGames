using Volo.Abp.Modularity;

namespace TheGames;

public abstract class TheGamesApplicationTestBase<TStartupModule> : TheGamesTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
