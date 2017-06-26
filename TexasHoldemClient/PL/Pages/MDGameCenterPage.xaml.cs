using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TexasHoldemClient.BusinessLayer;
using TexasHoldemClient.PL.Helpers;
using TexasHoldemClient.PL.Models;
using TexasHoldemClient.PL.Pages.AfterLoginPages;
using TexasHoldemClient.PL.UserControls;

namespace TexasHoldemClient.PL.Pages
{

    /// <summary>
    /// Interaction logic for MDGameCenterPage.xaml
    /// </summary>
    public partial class MDGameCenterPage : Page
    {
        public DemoItem[] DemoItems { get; }
        IUserManager um = BL.UserManager;
        IGameManager gm = BL.GameManager;

        public MDGameCenterPage()
        {
            InitializeComponent();
            DataContext = this;
            DemoItem homeItem = new DemoItem("Home", new HomePage());
            DemoItems = new[]
            {
                homeItem,
                new DemoItem("Active Games", new ActiveGamesPage()),
                new DemoItem("Avaliable Games", new AvaliableGamesPage()),
                new DemoItem("Replay  Games", new ReplayGamesPage()),
            };
            
            _mainFrame.Navigate(homeItem.Content);
            
        }



        
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            DemoItem di = (sender as ListBox).SelectedItem as DemoItem;
            _mainFrame.Navigate(di.Content);
            

            MenuToggleButton.IsChecked = false;
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new UserControls.SampleMessageDialog
            {
                Message = { Text = ((ButtonBase)sender).Content.ToString() }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        private async void EditProfile_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private async void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            await um.Logout(null, null);
        }

    }
}
