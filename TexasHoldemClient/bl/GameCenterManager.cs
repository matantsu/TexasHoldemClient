using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.Models;

namespace TexasHoldemClient.bl
{
    public class GameCenterManager : Observable
    {
        private ObservableCollection<Game> activeGames = new ObservableCollection<Game>();
        public ObservableCollection<Game> ActiveGames
        {
            get { return activeGames; }
            set
            {
                if(activeGames != value)
                {
                    activeGames = value;
                    OnPropertyChanged("ActiveGames");
                }
            }
        }

        private GameCenterManager()
        {
            init();
        }
        public static GameCenterManager instance = new GameCenterManager();

        private async void init()
        {

            await ServerAPI.serverApi.createNewGame(GamePolicy.Limit, 5, 5, 5, 5, 5, true);
            await ServerAPI.serverApi.createNewGame(GamePolicy.Limit, 4, 5, 5, 5, 5, true);
            await ServerAPI.serverApi.createNewGame(GamePolicy.Limit, 5, 12, 5, 5, 5, true);
            await ServerAPI.serverApi.createNewGame(GamePolicy.Limit, 5, 5, 34, 5, 5, true);
            await ServerAPI.serverApi.createNewGame(GamePolicy.Limit, 5, 4578, 5, 2, 5, true);
            await ServerAPI.serverApi.createNewGame(GamePolicy.Limit, 5, 5, 5, 5, 123, true);
            await ServerAPI.serverApi.createNewGame(GamePolicy.Limit, 5, 5, 2362, 5, 5, true);

            ICollection<Game> games = await ServerAPI.serverApi.getActiveGames();
            ActiveGames = new ObservableCollection<Game>(games);
        }
    }
}
