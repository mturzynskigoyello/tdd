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
            throw new NotImplementedException();
        }
    }
}
