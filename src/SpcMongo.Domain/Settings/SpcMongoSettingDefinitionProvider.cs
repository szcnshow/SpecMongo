using Volo.Abp.Settings;

namespace SpcMongo.Settings;

public class SpcMongoSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SpcMongoSettings.MySetting1));
    }
}
