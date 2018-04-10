using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    public class Scores : IComparable<Scores>, IEquatable<Scores>
    {
        #region Constants
        public static readonly Scores Love = new Scores(0);
        public static readonly Scores Fifteen = new Scores(15);
        public static readonly Scores Thirty = new Scores(30);
        public static readonly Scores Forty = new Scores(40);
        public static readonly Scores Advance = new Scores(100);
        public static readonly Scores Game = new Scores(110);
        #endregion Constants

        #region Private static fields
        private static Dictionary<int, string> _calls = new Dictionary<int, string>
        {
            { Love.Point,  nameof(Love) },
            { Fifteen.Point, nameof(Fifteen) },
            { Thirty.Point, nameof(Thirty) },
            { Forty.Point, nameof(Forty) },
            { Advance.Point, nameof(Advance) },
            { Game.Point, nameof(Game) }
        };

        private static Scores[] _scores = { Love, Fifteen, Thirty, Forty, Advance, Game };
        #endregion

        #region Constructor(s)
        private Scores(int point) => Point = point;
        #endregion

        #region Public properties
        public int Point { get; }

        public string Call => _calls[Point];
        #endregion

        #region Public methods
        public int CompareTo(Scores other) => Point.CompareTo(other.Point);

        public bool Equals(Scores other) => Point.Equals(other.Point);

        public override string ToString() => Call;

        public override bool Equals(object obj) => obj is Scores && Equals((Scores)obj);

        public override int GetHashCode() => Point;
        #endregion

        #region Public static methods and operators
        public static Scores FromPoint(int point) => _scores.First(s => s.Point == point);

        public static bool operator <(Scores one, Scores two) => one.Point < two.Point;

        public static bool operator >(Scores one, Scores two) => two < one;

        public static bool operator ==(Scores one, Scores two) => one.Point == two.Point;

        public static bool operator !=(Scores one, Scores two) => one.Point != two.Point;

        public static Scores operator ++(Scores score) => _scores.First(x => x > score);

        public static Scores operator --(Scores score) => _scores.Last(x => x < score);
        #endregion
    }
}
