using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.BusinessLayer.Models
{
    public class ChatMessage : Changing
    {
        private string userID;
        public string UserID
        {
            get
            {
                return userID;
            }
            set
            {
                if (userID != value)
                {
                    userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
    }
}
