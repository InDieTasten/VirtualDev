using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace VirtualDev.Database
{
    public static class Extensions
    {
        /// <summary>
        /// Registers IMongoDatabase to the service collection by specifying a factory method internally
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mongoDbOptions"></param>
        public static void AddMongoDb(this IServiceCollection services, Action<MongoDbOptions> mongoDbOptions)
        {
            services.Configure(mongoDbOptions);
            services.AddScoped(typeof(IMongoDatabase), (sp) =>
            {
                MongoDbOptions options = sp.GetRequiredService<IOptions<MongoDbOptions>>().Value;
                var client = new MongoClient(options.ConnectionString);
                IMongoDatabase db = client.GetDatabase(options.DatabaseName);
                return db;
            });
        }
    }
}
