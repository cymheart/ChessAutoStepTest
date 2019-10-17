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


        void DelBoardPieceRef(int boardRowIdx, int boardColIdx)
        {
            int key = GetPieceKeyForBoardIdx(boardRowIdx, boardColIdx);
            chessBoardPiecePos.Remove(key);
        }

        void AddBoardPieceRef(int boardRowIdx, int boardColIdx)
        {
            int key = GetPieceKeyForBoardIdx(boardRowIdx, boardColIdx);
            chessBoardPiecePos.AddLast(key);
        }


        int GetPieceKeyForBoardIdx(int boardRowIdx, int boardColIdx)
        {
            return (boardRowIdx << 12) | boardColIdx;
        }

        public BoardIdx GetPieceKeyBoardIdx(int pieceKey)
        {
            BoardIdx boardIdx = new BoardIdx();
            boardIdx.row = pieceKey >> 12;
            boardIdx.col = pieceKey & 0xfff;
            return boardIdx;
        }
    }
}
