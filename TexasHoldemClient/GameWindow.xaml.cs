using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TexasHoldemClient.bl;
using TexasHoldemClient.Models;
using TexasHoldemClient.uc;

namespace TexasHoldemClient
{

    class ChatMessage : Observable
    {
        private string message = "hello";
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        private User user = new User();
        public User User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                }
            }
        }
    }

    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private ICollection<PlayerControl> playerControls = new List<PlayerControl>();


        ObservableCollection<ChatMessage> messages = new ObservableCollection<ChatMessage>();
        Game game = new Game();

        public GameWindow()
        {
            InitializeComponent();
            playerControls.Add(Player0);
            playerControls.Add(Player1);
            playerControls.Add(Player2);

            Chat.ItemsSource = messages;



            game.Players.CollectionChanged += Players_CollectionChanged;

            game.Players.Add(new Player());
            game.Players.Add(new Player());
        }

        private void Players_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for(int i = 0; i < playerControls.Count; i++)
            {
                if(i < game.Players.Count)
                {
                    playerControls.ElementAt(i).Player = game.Players.ElementAt(i);
                    playerControls.ElementAt(i).IsCurrentPlayer = game.Players.ElementAt(i) == game.CurrentPlayer;
                    playerControls.ElementAt(i).IsDealer = game.Players.ElementAt(i) == game.Dealer;
                    playerControls.ElementAt(i).Visibility = Visibility.Visible;
                }
                else
                {
                    playerControls.ElementAt(i).Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            SendButton.IsEnabled = false;
            ChatTextBox.IsEnabled = false;
            Task<bool> sendTask = send(ChatTextBox.Text);
            ChatTextBox.Text = "";

            // on fail
            sendTask.ContinueWith(t => {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(t.Exception.Message);
                });
            }, TaskContinuationOptions.OnlyOnFaulted);

            // on success
            sendTask.ContinueWith(t => {
                Dispatcher.Invoke(() =>
                {

                });
            }, TaskContinuationOptions.NotOnFaulted);

            // on either case
            sendTask.ContinueWith(t => {
                Dispatcher.Invoke(() =>
                {
                    SendButton.IsEnabled = true;
                    ChatTextBox.IsEnabled = true;
                });
            });
        }

        private async Task<bool> send(string text)
        {
            Thread.Sleep(1000);
            messages.Add(new ChatMessage());
            return false;
        }
    }
}
