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
            readonly List<BetterPlayer> players;
            public BetterGame(BetterPlayer player1, BetterPlayer player2, BetterPlayer player3 = null, BetterPlayer player4 = null, BetterPlayer player5 = null, BetterPlayer player6 = null)
            {
                this.players =
                    new List<BetterPlayer>(new[] { player1, player2, player3, player4, player5, player6 })
                        .Where(p => p != null)
                        .ToList();
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
        public void AddUser()
        {
            // var output = new StringBuilder();
            // Console.SetOut(new StringWriter(output));

            // Game aGame = new Game();
            // Console.WriteLine(aGame.IsPlayable());
            // aGame.Add("Chet");

            // var configuration = BuildConfiguration();
            // this.Assent(output.ToString(), configuration);

            //

            Assert.Throws<NotEnoughPlayerException>(() => new BetterGame(null, null));
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
