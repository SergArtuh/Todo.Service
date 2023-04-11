
namespace Todo.Service.Config;

public class MongoConfig 
{
    public string Host { get; init; }

    public int Port { get; init; }

    public String Name { get; init; }

    public string ConnectionString => $"mongodb://{Host}:{Port}";
}
