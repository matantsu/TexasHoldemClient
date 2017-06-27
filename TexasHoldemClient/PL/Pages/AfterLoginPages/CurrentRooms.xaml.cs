using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.PL.Pages.AfterLoginPages
{

    public sealed class TabItem
    {
        public string Header { get; set; }
        public Page Content { get; set; }
    }

    /// <summary>
    /// Interaction logic for CurrentRooms.xaml
    /// </summary>
    public partial class CurrentRooms : Page
    {
        public ObservableCollection<TabItem> Tabs { get; set; }


        public CurrentRooms()
        {
            InitializeComponent();
            Tabs_ActiveRooms.DataContext = this;

            Tabs = new ObservableCollection<TabItem>();
/*            Tabs.Add(new TabItem { Header = "One", Content = new GameRoomPage(37) });
            Tabs.Add(new TabItem { Header = "Two", Content = new GameRoomPage(38) });*/


          
        }

        public void addGame(Game g)
        {
            Tabs.Add(new TabItem { Header = "Game " + g.ID, Content = new GameRoomPage(g.ID)});
        }


    }
}
