using Microsoft.AspNetCore.Builder;
using TheGames;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<TheGamesWebTestModule>();

public partial class Program
{
}
