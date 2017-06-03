using TexasHoldemClient.bl;

namespace TexasHoldemClient.Models
{
    public enum PlayerStatus
    {
        Fold,
        Raise,
        Check
    }

    public class Player : Observable
    {
        PlayerStatus status = PlayerStatus.Check;
        public PlayerStatus Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        string name = "Matan Tsuberi";
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        int money = 346;
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
    }
}
