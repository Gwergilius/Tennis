using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tennis.UnitTests.Helpers;

namespace Tennis.UnitTests
{
    [TestFixture]
    public class PlayerTestCases
    {
        [TearDown]
        public void TearDown() => TestPlayer.CleanUp();

        [TestCase(null, typeof(ArgumentNullException), TestName = "NameCannotBeNull")]
        [TestCase("", typeof(ArgumentNullException), TestName = "NameCannotBeEmpty")]
        [TestCase("Player One", null, TestName = "PlayerCanBeCreatedWithValidName_1")]
        [TestCase("Agassi", null, TestName = "PlayerCanBeCreatedWithValidName_2")]
        public void PlayerMustHaveAName(string name, Type expectedExceptionType)
        {
            Player player = null;
            TestDelegate createPlayer = () => player = new Player(name);

            if(expectedExceptionType == null)
            {
                Assert.DoesNotThrow(createPlayer, "Cannot create Player");
                Assert.AreEqual(name, player.Name);
            }
            else
            {
                Assert.Throws(expectedExceptionType, createPlayer, "Player must not be created with wrong data");
            }
        }

        [Test]
        public void PlayerGainPoints()
        {
            var player = new Player("A");
            List<string> points = new List<string>();
            string[] expectedPoints = new string[]
            {
                "Love", "Fifteen", "Thirty", "Forty", "Advance"
            };

            for (int i=0; i<expectedPoints.Length; i++)
            {
                points.Add(player.Score.Call);
                player.IncrementScore();
            }

            CollectionAssert.AreEqual(expectedPoints, points);
        }
    }
}
