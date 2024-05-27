using System;
using System.Collections.Generic;
using System.Text;
using TheGames.Localization;
using Volo.Abp.Application.Services;

namespace TheGames;

/* Inherit your application services from this class.
 */
public abstract class TheGamesAppService : ApplicationService
{
    protected TheGamesAppService()
    {
        LocalizationResource = typeof(TheGamesResource);
    }
}
