using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bme.Swlab1.Mongo.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IRepository _repository;

    public IndexModel(IRepository repository)
    {
        _repository = repository;
    }

    public IList<Order> Orders { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Status { get; set; }

    public void OnGet()
    {
        Orders = _repository.ListOrders(Status);
    }
}
