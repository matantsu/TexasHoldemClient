using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.BusinessLayer.Models
{
    public class Me : Player
    {
        private IEnumerable<Card> hand;
        public IEnumerable<Card> Hand
        {
            get { return hand; }
            set
            {
                if (hand != value)
                {
                    hand = value;
                    OnPropertyChanged("Hand");
                }
            }
        }

        public Me Patch(Player p)
        {
            this.IsActive = p.IsActive;
            this.Money = p.Money;
            this.PlayerStatus = p.PlayerStatus;
            this.UserID = p.UserID;
            this.Points = p.Points;
            return this;
        }
    }
}
