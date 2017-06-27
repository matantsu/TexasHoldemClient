using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TexasHoldemClient.BusinessLayer;

namespace TexasHoldemClient.PL.Pages.AfterLoginPages
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        IUserManager um = BL.UserManager;

        public EditUserPage()
        {
            InitializeComponent();

        }
    }
}
