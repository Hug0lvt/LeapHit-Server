using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class GameMapper
    {
        public static DTOGame ToDto(this Game game)
        {
            DTOGame dtoGame = new DTOGame()
            {
                gameId = game.gameId,
                durationGame = game.durationGame,
                nbMaxEchanges = game.nbMaxEchanges,
                playerWinner = game.winner,
                playerLoser = game.loser,
                scoreLoser = game.scoreLoser,
                scoreWinner = game.scoreWinner
            };
            return dtoGame;
        }

        public static Game ToGame(this DTOGame dtoGame)
        {
            return new Game
            {
                durationGame = dtoGame.durationGame,
                nbMaxEchanges = dtoGame.nbMaxEchanges,
                winner = dtoGame.playerWinner,
                loser = dtoGame.playerLoser,
                scoreLoser = dtoGame.scoreLoser,
                scoreWinner = dtoGame.scoreWinner
            };
        }
    }
}
