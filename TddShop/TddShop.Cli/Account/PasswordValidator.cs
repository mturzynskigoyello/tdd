using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Account.Repositories;

namespace TddShop.Cli.Account
{
    public interface IPasswordValidator
    {
        bool IsValid(string password);
    }

    public class PasswordValidator : IPasswordValidator
    {
        /// <summary>
        /// Password must meet at least 3 out of the following 4 complexity rules
        ///     at least 1 uppercase character(A-Z)
        ///     at least 1 lowercase character(a-z)
        ///     at least 1 digit(0-9)
        ///     at least 1 special character(punctuation) — do not forget to treat space as special characters too
        ///     at least 10 characters
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValid(string password)
        {
            if (!password.Any(x => char.IsUpper(x)))
            {
                return false;
            }
            if (!password.Any(x => char.IsLower(x)))
            {
                return false;
            }
            if (!password.Any(x => char.IsDigit(x)))
            {
                return false;
            }
            if (!password.Any(x => !char.IsLetterOrDigit(x) || x == ' '))
            {
                return false;
            }
            if (password.Length < 10)
            {
                return false;
            }
            return true;
        }
    }
}