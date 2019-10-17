using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Chessboard
    {
        int rowCount;
        int colCount;

        Piece[,] boardPieces;

        public Chessboard()
        {

        }

        public void Create()
        {
            boardPieces = new Piece[rowCount, colCount];
        }

    }
}
