using Volo.Abp.Modularity;

namespace TheGames;

/* Inherit from this class for your domain layer tests. */
public abstract class TheGamesDomainTestBase<TStartupModule> : TheGamesTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
