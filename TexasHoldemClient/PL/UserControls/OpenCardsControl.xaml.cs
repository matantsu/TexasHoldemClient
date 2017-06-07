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
    /// Interaction logic for OpenCardsControl.xaml
    /// </summary>
    public partial class OpenCardsControl : UserControl
    {

        LinkedList<CardControl> cardControls = new LinkedList<CardControl>();

        public LinkedList<Card> Cards
        {
            get { return (LinkedList<Card>)GetValue(CardsProperty); }
            set {
                SetValue(CardsProperty, value);
                DataContext = value;
                for (int i = 0; i < cardControls.Count; i++)
                {
                    if (i < value.Count())
                    {
                        cardControls.ElementAt(i).Card = value.ElementAt(i);
                        cardControls.ElementAt(i).Visibility = Visibility.Visible;
                    }
                    else
                    {
                        cardControls.ElementAt(i).Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        public static DependencyProperty CardsProperty = DependencyProperty.Register("Cards", typeof(LinkedList<Card>), typeof(OpenCardsControl));

        public OpenCardsControl()
        {
            InitializeComponent();
            cardControls.AddFirst(Card0);
            cardControls.AddFirst(Card1);
            cardControls.AddFirst(Card2);
            cardControls.AddFirst(Card3);
            cardControls.AddFirst(Card4);

            foreach (CardControl cc in cardControls)
            {
                cc.Visibility = Visibility.Hidden;
            }

       
        }


    }
}
