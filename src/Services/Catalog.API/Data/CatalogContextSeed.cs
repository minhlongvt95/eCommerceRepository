using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> mongoCollection)
        {
            if (!mongoCollection.Find(p => true).Any())
                mongoCollection.InsertMany(GetPreConfiguredProducts());
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "546c776b3e23f5f2ebdd3b03",
                    Name = "IPhone X",
                    Category = "Smart Phone",
                    Summary = "description",
                    Description = "description",
                    ImageFile = "product-1.png",
                    Price = 84.37m
                },
                new Product()
                {
                    Id = "337c776b3e23f5f2ebdd3b12",
                    Name = "Product 2",
                    Category = "SamSung",
                    Summary = "description",
                    Description = "this is description for product 2",
                    ImageFile = "product-2.png",
                    Price = 54.67m
                },
                new Product()
                {
                    Id = "488c776b3e23f5f2ebdd3b43",
                    Name = "Product 3",
                    Category = "Smart Phone",
                    Summary = "description",
                    Description = "this is description for product 3",
                    ImageFile = "product-3.png",
                    Price = 54.23m
                },
                new Product()
                {
                    Id = "947c776b3e23f5f2ebdd3b63",
                    Name = "Product 4",
                    Category = "Smart Phone",
                    Summary = "description",
                    Description = "this is description for product 4",
                    ImageFile = "product-4.png",
                    Price = 78.32m
                },
                new Product()
                {
                    Id = "642c776b3e23f5f2ebdd3b63",
                    Name = "Product 5",
                    Category = "Smart Phone",
                    Summary = "description",
                    Description = "this is description for product 5",
                    ImageFile = "product-5.png",
                    Price = 23.64m
                }
            };
        }
    }
}
