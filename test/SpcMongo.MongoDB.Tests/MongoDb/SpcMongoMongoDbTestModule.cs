using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace SpcMongo.MongoDB;

[DependsOn(
    typeof(SpcMongoTestBaseModule),
    typeof(SpcMongoMongoDbModule)
    )]
public class SpcMongoMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = SpcMongoMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                                   "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}
