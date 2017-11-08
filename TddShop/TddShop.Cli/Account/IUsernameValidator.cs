namespace TddShop.Cli.Account
{
    public interface IUsernameValidator
    {
        UsernameValidationResult IsValid(string username);
    }
}