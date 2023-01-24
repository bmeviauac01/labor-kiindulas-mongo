using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bme.Swlab1.Mongo.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IRepository _repository;

    public IndexModel(IRepository repository)
    {
        _repository = repository;
    }

    public IList<Product> Products { get; set; }

    public void OnGet()
    {
        Products = _repository.ListProducts();
    }
}
