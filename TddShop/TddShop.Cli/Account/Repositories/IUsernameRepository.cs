using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddShop.Cli.Account.Repositories
{
    public interface IUsernameRepository
    {
        bool IsInUse(string username);
        void Create(string username, string password);
    }
}
