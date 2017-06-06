﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TexasHoldemClient.PL.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewGameWindow.xaml
    /// </summary>
    public partial class CreateNewGameWindow : Window
    {
        IGameManager gm = BL.GameManager;

        public CreateNewGameWindow()
        {
            InitializeComponent();
            gametype_ComboBox.ItemsSource = Enum.GetValues(typeof(GameType)).Cast<GameType>();

        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("" +
                        (GameType)Enum.GetValues(typeof(GameType)).GetValue(gametype_ComboBox.SelectedIndex) + "\n" +
                        Int32.Parse(buyin_TextBox.Text) + "\n" +
                        Int32.Parse(initialChips_TextBox.Text) + "\n" +
                        Int32.Parse(minBet_TextBox.Text) + "\n" +
                        Int32.Parse(minPlayers_TextBox.Text) + "\n" +
                        Int32.Parse(maxPlayers_TextBox.Text) + "\n" +
                        spectatingAllowed_CheckBox.IsChecked.GetValueOrDefault());

            await gm.Create(

                        (GameType)Enum.GetValues(typeof(GameType)).GetValue(gametype_ComboBox.SelectedIndex),
                        null,
                        Int32.Parse(buyin_TextBox.Text),
                        Int32.Parse(initialChips_TextBox.Text),
                        Int32.Parse(minBet_TextBox.Text),
                        Int32.Parse(minPlayers_TextBox.Text),
                        Int32.Parse(maxPlayers_TextBox.Text),
                        spectatingAllowed_CheckBox.IsChecked.GetValueOrDefault());
         
        }
    }

    public class EnumToItemsSource : MarkupExtension
    {
        private readonly Type _type;

        public EnumToItemsSource(Type type)
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(_type)
                .Cast<object>()
                .Select(e => new { Value = (int)e, DisplayName = e.ToString() });
        }
    }
}
