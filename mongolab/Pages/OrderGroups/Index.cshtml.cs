using Bme.Swlab1.Mongo.Dal;
using Bme.Swlab1.Mongo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

namespace Bme.Swlab1.Mongo.Pages.OrderGroups;

public partial class IndexModel : PageModel
{
    private readonly IRepository _repository;

    public IndexModel(IRepository repository)
    {
        _repository = repository;
    }

    public IList<DateTime> Thresholds { get; set; }
    public Dictionary<DateTime, OrderGroup> Groups { get; set; }

    [BindProperty(SupportsGet = true)]
    [Range(1, double.PositiveInfinity)]
    public int GroupCount { get; set; } = 5;

    public void OnGet()
    {
        if (!ModelState.IsValid)
        {
            return;
        }

        var orderGroups = _repository.GroupOrders(GroupCount);
        Thresholds = orderGroups.Thresholds;
        Groups = orderGroups.Groups.ToDictionary(cs => cs.Date);
    }
}