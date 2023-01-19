using Bme.Swlab1.Mongo.Dal;

namespace Bme.Swlab1.Mongo;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddMongoConnection();
        builder.Services.AddTransient<IRepository, Repository>();

        var app = builder.Build();

        app.MapRazorPages();

        app.Run();
    }
}
