using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class GameManager
    {
        Player[] players;
        Chessboard chessBoard;

        int chessBoardRowCount;
        int chessBoardColCount;

        public GameManager()
        {
            players = new Player[2]
            {
                new Player(),
                new Player()
            };
        }

        public void Create()
        {
            chessBoard.XCount = chessBoardRowCount;
            chessBoard.YCount = chessBoardColCount;
            chessBoard.Create();
        }

    }
}
