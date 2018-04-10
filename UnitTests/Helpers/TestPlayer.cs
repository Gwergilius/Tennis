using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.UnitTests.Helpers
{
    /// <summary>
    /// Test player points can be initialized through its constructor
    /// </summary>
    /// <seealso cref="Tennis.Player" />
    public class TestPlayer : Player
    {
        public static Dictionary<string, Player> Players = new Dictionary<string, Player>();

        public TestPlayer(string name, int points=0)
            : base(name)
        {
            Score = Scores.FromPoint(points);
            Players.Add(this.Name, this);
        }

        public static void CleanUp()
        {
            Players.Clear();
        }

        public static Player Get(string name) => Players[name];
    }
}
