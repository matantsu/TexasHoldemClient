using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TexasHoldemClient.BusinessLayer;
using TexasHoldemClient.BusinessLayer.Models;
using TexasHoldemClient.PL.UserControls;

namespace TexasHoldemClient.PL.Windows
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        Game game = new Game();
        GameManager gm = BL.GameManager;
        LinkedList<PlayerControl> playerControls = new LinkedList<PlayerControl>();
        LinkedList<Player> currentPlayers = new LinkedList<Player>();

        public GameWindow(/*int gameID*/)
        {
            //this.game = gm.Listen(gameID);

            
            InitializeComponent();

            playerControls.AddFirst(Player0);
            playerControls.AddFirst(Player1);
            playerControls.AddFirst(Player2);
            playerControls.AddFirst(Player3);
            playerControls.AddFirst(Player4);
            playerControls.AddFirst(Player5);

            foreach (PlayerControl pc in playerControls)
            {
                pc.Visibility = Visibility.Hidden;
            }

            game.PropertyChanged += GameChangeHandler;



            LinkedList<Player> playerslst = new LinkedList<Player>();
            

            Player p1 = new Player();
            p1.PlayerStatus = PlayerStatus.Check;
            Player p2 = new Player();
            p1.PlayerStatus = PlayerStatus.Check;
            Player p3 = new Player();
            p1.PlayerStatus = PlayerStatus.Check;

            playerslst.AddFirst(p1);
            playerslst.AddFirst(p2);
            playerslst.AddFirst(p3);
            game.Players = playerslst;


            /*string directory = "C:\\Users\\Barakmen\\Desktop\\אוניסרביטאת בן גוריון שנה א\\שנה ג\\סמסטר ב\\סנדא ליישום פרוייקטי תכנה\\newClient_2\\TexasHoldemClient\\PL\\Images\\";
            foreach (var file in Directory.GetFiles(directory).Where(f => f.EndsWith(".png")))
            {
                // list.Items.Add(new BitmapImage(new Uri(file)));
                // or just add the filename, a default type converter will convert it into an ImageSource
                list.Items.Add(file);
            }*/

        }

        private void GameChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            
            if (e.PropertyName == "Players")
            {

                for(int i = 0; i < playerControls.Count; i++)
                {
                    if(i < game.Players.Count())
                    {
                        playerControls.ElementAt(i).Player = game.Players.ElementAt(i);
                        playerControls.ElementAt(i).Visibility = Visibility.Visible;
                    }
                    else
                    {
                        playerControls.ElementAt(i).Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private async void Check_Click(object sender, RoutedEventArgs e)
        {
            await gm.Check(game);
        }

        private async void Fold_Click(object sender, RoutedEventArgs e)
        {
            await gm.Fold(game);
        }

        private async void Raise_Click(object sender, RoutedEventArgs e)
        {
            await gm.Raise(game, Int32.Parse(BetTextBox.Text));
        }
    }
}
