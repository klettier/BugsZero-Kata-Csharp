using System;

namespace Trivia
{
    class BetterGame
    {
        private readonly BetterPlayerSet betterPlayerSet;
        private readonly ILogService logger;

        public BetterGame(BetterPlayerSet betterPlayerSet, 
        ILogService logger = null)
        {
            this.betterPlayerSet = betterPlayerSet ?? throw new ArgumentNullException(nameof(betterPlayerSet));
            this.logger = logger;
        }
    }
}
