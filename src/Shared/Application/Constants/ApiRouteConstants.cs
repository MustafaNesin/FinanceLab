namespace FinanceLab.Shared.Application.Constants;

public static class ApiRouteConstants
{
    private const string Api = "Api";
    private const string User = "User";

    public const string UserGet = $"{Api}/{User}";
    public const string UserSignIn = $"{Api}/{User}/SignIn";
    public const string UserSignOut = $"{Api}/{User}/SignOut";
    public const string UserSignUp = $"{Api}/{User}/SignUp";
}