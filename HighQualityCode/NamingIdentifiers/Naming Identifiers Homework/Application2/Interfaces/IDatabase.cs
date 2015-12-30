using System.Collections.Generic;

namespace MineGame.Interfaces
{
    public interface IDatabase
    {
        IEnumerable<IPlayer> Players { get; }

        void AddPlayer(IPlayer player);

        void InsertPlayer(IPlayer player);

        void SortPlayers();
    }
}
