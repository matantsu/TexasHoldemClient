using CefSharp;
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

            g = BL.GameManager.Listen(37);
            g.PropertyChanged += G_PropertyChanged;
        }

        private void G_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private void Browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            MessageBox.Show(e.Line + e.Message + e.Source);
        }

        private void Browser_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {
            MessageBox.Show(e.ErrorText + e.FailedUrl);
        }
    }
}
