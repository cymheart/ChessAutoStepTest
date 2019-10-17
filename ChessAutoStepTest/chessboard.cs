using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Chessboard
    {
        public int XCount;
        public int YCount;

        Piece[,] boardPieces;

        public BoardIdx LastActionPieceAtBoardIdx;
        public BoardIdx LastActionPieceAtPrevBoardIdx;

        


        public Chessboard()
        {

        }

        public void Create()
        {
            boardPieces = new Piece[XCount, YCount];

            LastActionPieceAtBoardIdx = new BoardIdx() { x = -1, y = -1 };
            LastActionPieceAtPrevBoardIdx = new BoardIdx() { x = -1, y = -1 };
        }

        public Piece GetLastActionPiece()
        {
            if (LastActionPieceAtBoardIdx.x == -1 || LastActionPieceAtBoardIdx.y == -1)
                return null;

            return boardPieces[LastActionPieceAtBoardIdx.x, LastActionPieceAtBoardIdx.y];
        }

        public bool IsHavPiece(int rowIdx, int colIdx)
        {
            if (boardPieces[rowIdx,colIdx] != null)
                return true;
            return false;
        }

    }
}
