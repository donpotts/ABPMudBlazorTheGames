using Volo.Abp.Modularity;

namespace TheGames;

[DependsOn(
    typeof(TheGamesDomainModule),
    typeof(TheGamesTestBaseModule)
)]
public class TheGamesDomainTestModule : AbpModule
{

}
