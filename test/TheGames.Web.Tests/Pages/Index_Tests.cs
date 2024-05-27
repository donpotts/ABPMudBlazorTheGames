using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace TheGames.Pages;

public class Index_Tests : TheGamesWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
