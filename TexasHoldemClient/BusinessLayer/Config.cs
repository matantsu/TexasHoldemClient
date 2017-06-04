using System.Net.Http;

namespace TexasHoldemClient.BusinessLayer
{
    internal class Config
    {
        public static readonly string API_ROOT = "https://us-central1-texasholdem-7ff59.cloudfunctions.net";
        public static readonly string DATABASE_ROOT = "https://texasholdem-7ff59.firebaseio.com/";
    }
}