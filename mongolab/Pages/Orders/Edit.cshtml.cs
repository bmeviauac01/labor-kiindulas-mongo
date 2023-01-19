using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bme.Swlab1.Mongo.Pages.Orders;

public class EditModel : PageModel
{
    private readonly IRepository _repository;

    public EditModel(IRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public Order Order { get; set; }

    public IActionResult OnGet(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Order = _repository.FindOrder(id);
        if (Order == null)
        {
            return NotFound();
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return _repository.UpdateOrder(Order)
            ? RedirectToPage("./Index")
            : NotFound();
    }
}
