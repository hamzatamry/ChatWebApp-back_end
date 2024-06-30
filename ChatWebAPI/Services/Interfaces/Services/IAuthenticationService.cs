using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Tuple<int, string> Login(string email, string password);
        void Register(string email, string password);
    }
}
