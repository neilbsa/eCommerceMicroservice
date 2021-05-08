using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private IConfiguration _config { get; set; }
        public CatalogContext(IConfiguration config)
        {
            _config = config;
            var mongoConfig = config.GetValue<string>("DatabaseSettings:ConnectionString");
            var mongoDatabase = config.GetValue<string>("DatabaseSettings:DatabaseName");
            var collectionName = config.GetValue<string>("DatabaseSettings:CollectionName");
            var client = new MongoClient(mongoConfig);
            var database = client.GetDatabase(mongoDatabase);
            Products = database.GetCollection<Product>(collectionName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
