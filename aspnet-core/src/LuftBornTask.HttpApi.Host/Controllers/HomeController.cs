using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace LuftBornTask.Controllers;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/Account/Login");
    }
}
