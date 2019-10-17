using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class PlayManager
    {
        Player[] players;
        Chessboard chessBoard;

        int chessBoardRowCount;
        int chessBoardColCount;

        public PlayManager()
        {
            players = new Player[2]
            {
                new Player(),
                new Player()
            };
        }

        public void Create()
        {
            chessBoard.RowCount = chessBoardRowCount;
            chessBoard.ColCount = chessBoardColCount;
            chessBoard.Create();
        }

    }
}
