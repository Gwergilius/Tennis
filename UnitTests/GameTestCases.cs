using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tennis.UnitTests.Helpers;

namespace Tennis.UnitTests
{
    [TestFixture]
    public class GameTestCases
    {
        [TearDown]
        public void TearDown() => TestPlayer.CleanUp();

        // Wikipedia tells: Tennis is a racket sport that can be played individually against a single opponent
        // i.e a Game must have exactly two different player
        [TestCaseSource(nameof(GameCreationTestCases))]
        public void GameCouldBePlayed(Player player0, Player player1, Exception expectedException)
        {
            var players = new[] { player0, player1 };

            Game game = null;
            TestDelegate createGame = () => game = new Game(players[0], players[1]);

            if (expectedException == null)
            {
                Assert.DoesNotThrow(createGame, "Game cannot be created");
                Assert.IsNotNull(game.Players);
                CollectionAssert.AreEquivalent(players, game.Players);
            }
            else
            {
                Exception thrown = Assert.Throws(expectedException.GetType(), createGame);
                Assert.AreEqual(expectedException.Message, thrown.Message);
            }
        }

        public static IEnumerable<TestCaseData> GameCreationTestCases()
        {
            Player player0 = new Player("A");
            Player player1 = new Player("B");

            yield return new TestCaseData(
                player0, player1,
                null)
                .SetName("PlayedByTwoDifferentPlayers");

            yield return new TestCaseData(
                null, null, new ArgumentException("Player cannot be null")
                )
                .SetName("CannotPlayedWithoutPlayers");

            yield return new TestCaseData(
                player0, null, new ArgumentException("Player cannot be null")
                )
                .SetName("CannotPlayedWithOnePlayer_1");

            yield return new TestCaseData(
                null, player1, new ArgumentException("Player cannot be null")
                )
                .SetName("CannotPlayedWithOnePlayer_2");

            yield return new TestCaseData(
                player0, player0, new ArgumentException("Players must be different")
                )
                .SetName("CanPlayedWithDifferentPlayers");
        }

        [TestCase(0, 0)]
        [TestCase(15, 0)]
        [TestCase(30, 0)]
        [TestCase(40, 0)]
        [TestCase(0, 15)]
        [TestCase(15, 15)]
        [TestCase(30, 15)]
        [TestCase(40, 15)]
        [TestCase(0, 30)]
        [TestCase(15, 30)]
        [TestCase(30, 30)]
        [TestCase(40, 30)]
        [TestCase(0, 40)]
        [TestCase(15, 40)]
        [TestCase(30, 40)]
        [TestCase(40, 40)]
        public void NonGameIsNotWinner(int point0, int point1)
        {
            Player player0 = new TestPlayer("A", point0);
            Player player1 = new TestPlayer("B", point1);
            Game game = new Game(player0, player1);

            Assert.IsNull(game.Winner);
        }

        [TestCase(100, 0)]
        [TestCase(100, 15)]
        [TestCase(100, 30)]
        [TestCase(0,  100)]
        [TestCase(15, 100)]
        [TestCase(30, 100)]
        public void GameIsWinnerAgainstNon40(int point0, int point1)
        {
            Player player0 = new TestPlayer("A", point0);
            Player player1 = new TestPlayer("B", point1);
            Game game = new Game(player0, player1);

            Player winner = player0.Score == Scores.Advance ? player0 : player1;
            Assert.AreSame(winner, game.Winner);
        }

        [TestCase(40, 100)]
        [TestCase(100, 40)]
        public void AdvanceIsNeeded(int point0, int point1)
        {
            Player player0 = new TestPlayer("A", point0);
            Player player1 = new TestPlayer("B", point1);
            Game game = new Game(player0, player1);

            Assert.IsNull(game.Winner, "Must win by advance");
        }

        [TestCase( 0, 0, "A",  15, 0)]
        [TestCase(15, 0, "A",  30, 0)]
        [TestCase(30, 0, "A",  40, 0)]
        [TestCase(40, 0, "A", 100, 0)]
        [TestCase(0,  0, "B", 0,  15)]
        [TestCase(0, 15, "B", 0,  30)]
        [TestCase(0, 30, "B", 0,  40)]
        [TestCase(0, 40, "B", 0, 100)]
        public void WinningABallWouldIncrementScore(int point0, int point1, string winner, int expected0, int expected1)
        {
            Player player0 = new TestPlayer("A", point0);
            Player player1 = new TestPlayer("B", point1);
            Game game = new Game(player0, player1);
            Player w = TestPlayer.Get(winner);

            game.WinBall(w);

            Assert.AreEqual(Scores.FromPoint(expected0), player0.Score);
            Assert.AreEqual(Scores.FromPoint(expected1), player1.Score);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void DeuceWontWinWithABall(string winner)
        {
            Player player0 = new TestPlayer("A", 40);
            Player player1 = new TestPlayer("B", 40);
            Game game = new Game(player0, player1);
            Player w = TestPlayer.Get(winner);

            game.WinBall(w);
            Assert.AreEqual(Scores.FromPoint(100), w.Score);
            Assert.IsNull(game.Winner);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void DeuceWinWithadvance(string winner)
        {
            Player player0 = new TestPlayer("A", 40);
            Player player1 = new TestPlayer("B", 40);
            Game game = new Game(player0, player1);
            Player w = TestPlayer.Get(winner);

            game.WinBall(w);
            game.WinBall(w);

            Assert.AreEqual(Scores.FromPoint(110), w.Score, $"{w} must got the advance");
            Assert.AreSame(w, game.Winner, $"{w} must win the game");
        }

        [Test]
        public void AdvanceLostFromDeuce()
        {
            Player player0 = new TestPlayer("A", 40);
            Player player1 = new TestPlayer("B", 40);
            Game game = new Game(player0, player1);

            game.WinBall(player0);
            game.WinBall(player1);

            Assert.AreEqual(Scores.Forty, player0.Score, $"{player0} must lost the advance");
            Assert.AreEqual(Scores.Forty, player1.Score, $"{player1} must not got the advance");
            Assert.IsNull(game.Winner, $"The game is still in deuce state");
        }
    }
}
