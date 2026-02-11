using LuftBornTask.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LuftBornTask.Permissions;

public class LuftBornTaskPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var productsPermission = myGroup.AddPermission(LuftBornTaskPermissions.Products.Default, L("Permission:Products"));
        productsPermission.AddChild(LuftBornTaskPermissions.Products.Create, L("Permission:Products.Create"));
        productsPermission.AddChild(LuftBornTaskPermissions.Products.Edit, L("Permission:Products.Edit"));
        productsPermission.AddChild(LuftBornTaskPermissions.Products.Delete, L("Permission:Products.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LuftBornTaskResource>(name);
    }
}
