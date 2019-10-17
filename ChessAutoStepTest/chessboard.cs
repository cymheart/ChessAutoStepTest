using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    [Serializable]
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

        public void AppendPiece(Piece piece, int x, int y)
        {
            if (x < 0 || x >= XCount || y < 0 || y >= YCount)
                return;

            boardPieces[x, y] = piece;
            LastActionPieceAtBoardIdx.x = x;
            LastActionPieceAtBoardIdx.y = y;
        }

        public void RemovePiece(int x, int y)
        {
            if (x < 0 || x >= XCount || y < 0 || y >= YCount)
                return;

            boardPieces[x, y] = null;
        }

        public Piece GetPiece(BoardIdx boardIdx)
        {
            return boardPieces[boardIdx.x, boardIdx.y];
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

        public ChessColor GetCellColor(int x, int y)
        {
            ChessColor color = ChessColor.White;
            if (x % 2 == 0)
                color = ChessColor.Black;

            if (y % 2 != 0)
            {
                if (color == ChessColor.White)
                    color = ChessColor.Black;
                else
                    color = ChessColor.White;
            }

            return color;
        }
    }
}
