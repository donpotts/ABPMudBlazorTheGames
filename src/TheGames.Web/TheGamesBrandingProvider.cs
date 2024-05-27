using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace TheGames.Web;

[Dependency(ReplaceServices = true)]
public class TheGamesBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "The Games";
}
