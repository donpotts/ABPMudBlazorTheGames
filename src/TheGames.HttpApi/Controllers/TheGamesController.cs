using TheGames.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TheGames.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TheGamesController : AbpControllerBase
{
    protected TheGamesController()
    {
        LocalizationResource = typeof(TheGamesResource);
    }
}
