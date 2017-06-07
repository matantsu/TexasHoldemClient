namespace TexasHoldemClient.BusinessLayer.Models
{
    public enum PlayerStatus
    {
        Check,
        Raise,
        Fold
    }

    public class Player : Changing
    {
        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        private PlayerStatus playerStatus;
        public PlayerStatus PlayerStatus
        {
            get { return playerStatus; }
            set
            {
                if (playerStatus != value)
                {
                    playerStatus = value;
                    OnPropertyChanged("PlayerStatus");
                }
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        private int bet;
        public int Bet
        {
            get { return bet; }
            set
            {
                if (bet != value)
                {
                    bet = value;
                    OnPropertyChanged("Bet");
                }
            }
        }
    }
}