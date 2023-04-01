using SpcMongo.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SpcMongo.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SpcMongoMongoDbModule),
    typeof(SpcMongoApplicationContractsModule)
    )]
public class SpcMongoDbMigratorModule : AbpModule
{

}
