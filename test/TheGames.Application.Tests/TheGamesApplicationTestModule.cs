using Volo.Abp.Modularity;

namespace TheGames;

[DependsOn(
    typeof(TheGamesApplicationModule),
    typeof(TheGamesDomainTestModule)
)]
public class TheGamesApplicationTestModule : AbpModule
{

}
