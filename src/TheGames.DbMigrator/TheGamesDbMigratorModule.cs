using TheGames.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace TheGames.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TheGamesEntityFrameworkCoreModule),
    typeof(TheGamesApplicationContractsModule)
    )]
public class TheGamesDbMigratorModule : AbpModule
{
}
