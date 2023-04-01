using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SpcMongo.Blazor;

[Dependency(ReplaceServices = true)]
public class SpcMongoBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SpcMongo";
}
