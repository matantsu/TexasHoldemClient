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
using TexasHoldemClient.BusinessLayer;
using TexasHoldemClient.BusinessLayer.Models;
using TexasHoldemClient.PL.Helpers;

namespace TexasHoldemClient.PL.UserControls
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        IGameManager gm = BL.GameManager;



        public Bind<Game> BindGame { get; set; } = new Bind<Game>();
        public ChatControl()
        {
            InitializeComponent();
        }

        internal void setGame(Game game)
        {
            BindGame.Data = game;
            DataContext = BindGame;
        
        }
        
        internal void setGameCenter()
        {
            DataContext = gm;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext == gm)
            {
                gm.Send(MessageTextBox.Text);
            }
            else
            {
                gm.Send(BindGame.Data, MessageTextBox.Text);
            }
            MessageTextBox.Text = "";
        }
    }
}
