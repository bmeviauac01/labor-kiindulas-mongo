﻿using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bme.Swlab1.Mongo.Pages.Orders;

public partial class DeleteModel : PageModel
{
    private readonly IRepository _repository;

    public DeleteModel(IRepository repository)
    {
        _repository = repository;
    }

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

    public IActionResult OnPost(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        _repository.DeleteOrder(id);

        return RedirectToPage("./Index");
    }
}
