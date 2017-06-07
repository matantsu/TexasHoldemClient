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
    /// Interaction logic for TwoPlayerCardsControl.xaml
    /// </summary>
    public partial class TwoPlayerCardsControl : UserControl
    {


        public TwoPlayerCardsControl()
        {
            InitializeComponent();
            
            Card c0 = new Card(CardType.Club, CardRank.Ace);
            Card c1 = new Card(CardType.Spade, CardRank.Ace);

            Card0.Card = c0;
            Card1.Card = c1;
            
        }

      

    }
}
