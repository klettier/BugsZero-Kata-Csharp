using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Trivia
{
    class BetterPlayerSet
    {
        public IReadOnlyCollection<BetterPlayer> Players { get; private set; }
        public BetterPlayerSet(BetterPlayer player1, BetterPlayer player2, BetterPlayer player3 = null, BetterPlayer player4 = null, BetterPlayer player5 = null, BetterPlayer player6 = null)
        {
            List<BetterPlayer> list = new List<BetterPlayer>(new[] { player1, player2, player3, player4, player5, player6 })
                        .Where(p => p != null)
                        .ToList();

            this.Players = new ReadOnlyCollection<BetterPlayer>(list);

            if (this.Players.Count < 2)
            {
                throw new NotEnoughPlayerException();
            }
        }
    }
}
