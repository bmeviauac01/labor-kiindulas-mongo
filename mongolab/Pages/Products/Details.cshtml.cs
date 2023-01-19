﻿using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bme.Swlab1.Mongo.Pages.Products;

public class DetailsModel : PageModel
{
    private readonly IRepository _repository;

    public DetailsModel(IRepository repository)
    {
        _repository = repository;
    }

    public Product Product { get; set; }

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
}
