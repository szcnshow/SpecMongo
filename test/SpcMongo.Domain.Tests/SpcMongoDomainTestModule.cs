using SpcMongo.MongoDB;
using Volo.Abp.Modularity;

namespace SpcMongo;

[DependsOn(
    typeof(SpcMongoMongoDbTestModule)
    )]
public class SpcMongoDomainTestModule : AbpModule
{

}
