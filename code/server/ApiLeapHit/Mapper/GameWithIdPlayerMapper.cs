using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class GameWithIdPlayerMapper
    {
        public static Game ToGame(this DTOGameWithIdPlayer dtoGame, Player winner, Player loser)
        {
            return new Game
            {
                durationGame = dtoGame.durationGame,
                nbMaxEchanges = dtoGame.nbMaxEchanges,
                winner = winner.playerId,
                loser = loser.playerId
            };
        }
    }
}
