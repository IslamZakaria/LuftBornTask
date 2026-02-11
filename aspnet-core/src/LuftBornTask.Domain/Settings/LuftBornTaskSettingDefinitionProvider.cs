using Volo.Abp.Settings;

namespace LuftBornTask.Settings;

public class LuftBornTaskSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(LuftBornTaskSettings.MySetting1));
    }
}
