using System.Threading.Tasks;
using TheGames.Localization;
using TheGames.MultiTenancy;
using TheGames.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace TheGames.Web.Menus;

public class TheGamesMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<TheGamesResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                TheGamesMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "TheGames",
                l["Menu:TheGames"],
                icon: "fa fa-database")
            .AddItem(
                new ApplicationMenuItem(
                    "TheGames.Publishers",
                    l["Menu:Publishers"],
                    url: "/Publishers"
                ).RequirePermissions(TheGamesPermissions.Publishers.Default) // Check the permission!
            )
            .AddItem(
                new ApplicationMenuItem(
                    "TheGames.Games",
                    l["Menu:Games"],
                    url: "/Games"
                ).RequirePermissions(TheGamesPermissions.Games.Default) // Check the permission!
            )
        );

        return Task.CompletedTask;
    }
}
