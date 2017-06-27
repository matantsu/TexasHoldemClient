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
using TexasHoldemClient.PL.Helpers;

namespace TexasHoldemClient.PL.UserControls
{
    /// <summary>
    /// Interaction logic for RuleUserControl.xaml
    /// </summary>
    public partial class RuleUserControl : UserControl
    {
        public Bind<string> BindPath { get; set; } = new Bind<string>("");
        public RuleUserControl(string path)
        {
            InitializeComponent();
            photo.DataContext = BindPath;
            BindPath.Data = path;
        }
       
    }
}
