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

namespace TexasHoldemClient.PL.Pages.AfterLoginPages
{
    /// <summary>
    /// Interaction logic for CurrentRooms.xaml
    /// </summary>
    public partial class CurrentRooms : Page
    {
        public ObservableCollection<GameRoomPage>  gameRooms = new ObservableCollection<GameRoomPage>();
        public ObservableCollection<GameRoomPage> GameRooms { get { return gameRooms; }}
        
        public CurrentRooms()
        {
            InitializeComponent();
            Tabs_ActiveRooms.DataContext = this;
            GameRooms.Add(new GameRoomPage(37));
            GameRooms.Add(new GameRoomPage(37));
            GameRooms.Add(new GameRoomPage(37));
            GameRooms.Add(new GameRoomPage(37));

            
                
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10);
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    a();
                });
            });

        }

        public void a()
        {       
            
        }

    }
}
