﻿using Ekart.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {

            //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var path = Directory.GetParent(Directory.GetCurrentDirectory());

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText(path + @"/Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands!);
                context.SaveChanges();
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText(path + @"/Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types!);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText(path + @"/Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products!);
                context.SaveChanges();
            }

            //if (!context.DeliveryMethods.Any())
            //{
            //    var deliveryData = File.ReadAllText(path + @"/Data/SeedData/delivery.json");
            //    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
            //    context.DeliveryMethods.AddRange(methods);
            //}

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
