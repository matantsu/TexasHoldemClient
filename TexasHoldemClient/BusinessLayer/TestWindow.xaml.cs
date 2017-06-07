using Newtonsoft.Json;
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
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        Game g;
        public TestWindow()
        {
            InitializeComponent();
            BL.GameManager.PropertyChanged += GameManager_PropertyChanged;

            g = BL.GameManager.Listen(37);
            g.PropertyChanged += G_PropertyChanged;
        }

        private void G_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private void GameManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //await BL.UserManager.Register("x@gmail.com", "xxx", "zaq1xsw2");
            await BL.UserManager.Login("x@gmail.com", "zaq1xsw2");
            
        }
    }
}
