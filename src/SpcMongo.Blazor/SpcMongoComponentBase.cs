using SpcMongo.Localization;
using Volo.Abp.AspNetCore.Components;

namespace SpcMongo.Blazor;

public abstract class SpcMongoComponentBase : AbpComponentBase
{
    protected SpcMongoComponentBase()
    {
        LocalizationResource = typeof(SpcMongoResource);
    }
}
