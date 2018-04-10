using System;

namespace Tennis
{
    public class Player
    {
        #region Constructor
        public Player(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Player must have a valid name");
            }

            Name = name;
            Score = Scores.Love;
        }
        #endregion

        #region Public properties
        public Scores Score
        {
            get;
            protected set;
        }

        public string Name { get; }
        #endregion

        #region Public methods
        public void IncrementScore() => Score++;

        public void DecrementScore() => Score--;

        public override string ToString() => $"Player '{Name}'";
        #endregion
    }
}
