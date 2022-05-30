using FinanceLab.Client.Domain.Models;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace FinanceLab.Client.Presentation.Pages;

[UsedImplicitly]
public partial class TradeListPage
{
    private readonly TradeListInput _input = new();

    [Parameter]
    public string UserName { get; set; } = default!;

    public ICollection<TradeDto> TradeList { get; set; } = new List<TradeDto>();

    public async void OnGetAsync()
    {
        _input.UserName = UserName;

        var response =
            await HttpClientService.PostAsync<TradeListInput, ICollection<TradeDto>>(ApiRouteConstants.GetTradeList,
                _input);

        if (response.IsSuccessful)
        {
            TradeList = response.Data.ToList();
            Console.WriteLine(response.Data);
        }
        else
        {
            ShowProblem(response.ProblemDetails, false);
            Console.WriteLine(response.ProblemDetails);
        }
    }

    protected override void OnParametersSet()
    {
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        UserName ??= StateContainer.User?.UserName!;

        if (StateContainer.User?.UserName == UserName ||
            StateContainer.User?.Role is RoleConstants.Admin)
            return;

        ShowProblem(new ProblemDetails {Title = L["NotAuthorized"]}, false);
        NavigationManager.NavigateTo("/Wallets");
    }
}