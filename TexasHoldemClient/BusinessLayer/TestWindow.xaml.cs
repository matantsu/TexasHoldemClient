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
       
        public TestWindow()
        {
            InitializeComponent();
            BL.GameManager.PropertyChanged += GameManager_PropertyChanged;
        }

        private void GameManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MessageBox.Show(e.PropertyName);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = await BL.UserManager.Login("newnewone1@gmail.com", "newnewone1123");
            var gid = await BL.GameManager.Create(GameType.PotLimit, "newnewone1's new game", 1, 2, 3, 4, 5, true);
        }
    }
}
