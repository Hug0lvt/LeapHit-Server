using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class GameMapper
    {
        public static DTOGame ToDto(this Game game, Player winner, Player loser)
        {
            DTOGame dtoGame = new DTOGame()
            {
                gameId = game.gameId,
                durationGame = game.durationGame,
                nbMaxEchanges = game.nbMaxEchanges,
                playerWinner = winner.ToDto(),
                playerLoser = loser.ToDto()
            };
            return dtoGame;
        }

        public static Game ToGame(this DTOGame dtoGame, Player winner, Player loser)
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
