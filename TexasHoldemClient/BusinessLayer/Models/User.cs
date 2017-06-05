using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.BusinessLayer.Models
{
    public class User : Changing
    {
        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        private string photoURL;
        public string PhotoURL
        {
            get { return photoURL; }
            set
            {
                if (photoURL != value)
                {
                    photoURL = value;
                    OnPropertyChanged("PhotoURL");
                }
            }
        }

        public string Username = "test1";
        public string Password = "123456";
    }
}
