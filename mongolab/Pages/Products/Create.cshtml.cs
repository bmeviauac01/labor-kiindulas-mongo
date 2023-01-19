using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bme.Swlab1.Mongo.Pages.Products;

public class CreateModel : PageModel
{
    private readonly IRepository _repository;

    public CreateModel(IRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Product Product { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _repository.InsertProduct(Product);

        return RedirectToPage("./Index");
    }
}