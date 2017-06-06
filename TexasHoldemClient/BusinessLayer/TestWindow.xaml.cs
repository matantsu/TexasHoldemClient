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
        private Game g;

        public TestWindow()
        {
            InitializeComponent();
            s();
        }

        private async void s(){
            await BL.UserManager.Login(null, null);
            g = BL.GameManager.Listen(1);
            MessageBox.Show("" + g);

            g.PropertyChanged += G_PropertyChanged;
        }

        private void G_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MessageBox.Show(JsonConvert.SerializeObject(g));
        }
    }
}
