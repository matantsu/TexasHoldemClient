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
    /// Interaction logic for CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); DataContext = value; }
        }

        public static DependencyProperty CardProperty = DependencyProperty.Register("Card", typeof(Card), typeof(Card));


        public string DisplayedImagePath
        {
            get { return @"~\\..\\Images\\1.png"; }
        }
        

        public CardControl()
        {
            DataContext = Card; 
            InitializeComponent();
        }

     
    }
}
