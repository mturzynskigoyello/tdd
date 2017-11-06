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

    /// <summary>
    /// Somebody already wrote the tests for you.
    /// Tests are great source of requirements - you can read from them how the class should work.
    /// </summary>
    public class UsernameValidator
    {
        private readonly IUsernameRepository _usernameRepository;

        public UsernameValidator(IUsernameRepository usernameRepository)
        {
            _usernameRepository = usernameRepository;
        }

        public UsernameValidationResult IsValid(string username)
        {
            throw new NotImplementedException();
        }
    }
}
