using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

namespace Bme.Swlab1.Mongo.Pages.Products;

public class BuyModel : PageModel
{
    private readonly IRepository _repository;

    public BuyModel(IRepository repository)
    {
        _repository = repository;
    }

    public Product Product { get; set; }

    [BindProperty]
    [Range(1, double.PositiveInfinity)]
    public int Amount { get; set; }

    public IActionResult OnGet(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Product = _repository.FindProduct(id);
        if (Product == null)
        {
            return NotFound();
        }

        return Page();
    }

    public IActionResult OnPost(string id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var success = _repository.SellProduct(id, Amount);
        if (success)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            Product = _repository.FindProduct(id);
            ModelState.AddModelError(nameof(Amount), "Not enough product in stock!");
            return Page();
        }
    }
}
