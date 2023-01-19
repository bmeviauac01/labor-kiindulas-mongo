using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Bme.Swlab1.Mongo.Dal;

public static class MongoConnectionConfig
{
    public static void AddMongoConnection(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            // A SZERVER ELERESET MEGVALTOZTATHATOD
            // YOU CAN CHANGE THE SERVER ADDRESS IF YOU NEED TO
            return new MongoClient(@"mongodb://localhost:27017");
        });

        services.AddSingleton<IMongoDatabase>(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();

            // AZ ADATBAZIS NEVET MEGVALTOZTATHATOD
            // YOU CAN CHANGE THE DB NAME IF YOU NEED TO
            return client.GetDatabase("datadriven");
        });

        ConventionRegistry.Register(
            "MyConventions",
            new ConventionPack
            {
                new ElementNameConvention(),
            },
            filter: _ => true);
    }
}
