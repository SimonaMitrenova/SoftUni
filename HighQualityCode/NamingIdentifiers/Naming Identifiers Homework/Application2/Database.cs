using System.Collections.Generic;
using System.Linq;
using MineGame.Interfaces;


namespace MineGame
{
    public class Database : IDatabase
    {
        private const int PlayersMaxCount = 6;
        private readonly List<IPlayer> players;
 
        public Database()
        {
            players = new List<IPlayer>(PlayersMaxCount);
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                return this.players;
            }
        }

        public void AddPlayer(IPlayer player)
        {
            players.Add(player);
        }

        public void InsertPlayer(IPlayer player)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Points < player.Points)
                {
                    players.Insert(i, player);
                    players.RemoveAt(players.Count - 1);
                }
            }
        }

        public void SortPlayers()
        {
            players.OrderBy(player => player.Name)
                .ThenBy(player => player.Points);
        }
    }
}
