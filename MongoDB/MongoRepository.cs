using System.Linq.Expressions;
using MongoDB.Driver;
using Todo.Service.Interfaces;


public class MongoRepository<T> : IRepository<T> where T: IEntity
{
    private readonly MongoDB.Driver.IMongoCollection<T> itemCollection;
    FilterDefinitionBuilder<T> filterBuilder = new();

    public MongoRepository(IMongoDatabase dbMongo, string colectionName)
    {
        itemCollection = dbMongo.GetCollection<T>(colectionName);           
    }


    public IEnumerable<T> GetAll()
    {
        FilterDefinitionBuilder<T> filterBuilder = new();
        return itemCollection.FindSync(filterBuilder.Empty).ToEnumerable();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
    {
        return itemCollection.FindSync(filter).ToEnumerable();
    }

    public T Get(Guid id)
    {
        FilterDefinition<T> filterDefinition = filterBuilder.Eq(entity => entity.Id, id);
        return itemCollection.Find(filterDefinition).FirstOrDefault();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        return itemCollection.Find(filter).FirstOrDefault();
    }

    public void Create(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException();
        }

        itemCollection.InsertOne(item);
    }

    public void Update(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException();
        }

        FilterDefinition<T> filterDefinition = filterBuilder.Eq(entity => entity.Id, item.Id);
        itemCollection.ReplaceOne(filterDefinition, item);
    }

    public void Remove(Guid id)
    {
        FilterDefinition<T> filterDefinition = filterBuilder.Eq(entity => entity.Id, id);
        itemCollection.DeleteOne(filterDefinition);
    }
}
