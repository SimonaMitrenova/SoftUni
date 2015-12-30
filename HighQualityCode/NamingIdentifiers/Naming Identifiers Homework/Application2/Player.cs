using System;
using MineGame.Interfaces;

namespace MineGame
{
    public class Player : IPlayer
    {
        private string name;
        private int points;

        public Player(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Invalid player name.");
                }
                this.name = value;
            }
        }

        public int Points
        {
            get
            {
                return this.points;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Points cannot be negative nimber.");
                }
                this.points = value;
            }
        }
    }
}
