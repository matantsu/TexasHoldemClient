using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.Exceptions
{
    public static class ExceptionControl
    {
        public static bool LoginAllow = true;
        public static bool RegisterAllow = true;
    }
    
    class LoginException : Exception 
    {
        public LoginException() : base("Cannot Login")
        {
        }
    }

    class RegestrationException : Exception
    {
        public RegestrationException() : base("Cannot Register")
        {
        }
    }

    
}
