using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    public class Game
    {
        #region Constructor
        public Game(Player p1, Player p2)
        {
            if (p1 == null || p2 == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            if (p1.Equals(p2))
            {
                throw new ArgumentException("Players must be different");
            }

            Players = new Player[] { p1, p2 };
        }
        #endregion

        #region Public properties
        public IReadOnlyList<Player> Players { get; }

        public Player Winner
        {
            get
            {
                if (Players.All(p => p.Score < Scores.Advance))
                    return null;
                if (Players[0].Score < Scores.Forty)
                    return Players[1];
                if (Players[1].Score < Scores.Forty)
                    return Players[0];
                return Players.FirstOrDefault(p => p.Score == Scores.Game);
            }
        }
        #endregion

        #region Public methods
        public void WinBall(Player w)
        {
            int winner = w == Players[0] ? 0 : 1;
            if (Players[1 - winner].Score < Scores.Advance)
            {
                Players[winner].IncrementScore();
            }
            else
            {
                Players[1 - winner].DecrementScore();   // Advance lost
            }
        }
        #endregion
    }
}
