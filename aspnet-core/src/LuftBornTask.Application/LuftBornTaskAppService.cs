using System;
using System.Collections.Generic;
using System.Text;
using LuftBornTask.Localization;
using Volo.Abp.Application.Services;

namespace LuftBornTask;

/* Inherit your application services from this class.
 */
public abstract class LuftBornTaskAppService : ApplicationService
{
    protected LuftBornTaskAppService()
    {
        LocalizationResource = typeof(LuftBornTaskResource);
    }
}
