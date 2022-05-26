namespace FinanceLab.Shared.Application.Constants;

public static class ApiRouteConstants
{
    private const string Api = "api";
    private const string User = "users";

    public const string UserGet = $"{Api}/{User}";
    public const string UserGetList = $"{Api}/{User}/list";
    public const string UserSignIn = $"{Api}/{User}/sign-in";
    public const string UserSignOut = $"{Api}/{User}/sign-out";
    public const string UserSignUp = $"{Api}/{User}/sign-up";

    public const string Trade = "trade";
    public const string Wallet = "wallet";
    public const string Transfer = "transfer";
    public const string Username = "{username}";

    public const string TradeGet = $"{Api}/{Username}/{Trade}";
    public const string WalletGet = $"{Api}/{Username}/{Wallet}";
    public const string TransferGet = $"{Api}/{Username}/{Transfer}";
}