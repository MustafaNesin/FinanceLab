namespace FinanceLab.Shared.Application.Constants;

public static class ApiRouteConstants
{
    private const string Api = "api/";

    private const string UserName = "{userName}/";
    private const string List = "list/";

    #region Markets

    private const string Markets = "markets/";

    public const string GetMarket = $"{Api}{Markets}";
    public const string GetMarketList = $"{Api}{Markets}{List}";

    #endregion

    #region Trades

    private const string Trades = "trades/";

    public const string GetTrade = $"{Api}{Users}{UserName}{Trades}";
    public const string GetTradeList = $"{Api}{Users}{UserName}{Trades}{List}";
    public const string PostTrade = $"{Api}{Users}{UserName}{Trades}";

    #endregion

    #region Transfers

    private const string Transfers = "transfers/";

    public const string GetTransfer = $"{Api}{Users}{UserName}{Transfers}";
    public const string GetTransferList = $"{Api}{Users}{UserName}{Transfers}{List}";
    public const string PostTransfer = $"{Api}{Users}{UserName}{Transfers}";

    #endregion

    #region Wallets

    private const string Wallets = "wallets/";

    public const string GetWallet = $"{Api}{Users}{UserName}{Wallets}";
    public const string GetWalletList = $"{Api}{Users}{UserName}{Wallets}{List}";

    #endregion

    #region Users

    private const string Users = "users/";

    public const string GetUser = $"{Api}{Users}";
    public const string GetUserList = $"{Api}{Users}{List}";

    #region Authentication

    private const string Authentication = "auth/";

    public const string SignIn = $"{Api}{Authentication}signin";
    public const string SignOut = $"{Api}{Authentication}signout";
    public const string SignUp = $"{Api}{Authentication}signup";

    #endregion

    #endregion
}