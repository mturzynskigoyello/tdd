using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Account.Repositories;

namespace TddShop.Cli.Account
{
    public class UserService
    {
        private readonly IUsernameValidator _usernameValidator;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IUsernameRepository _usernameRepository;

        public UserService(IUsernameValidator usernameValidator, IPasswordValidator passwordValidator,
            IUsernameRepository usernameRepository)
        {
            _usernameValidator = usernameValidator;
            _passwordValidator = passwordValidator;
            _usernameRepository = usernameRepository;
        }

        public bool CreateUser(string username, string password)
        {
            if (!_passwordValidator.IsValid(password))
            {
                return false;
            }
            if (_usernameValidator.IsValid(username) != UsernameValidationResult.Ok)
            {
                return false;
            }
            _usernameRepository.Create(username, password);
            return true;
        }
    }
}
