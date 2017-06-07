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

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }

        private int money;
        public int Money
        {
            get { return money; }
            set
            {
                if (money != value)
                {
                    money = value;
                    OnPropertyChanged("Money");
                }
            }
        }

        private int points;
        public int Points
        {
            get { return points; }
            set
            {
                if (points != value)
                {
                    points = value;
                    OnPropertyChanged("Points");
                }
            }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set
            {
                if (userID != value)
                {
                    userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        private int lastBet;
        public int LastBet
        {
            get { return lastBet; }
            set
            {
                if (lastBet != value)
                {
                    lastBet = value;
                    OnPropertyChanged("LastBet");
                }
            }
        }
    }
}