using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TexasHoldemClient.BusinessLayer;
using TexasHoldemClient.BusinessLayer.Models;
using TexasHoldemClient.PL.Helpers;

namespace TexasHoldemClient.PL.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewGameWindow.xaml
    /// </summary>
    public partial class CreateNewGameWindow : Window
    {

        public Bind<int> minPlayers { get; } = new Bind<int>(2);

        IGameManager gm = BL.GameManager;

        public CreateNewGameWindow()
        {
            InitializeComponent();
            ProgressBar_LoginPressed.Visibility = Visibility.Hidden;
            gametype_ComboBox.ItemsSource = Enum.GetValues(typeof(GameType)).Cast<GameType>();
            DataContext = this;
        }

        public bool IsPositiveNumber(string t)
        {
            return Regex.IsMatch(t, "^[0-9]+$");
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFormValid()) { MessageBox.Show("One or more fields are missing"); return; }
            
            try {
                Button_Submit.IsEnabled = false;
                ProgressBar_LoginPressed.Visibility = Visibility.Visible;
                await gm.Create(

                            (GameType)Enum.GetValues(typeof(GameType)).GetValue(gametype_ComboBox.SelectedIndex),
                            GameName_TextBoxExtention.UserInput,
                            Int32.Parse(buyin_TextBoxExtention.UserInput),
                            Int32.Parse(initialChips_TextBoxExtention.UserInput),
                            Int32.Parse(minBet_TextBoxExtention.UserInput),
                            (int)minPlayers_Slider.Value,
                            (int)maxPlayers_Slider.Value,
                            spectatingAllowed_CheckBox.IsChecked.GetValueOrDefault());
                MessageBox.Show("Game Created Successfully !");

            }
            catch (Exception exp)
            {
                Button_Submit.IsEnabled = true;
                MessageBox.Show(exp.Message);
            }
            ProgressBar_LoginPressed.Visibility = Visibility.Hidden;
            Close();
        }

        private void minPlayers_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            minPlayers.Data = (int)e.NewValue;
        }

        private bool IsFormValid()
        {
            return  GameName_TextBoxExtention.ValidateInput() &
                    buyin_TextBoxExtention.ValidateInput() &
                    initialChips_TextBoxExtention.ValidateInput() &
                    minBet_TextBoxExtention.ValidateInput();   
        }
    }

  
}
