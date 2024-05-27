using TheGames.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace TheGames.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TheGamesPageModel : AbpPageModel
{
    protected TheGamesPageModel()
    {
        LocalizationResourceType = typeof(TheGamesResource);
    }
}
