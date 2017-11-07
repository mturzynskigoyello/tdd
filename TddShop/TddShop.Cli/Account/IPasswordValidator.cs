namespace TddShop.Cli.Account
{
    public interface IPasswordValidator
    {
        bool IsValid(string password);
    }
}