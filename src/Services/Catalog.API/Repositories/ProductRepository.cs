using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ICatalogContext _context { get; }
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(prop => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string catagoryName)
        {
            var filter = Builders<Product>.Filter.Eq(prop => prop.Category == catagoryName, true);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(prop => prop.Id == id, true);

            return await _context
                .Products
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(prop => prop.Name, name);

            return await _context
                .Products
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context
                .Products
                .InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                .Products
                .ReplaceOneAsync(filter: prod => prod.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> DeleteProduct(string id)
        {
            Expression<Func<Product, bool>> filter = prod => prod.Id == id;

            var deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;

        }
    }
}
