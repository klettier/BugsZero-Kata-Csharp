using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Trivia
{
    class BetterPlayerSet
    {
        public IReadOnlyCollection<BetterPlayer> Players { get; private set; }
        public BetterPlayerSet(
            BetterPlayer player1, BetterPlayer player2, BetterPlayer player3 = null, BetterPlayer player4 = null, BetterPlayer player5 = null, BetterPlayer player6 = null,
            ILogService logger = null)
        {

            logger?.Log("False");
            List<BetterPlayer> players = new List<BetterPlayer>(new[] { player1, player2, player3, player4, player5, player6 })
                        .Where(p => p != null)
                        .ToList();

            this.Players = new ReadOnlyCollection<BetterPlayer>(players);

            if (this.Players.Count < 2)
            {
                throw new NotEnoughPlayerException();
            }

            for (var i = 0; i < players.Count; i++)
            {
                var player = players[i];
                logger?.Log(player.Name + " was Added");
                logger?.Log("They are player number " + (i + 1));
            }
        }

        // public BetterGame(BetterPlayer player1, BetterPlayer player2, BetterPlayer player3 = null, BetterPlayer player4 = null, BetterPlayer player5 = null, BetterPlayer player6 = null)
        // {
        //     this.gameHistory = new List<string>();

        //     //TODO:Temp log
        //     this.AddHistoryEntry("False");

        //     this.players =
        //         new List<BetterPlayer>(new[] { player1, player2, player3, player4, player5, player6 })
        //             .Where(p => p != null)
        //             .ToList();

        //     if (this.players.Count() < 2)
        //         throw new NotEnoughPlayerException("You must specify at least two players !");

        //     for (var i = 0; i < this.players.Count; i++)
        //     {
        //         var player = players[i];
        //         this.AddHistoryEntry(player.Name + " was Added");
        //         this.AddHistoryEntry("They are player number " + (i + 1));
        //     }
        // }
    }
}
