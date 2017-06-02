using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.bl
{
    public class NavigationManager : Observable
    {
        private NavigationManager()
        {

        }
        public static NavigationManager instance = new NavigationManager();

        private object page;
        public object Page
        {
            get { return page; }
            set
            {
                if (page != value)
                {
                    page = value;
                    OnPropertyChanged("Page");
                }
            }
        }

       
    }
}
