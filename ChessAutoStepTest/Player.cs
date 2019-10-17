using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Player
    {
        public LinkedList<int> chessBoardPiecePos = new LinkedList<int>();

        public void DelBoardPieceRef(int boardX, int boardY)
        {
            int key = GetPieceKeyForBoardIdx(boardX, boardY);
            chessBoardPiecePos.Remove(key);
        }

        public void AddBoardPieceRef(int boardX, int boardY)
        {
            int key = GetPieceKeyForBoardIdx(boardX, boardY);
            chessBoardPiecePos.AddLast(key);
        }


        int GetPieceKeyForBoardIdx(int boardX, int boardY)
        {
            return (boardX << 12) | boardY;
        }

        BoardIdx GetPieceKeyBoardIdx(int pieceKey)
        {
            BoardIdx boardIdx = new BoardIdx();
            boardIdx.x = pieceKey >> 12;
            boardIdx.y = pieceKey & 0xfff;
            return boardIdx;
        }

        public BoardIdx GetRandomPieceBoardIdx()
        {
            Random ra = Tools.Instance.Rand();
            int pieceKey = ra.Next(0, chessBoardPiecePos.Count - 1);
            BoardIdx idx = GetPieceKeyBoardIdx(pieceKey);
            return idx;
        }
    }
}
