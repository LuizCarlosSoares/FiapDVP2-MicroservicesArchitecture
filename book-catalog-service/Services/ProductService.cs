using System.Collections.Generic;
using DatabaseSettings;
using MongoDB.Driver;
using Products;

namespace Services
{
    public class ProductServiceBase<T>: IProductService<T> where T: IProduct
    {
        private readonly IMongoCollection<T> collection;

        public ProductServiceBase(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            collection = database.GetCollection<T>(settings.CollectionName);
        }

        public IEnumerable<T> Get() =>
            collection.Find(book => true).ToEnumerable();

        public void Get(string id) =>
            collection.Find<T>(book => book.Id == id).FirstOrDefault();

        public T Create(T product)
        {
            collection.InsertOne(product);
            return product ;
        }

        public void Update(string id, T product) =>
            collection.ReplaceOne(it => it.Id == id, product);

        public void Remove(T product) =>
            collection.DeleteOne( it => it.Id == product.Id);

        public void Remove(string id) => 
            collection.DeleteOne(it => it.Id == id);

        T IProductService<T>.Get(string id)
        {
            return collection.Find(it=>it.Id.Equals(id)).FirstOrDefault();
        }
    }
        
    }
