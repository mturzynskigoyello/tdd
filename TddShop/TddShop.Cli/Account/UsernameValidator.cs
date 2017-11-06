using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Account.Repositories;

namespace TddShop.Cli.Account
{
    public enum UsernameValidationResult
    {
        Ok,
        InvalidUsername,
        UsernameInUse
    }

    public class UsernameValidator
    {
        private readonly IUsernameRepository _usernameRepository;

        public UsernameValidator(IUsernameRepository usernameRepository)
        {
            _usernameRepository = usernameRepository;
        }

        public UsernameValidationResult IsValid(string username)
        {
            if (!IsValidName(username))
            {
                return UsernameValidationResult.InvalidUsername;
            }
            if (_usernameRepository.IsInUse(username))
            {
                return UsernameValidationResult.UsernameInUse;
            }
            return UsernameValidationResult.Ok;
        }

        private bool IsValidName(string username)
        {
            return username.All(x => char.IsLetterOrDigit(x));
        }
    }
}
