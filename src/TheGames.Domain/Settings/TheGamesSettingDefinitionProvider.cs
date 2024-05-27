using Volo.Abp.Settings;

namespace TheGames.Settings;

public class TheGamesSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TheGamesSettings.MySetting1));
    }
}
