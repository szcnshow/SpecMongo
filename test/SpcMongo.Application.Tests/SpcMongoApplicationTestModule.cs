using Volo.Abp.Modularity;

namespace SpcMongo;

[DependsOn(
    typeof(SpcMongoApplicationModule),
    typeof(SpcMongoDomainTestModule)
    )]
public class SpcMongoApplicationTestModule : AbpModule
{

}
