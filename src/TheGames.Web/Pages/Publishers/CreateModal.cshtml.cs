using System.Threading.Tasks;
using TheGames.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace TheGames.Web.Pages.Publishers;

public class CreateModalModel : TheGamesPageModel
{
    [BindProperty]
    public CreateUpdatePublisherDto Publisher { get; set; } = null!;

    private readonly IPublisherAppService _publisherAppService;

    public CreateModalModel(IPublisherAppService publisherAppService)
    {
        _publisherAppService = publisherAppService;
    }

    public void OnGet()
    {
        Publisher = new CreateUpdatePublisherDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _publisherAppService.CreateAsync(Publisher);
        return NoContent();
    }
}
