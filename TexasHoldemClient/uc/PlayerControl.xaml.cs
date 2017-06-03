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
using TexasHoldemClient.Models;

namespace TexasHoldemClient.uc
{
    

    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public Player Player
        {
            get { return (Player)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public bool IsDealer
        {
            get { return (bool)GetValue(IsDealerProperty); }
            set {
                SetValue(IsDealerProperty, value);
                DealerImage.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsCurrentPlayer
        {
            get { return (bool)GetValue(IsCurrentPlayerProperty); }
            set {
                SetValue(IsCurrentPlayerProperty, value);
                Root.Background = value ? Brushes.Aqua : Brushes.Transparent;
            }
        }

        public static DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(Player), typeof(PlayerControl));
        public static DependencyProperty IsDealerProperty = DependencyProperty.Register("IsDealer", typeof(bool), typeof(PlayerControl));
        public static DependencyProperty IsCurrentPlayerProperty = DependencyProperty.Register("IsCurrentPlayer", typeof(bool), typeof(PlayerControl));

        public PlayerControl()
        {
            InitializeComponent();
            Player = new Player();
            DataContext = Player;
        }
    }
}
