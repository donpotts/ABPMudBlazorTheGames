using System;
using System.Threading.Tasks;
using TheGames.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace TheGames.Web.Pages.Publishers;

public class EditModalModel : TheGamesPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public CreateUpdatePublisherDto Publisher { get; set; } = null!;

    private readonly IPublisherAppService _publisherAppService;

    public EditModalModel(IPublisherAppService publisherAppService)
    {
        _publisherAppService = publisherAppService;
    }

    public async Task OnGetAsync()
    {
        var publisherDto = await _publisherAppService.GetAsync(Id);
        Publisher = ObjectMapper.Map<PublisherDto, CreateUpdatePublisherDto>(publisherDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _publisherAppService.UpdateAsync(Id, Publisher);
        return NoContent();
    }
}
