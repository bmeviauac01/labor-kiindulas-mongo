using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bme.Swlab1.Mongo.Pages.Customers;

public partial class IndexModel : PageModel
{
    private readonly IRepository _repository;

    public IndexModel(IRepository repository)
    {
        _repository = repository;
    }

    public IList<Customer> Customers { get; set; }

    public void OnGet()
    {
        Customers = _repository.ListCustomers();
    }
}