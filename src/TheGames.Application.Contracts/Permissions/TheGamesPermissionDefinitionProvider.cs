using TheGames.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TheGames.Permissions;

public class TheGamesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var theGamesGroup = context.AddGroup(TheGamesPermissions.GroupName, L("Permission:TheGames"));

        var publishersPermission = theGamesGroup.AddPermission(TheGamesPermissions.Publishers.Default, L("Permission:Publishers"));
        publishersPermission.AddChild(TheGamesPermissions.Publishers.Create, L("Permission:Publishers.Create"));
        publishersPermission.AddChild(TheGamesPermissions.Publishers.Edit, L("Permission:Publishers.Edit"));
        publishersPermission.AddChild(TheGamesPermissions.Publishers.Delete, L("Permission:Publishers.Delete"));
        var gamesPermission = theGamesGroup.AddPermission(TheGamesPermissions.Games.Default, L("Permission:Games"));
        gamesPermission.AddChild(TheGamesPermissions.Games.Create, L("Permission:Games.Create"));
        gamesPermission.AddChild(TheGamesPermissions.Games.Edit, L("Permission:Games.Edit"));
        gamesPermission.AddChild(TheGamesPermissions.Games.Delete, L("Permission:Games.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TheGamesResource>(name);
    }
}
