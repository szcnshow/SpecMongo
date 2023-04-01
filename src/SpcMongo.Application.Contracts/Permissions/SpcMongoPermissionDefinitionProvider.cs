using SpcMongo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SpcMongo.Permissions;

public class SpcMongoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SpcMongoPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SpcMongoPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SpcMongoResource>(name);
    }
}
