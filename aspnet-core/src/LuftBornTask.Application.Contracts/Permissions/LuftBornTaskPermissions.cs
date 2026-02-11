namespace LuftBornTask.Permissions;

public static class LuftBornTaskPermissions
{
    public const string GroupName = "LuftBornTask";

    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
