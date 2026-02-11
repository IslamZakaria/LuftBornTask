using LuftBornTask.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LuftBornTask.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LuftBornTaskController : AbpControllerBase
{
    protected LuftBornTaskController()
    {
        LocalizationResource = typeof(LuftBornTaskResource);
    }
}
