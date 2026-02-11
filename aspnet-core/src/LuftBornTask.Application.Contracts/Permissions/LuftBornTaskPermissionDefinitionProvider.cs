using LuftBornTask.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LuftBornTask.Permissions;

public class LuftBornTaskPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LuftBornTaskPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LuftBornTaskPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LuftBornTaskResource>(name);
    }
}
