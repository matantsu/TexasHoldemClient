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
using System.Windows.Shapes;
using TexasHoldemClient.bl;
using TexasHoldemClient.Models;

namespace TexasHoldemClient
{
    /// <summary>
    /// Interaction logic for GameCenter.xaml
    /// </summary>
    public partial class GameCenter : Window
    {
        NavigationManager navi = NavigationManager.instance;
        UserManager manager = UserManager.instance;

        public GameCenter()
        {
            
            InitializeComponent();
            navi.PropertyChanged += PageChange_PropertyChanged;
            DataContext = manager;
        }


        private void PageChange_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Page")
            {

            }
        }


    }
}
