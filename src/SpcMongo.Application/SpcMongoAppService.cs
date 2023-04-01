using System;
using System.Collections.Generic;
using System.Text;
using SpcMongo.Localization;
using Volo.Abp.Application.Services;

namespace SpcMongo;

/* Inherit your application services from this class.
 */
public abstract class SpcMongoAppService : ApplicationService
{
    protected SpcMongoAppService()
    {
        LocalizationResource = typeof(SpcMongoResource);
    }
}
