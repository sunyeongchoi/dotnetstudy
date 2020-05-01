using System.Collections.Generic;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract {
    public interface IProductRepository {

        IEnumerable<Product> Products { get; }

        void SavaProduct(Product product);

        Product DeleteProduct(int productID);
    }
}