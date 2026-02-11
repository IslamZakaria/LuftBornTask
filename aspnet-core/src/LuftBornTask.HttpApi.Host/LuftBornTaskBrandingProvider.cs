using Microsoft.Extensions.Localization;
using LuftBornTask.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LuftBornTask;

[Dependency(ReplaceServices = true)]
public class LuftBornTaskBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<LuftBornTaskResource> _localizer;

    public LuftBornTaskBrandingProvider(IStringLocalizer<LuftBornTaskResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
