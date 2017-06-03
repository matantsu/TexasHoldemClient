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
using TexasHoldemClient.Models;

namespace TexasHoldemClient
{
    /// <summary>
    /// Interaction logic for CreateNewGame.xaml
    /// </summary>
    public partial class CreateNewGame : Window
    {
        Game newGame = new Game();

        public CreateNewGame()
        {
            InitializeComponent();
            DataContext = newGame;
            gamePolicyComboBox.ItemsSource = Enum.GetValues(typeof(GamePolicy)).Cast<GamePolicy>();
        }

        private void CreateNewGame_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0}", newGame.GamePolicy);
        }
    }
}
