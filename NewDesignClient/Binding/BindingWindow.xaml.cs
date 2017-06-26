using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace NewDesignClient.Binding
{
    /// <summary>
    /// Interaction logic for BindingWindow.xaml
    /// </summary>
    public partial class BindingWindow : Window
    {
        public Bind<int> bindTest { get; } = new Bind<int>(1);

        public BindingWindow()
        {
            InitializeComponent();

            DataContext = this;

            int interval = 7000;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(interval);
                a();
            });

        }

        public void a()
        {
            bindTest.Data = 2;
        }


    }
}
