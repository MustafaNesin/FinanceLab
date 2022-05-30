namespace FinanceLab.Shared.Application.Constants;

public static class ApiRouteConstants
{
    private const string Api = "/api";
    private const string List = "/list";
    private const string Authentication = "/auth";
    private const string Markets = "/markets";
    private const string Trades = "/trades";
    private const string Transfers = "/transfers";
    private const string Users = "/users";
    private const string Wallets = "/wallets";

    public const string SignIn = $"{Api}{Authentication}/signin";
    public const string SignOut = $"{Api}{Authentication}/signout";
    public const string SignUp = $"{Api}{Authentication}/signup";

    public const string GetMarketList = $"{Api}{Markets}{List}";
    public const string CheckMarketExist = $"{Api}{Markets}";

    public const string GetTradeList = $"{Api}{Trades}{List}";
    public const string PostTrade = $"{Api}{Trades}";

    public const string GetTransferList = $"{Api}{Transfers}{List}";
    public const string PostTransfer = $"{Api}{Transfers}";

    public const string GetWallet = $"{Api}{Wallets}";
    public const string GetWalletList = $"{Api}{Wallets}{List}";

    public const string GetUser = $"{Api}{Users}";
    public const string GetUserList = $"{Api}{Users}{List}";
    public const string NewGame = $"{Api}{Users}/newgame";
}