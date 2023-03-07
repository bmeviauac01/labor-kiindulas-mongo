using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

namespace Bme.Swlab1.Mongo.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly IRepository _repository;

    public CreateModel(IRepository repository)
    {
        _repository = repository;
    }

    public SelectList Products { get; set; }

    [BindProperty]
    public Order Order { get; set; }

    [BindProperty]
    [Required]
    public string ProductID { get; set; }

    [BindProperty]
    [Range(1, double.PositiveInfinity)]
    public int Amount { get; set; }

    public void OnGet()
    {
        var termekek = _repository.ListProducts();
        Products = new SelectList(termekek, nameof(Product.Id), nameof(Product.Name));
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var termek = _repository.FindProduct(ProductID);
        _repository.InsertOrder(Order, termek, Amount);

        return RedirectToPage("./Index");
    }
}