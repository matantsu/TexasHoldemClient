
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TexasHoldemClient.bl.Models;

namespace TexasHoldemClient.bl
{
    class G
    {
        public int bet;
    }

    class GameManager
    {
        // singelton
        public readonly static GameManager Instance;
        static GameManager()
        {
            Instance = new GameManager();
        }

        ObservableCollection<Game> Games = new ObservableCollection<Game>();
       
        
        private GameManager()
        {
            init();
        }
        
        private async void init()
        {
            
        }

        public async Task<Game> Create()
        {
            return new Game();
        }

        public async Task Join(Game game)
        {

        }

        public async Task Spectate(Game game)
        {

        }

        public async Task Leave(Game game)
        {

        }
    }
}
