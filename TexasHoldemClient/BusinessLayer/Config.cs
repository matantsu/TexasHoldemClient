using System.Net.Http;

namespace TexasHoldemClient.BusinessLayer
{
    internal class Config
    {
        public static readonly string ApiRoot = "https://us-central1-texasholdem-7ff59.cloudfunctions.net";
        public static readonly string DatabaseRoot = "https://texasholdem-7ff59.firebaseio.com/real/";
        public static readonly string FirebaseAppKey = "AIzaSyDNeJein6-7c543frBjRY-YMj30GV-9XZI"; // https://console.firebase.google.com/

        public static readonly string GoogleClientId = "989213145723-78seudgbebld842o21up0t7nml3fffhu.apps.googleusercontent.com"; // https://console.developers.google.com/apis/credentials
        public static readonly string GoogleClientSecret = "K9n32CzRfQeOXZX-miDWxrde";

        public static string ProjectID { get; internal set; }
    }
}