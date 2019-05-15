using System;
using System.IO;
using System.Text;
using Xunit;
using Assent;
using Assent.Reporters;
using Assent.Reporters.DiffPrograms;
using System.Linq;

namespace Trivia
{
    public partial class TriviaTests
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

        const string ValidPlayerName = "Player";

        [Fact]
        public void PleayerSet_should_have_at_least_two_users()
        {
            Assert.Throws<NotEnoughPlayerException>(() => new BetterPlayerSet(null, null, new BetterPlayer(ValidPlayerName)));
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

            var logger = new Logger();
            // New game console output
            var newGame = new BetterPlayerSet(
                new BetterPlayer("Chet"),
                new BetterPlayer("Pat"),
                new BetterPlayer("Sue"),
                logger: logger);

            var histo = logger.GetLog();
            Assert.Equal(oldGameConsoleStr, histo);
        }

        [Fact]
        public void Should_be_able_to_create_a_PleayerSet_with_two_users()
        {
            new BetterPlayerSet(new BetterPlayer(ValidPlayerName), null, null, null, null, new BetterPlayer(ValidPlayerName));
        }

        [Fact]
        public void Game_Should_throw_when_provided_with_null_PlayerSet()
        {
            Assert.Throws<ArgumentNullException>(() => new BetterGame(null)); 
        }

        [Fact]
        public void Default_Dice_roll_should_return_a_value_from_one_to_six()
        {
            var dice = new DefaultDice();
            foreach(var roll in Enumerable.Range(0, 100).Select(_=>dice.Roll()))
            {
                Assert.InRange(roll, 1, 6);
            }
        }

        [Fact]
        public void Player_should_have_a_name()
        {
            Assert.Throws<NotAValidPlayerNameException>(() => new BetterPlayer(""));
            Assert.Throws<NotAValidPlayerNameException>(() => new BetterPlayer(" "));
            Assert.Throws<NotAValidPlayerNameException>(() => new BetterPlayer(null));
        }

        // Does not even compile when provided with more than 6 players
        // [Fact]
        // public void Game_should_have_at_most_six_users()
        // {
        //     Assert.Throws<NotEnoughPlayerException>(() => 
        //         new BetterGame(
        //             new BetterPlayer(""),
        //             new BetterPlayer(""),
        //             new BetterPlayer(""),
        //             new BetterPlayer(""),
        //             new BetterPlayer(""),
        //             new BetterPlayer(""),
        //             new BetterPlayer("")));
        // }

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
