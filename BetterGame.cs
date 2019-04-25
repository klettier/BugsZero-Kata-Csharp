using System;

namespace Trivia
{
    class BetterGame
    {
        private readonly BetterPlayerSet betterPlayerSet;

        public BetterGame(BetterPlayerSet betterPlayerSet)
        {
            this.betterPlayerSet = betterPlayerSet ?? throw new ArgumentNullException(nameof(betterPlayerSet));
        }
    }

    class DefaultDice : IDice
    {
        static Random random = new Random();
        public int Roll() => random.Next(1, 7);
    }
}
