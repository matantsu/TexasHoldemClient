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

namespace TexasHoldemClient.PL.Pages
{
    /// <summary>
    /// Interaction logic for GameCenterPage.xaml
    /// </summary>
    public partial class GameCenterPage : Page
    {
        public GameCenterPage()
        {
            InitializeComponent();

            _chatFrame.Navigate(new ChatPage());
        }
    }
}
