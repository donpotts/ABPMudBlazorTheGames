using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TheGames.Data;

/* This is used if database provider does't define
 * ITheGamesDbSchemaMigrator implementation.
 */
public class NullTheGamesDbSchemaMigrator : ITheGamesDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
