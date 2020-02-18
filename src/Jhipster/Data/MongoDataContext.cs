using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MyCompany.Data
{
    public class MongoDataContext<TModel>
        where TModel : IModel
    {
        private readonly IMongoDatabase database;
        private readonly string collectionName = MongoCollections.TypeCollectionMap[typeof(TModel)];


        public MongoDataContext(IMongoDatabase database)
        {
            this.database = database;
        }

        public IMongoCollection<TModel> Collection => database.GetCollection<TModel>(collectionName);

        public IMongoQueryable<TModel> Queryable => Collection.AsQueryable().OfType<TModel>();

    }
}
