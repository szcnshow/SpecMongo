using SpcMongo.MongoDB;
using Xunit;

namespace SpcMongo;

[CollectionDefinition(SpcMongoTestConsts.CollectionDefinitionName)]
public class SpcMongoDomainCollection : SpcMongoMongoDbCollectionFixtureBase
{

}
