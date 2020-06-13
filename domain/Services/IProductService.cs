using System.Collections.Generic;
using Products;

namespace Domain.Services {

    public interface IProductService<T> {

        IEnumerable<T> Get ();
        T Get (string id);

        T Create (T product);

        void Update (string id, T product);

        void Remove (T product);
        void Remove (string id);
    }
}