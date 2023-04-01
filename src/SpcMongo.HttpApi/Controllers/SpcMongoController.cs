using SpcMongo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SpcMongo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SpcMongoController : AbpControllerBase
{
    protected SpcMongoController()
    {
        LocalizationResource = typeof(SpcMongoResource);
    }
}
