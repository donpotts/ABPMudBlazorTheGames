using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheGames.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace TheGames.Web.Pages.Games;

public class EditModalModel : TheGamesPageModel
{

    [BindProperty]
    public EditGameViewModel Game { get; set; } = null!;

    public List<SelectListItem> Publishers { get; set; } = null!;

    private readonly IGameAppService _gameAppService;

    public EditModalModel(IGameAppService gameAppService)
    {
        _gameAppService = gameAppService;
    }

    public async Task OnGetAsync(int id)
    {
        var gameDto = await _gameAppService.GetAsync(id);
        Game = ObjectMapper.Map<GameDto, EditGameViewModel>(gameDto);

        var publisherLookup = await _gameAppService.GetPublisherLookupAsync();
        Publishers = publisherLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _gameAppService.UpdateAsync(
            Game.Id,
            ObjectMapper.Map<EditGameViewModel, CreateUpdateGameDto>(Game)
        );

        return NoContent();
    }

    public class EditGameViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

 
        public string? Name { get; set; }

 
        public string? Developer { get; set; }

 
        [SelectItems(nameof(Publishers))]
        [DisplayName("Publisher")]
        public int PublisherId { get; set; }
    }
}
