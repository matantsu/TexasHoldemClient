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
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.PL.UserControls
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {

        

        public Player Player
        {
            get { return (Player)GetValue(PlayerProperty); }
            set
            {
                this.Dispatcher.Invoke(() =>
                {
                    SetValue(PlayerProperty, value); DataContext = value;
                });
            }
        }

        public static DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(Player), typeof(PlayerControl));

        public PlayerControl()
        {
            DataContext = Player;
            InitializeComponent();
        }


    }
}
