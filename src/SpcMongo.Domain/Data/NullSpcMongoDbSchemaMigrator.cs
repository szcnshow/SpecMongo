using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SpcMongo.Data;

/* This is used if database provider does't define
 * ISpcMongoDbSchemaMigrator implementation.
 */
public class NullSpcMongoDbSchemaMigrator : ISpcMongoDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
