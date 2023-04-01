using System.Threading.Tasks;

namespace SpcMongo.Data;

public interface ISpcMongoDbSchemaMigrator
{
    Task MigrateAsync();
}
