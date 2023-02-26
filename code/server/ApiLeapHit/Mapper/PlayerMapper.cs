using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class PlayerMapper
    {
        public static DTOPlayer ToDto(this Player player)
        {
            DTOPlayer dtoPlayer = new DTOPlayer()
            {
                playerId = player.playerId,
                name = player.name,
                nbBallTouchTotal = player.nbBallTouchTotal,
                timePlayed = player.timePlayed
            };
            return dtoPlayer;
        }

        public static Player ToPlayer(this DTOPlayer dtoPlayer)
        {
            return new Player
            {
                playerId = dtoPlayer.playerId,
                name = dtoPlayer.name,
                nbBallTouchTotal = dtoPlayer.nbBallTouchTotal,
                timePlayed = dtoPlayer.timePlayed
            };
        }
    }
}
