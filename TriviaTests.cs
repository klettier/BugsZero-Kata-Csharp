using System;
using System.IO;
using System.Text;
using Xunit;
using Assent;
using Assent.Reporters;
using Assent.Reporters.DiffPrograms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Trivia
{
    public class TriviaTests
    {
        [Fact]
        public void RefactoringTests()
        {
            var output = new StringBuilder();
            Console.SetOut(new StringWriter(output));

            Game aGame = new Game();
            Console.WriteLine(aGame.IsPlayable());
            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);
            aGame.Roll(1);

            aGame.WasCorrectlyAnswered();
            aGame.WrongAnswer();

            aGame.Roll(2);

            aGame.Roll(6);

            aGame.WrongAnswer();

            aGame.Roll(2);

            aGame.Roll(2);


            aGame.WrongAnswer();

            aGame.WasCorrectlyAnswered();
            aGame.Roll(1);
            aGame.WasCorrectlyAnswered();

            var configuration = BuildConfiguration();
            this.Assent(output.ToString(), configuration);
        }

        [Fact]
        public void GetOldConsoleOutput()
        {
            //Arrange

            // Old game colnsole output
            var oldGameOutput = new StringBuilder();
            Console.SetOut(new StringWriter(oldGameOutput));
            Game aGame = new Game();
            Console.WriteLine(aGame.IsPlayable());
            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");
            var oldGameConsoleStr = oldGameOutput.ToString();

            // New game console output
            var newGame = new BetterGame(new BetterPlayer("Chet"), new BetterPlayer("Pat"), new BetterPlayer("Sue"));
            var histo = newGame.GetFlatGameHistory();

            Assert.Equal(oldGameConsoleStr, histo);
        }

        class BetterPlayer
        {
            public BetterPlayer(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        class BetterGame
        {
            private List<string> gameHistory;

            readonly List<BetterPlayer> players;
            public BetterGame(BetterPlayer player1, BetterPlayer player2, BetterPlayer player3 = null, BetterPlayer player4 = null, BetterPlayer player5 = null, BetterPlayer player6 = null)
            {
                this.gameHistory = new List<string>();

                this.AddHistoryEntry("False");

                this.players =
                    new List<BetterPlayer>(new[] { player1, player2, player3, player4, player5, player6 })
                        .Where(p => p != null)
                        .ToList();

                if (this.players.Count() < 2)
                    throw new NotEnoughPlayerException("You must specify at least two players !");

                for (var i = 0; i < this.players.Count; i++)
                {
                    var player = players[i];
                    this.AddHistoryEntry(player.Name + " was Added");
                    this.AddHistoryEntry("They are player number " + (i + 1));
                }
            }

            private void AddHistoryEntry(string action) => this.gameHistory.Add(action);
            public string[] GetGameHistory() => this.gameHistory.ToArray();
            public string GetFlatGameHistory() => string.Join("\r\n", GetGameHistory()) + "\r\n";

            public void Roll(int roll)
            {

            }
        }

        class NotEnoughPlayerException : Exception
        {
            public NotEnoughPlayerException()
            {
            }

            public NotEnoughPlayerException(string message) : base(message)
            {
            }

            public NotEnoughPlayerException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected NotEnoughPlayerException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Fact]
        public void Should_Not_Throw_Exception_If_At_Least_Two_Players()
        {
            new BetterGame(new BetterPlayer("Kéviiiin"), new BetterPlayer("Clément"));
            new BetterGame(new BetterPlayer("Kéviiiin"), new BetterPlayer("Clément"), new BetterPlayer("Clément"));
            new BetterGame(new BetterPlayer("Kéviiiin"), new BetterPlayer("Clément"), new BetterPlayer("Clément"), new BetterPlayer("Clément"));
            new BetterGame(new BetterPlayer("Kéviiiin"), new BetterPlayer("Clément"), new BetterPlayer("Clément"), new BetterPlayer("Clément"), new BetterPlayer("Clément"));
            new BetterGame(new BetterPlayer("Kéviiiin"), new BetterPlayer("Clément"), new BetterPlayer("Clément"), new BetterPlayer("Clément"), new BetterPlayer("Clément"), new BetterPlayer("Clément"));
        }

        [Fact]
        public void Should_Throw_Exception_If_Less_Than_Two_Players()
        {
            Assert.Throws<NotEnoughPlayerException>(() => new BetterGame(null, null));
            Assert.Throws<NotEnoughPlayerException>(() => new BetterGame(new BetterPlayer("Player"), null));
            Assert.Throws<NotEnoughPlayerException>(() => new BetterGame(null, new BetterPlayer("Player")));
        }

        private static Configuration BuildConfiguration()
        {
            return
                new Configuration()

                // Uncomment this block if an exception 
                // « Could not find a diff program to use »
                // is thrown and if you have VsCode installed.
                // Otherwise, use other DiffProgram with its full path
                // as parameter.
                // See  https://github.com/droyad/Assent/wiki/Reporting
                //                    .UsingReporter(
                //                        new DiffReporter(
                //                            new []
                //                            {
                // For linux
                //                                new VsCodeDiffProgram(new []
                //                                {
                //                                    "/usr/bin/code"
                //                                })

                // For Windows
                //                                new VsCodeDiffProgram(), 
                //                            }))
                ;
        }
    }
}
