using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Chessboard
    {
        public int RowCount;
        public int ColCount;

        Piece[,] boardPieces;

        public Chessboard()
        {

        }

        public void Create()
        {
            boardPieces = new Piece[RowCount, ColCount];
        }


        public bool IsHavPiece(int rowIdx, int colIdx)
        {
            if (boardPieces[rowIdx,colIdx] != null)
                return true;
            return false;
        }

    }
}
