using System.Threading.Tasks;

namespace TheGames.Data;

public interface ITheGamesDbSchemaMigrator
{
    Task MigrateAsync();
}
