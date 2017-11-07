using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Account.Repositories;

namespace TddShop.Cli.Tests.Account.Stubs
{
    class UsernameRepositoryStub : IUsernameRepository
    {
        public bool IsInUseResult { get; set; }
        public Func<string, bool> IsInUseResultPredicate { get; set; } = (username) => false;

        public bool IsInUse(string username)
        {
            return IsInUseResult || IsInUseResultPredicate(username);
        }

        public void Create(string username, string password)
        {
            
        }
    }
}
