using System.Threading.Tasks;

namespace LuftBornTask.Data;

public interface ILuftBornTaskDbSchemaMigrator
{
    Task MigrateAsync();
}
