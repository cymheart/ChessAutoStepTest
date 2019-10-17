using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Piece
    {
        protected int[] moveOffset;
        protected int[] eatOffset;
        protected PieceMoveType moveType = PieceMoveType.Point;
        protected int moveLimitCount = -1;

        public virtual BoardIdx[] ComputeMovePos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            switch (moveType)
            {
                case PieceMoveType.Point:
                    return ComputeMovePosForPointType(curtRowIdx, curtColIdx, chessBoard);
                case PieceMoveType.Line:
                    return ComputeMovePosForLineType(curtRowIdx, curtColIdx, chessBoard);
            }

            return null;
        }
        public virtual BoardIdx[] ComputeEatPos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            switch (moveType)
            {
                case PieceMoveType.Point:
                    return ComputeEatPosForPointType(curtRowIdx, curtColIdx, chessBoard);
                case PieceMoveType.Line:
                    return ComputeEatPosForLineType(curtRowIdx, curtColIdx, chessBoard);
            }

            return null;
        }


        BoardIdx[] ComputeMovePosForPointType(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            BoardIdx boardIdx = new BoardIdx();
            for (int i = 0; i < moveOffset.Length; i += 2)
            {
                boardIdx.row = curtRowIdx + moveOffset[i];
                boardIdx.col = curtColIdx + moveOffset[i + 1];

                if (boardIdx.row < 0 || boardIdx.col < 0 ||
                   boardIdx.row >= chessBoard.RowCount ||
                   boardIdx.col >= chessBoard.ColCount)
                    continue;

                if (chessBoard.IsHavPiece(boardIdx.row, boardIdx.col))
                    continue;

                boardIdxList.Add(boardIdx);
            }

            return boardIdxList.ToArray();
        }

        BoardIdx[] ComputeMovePosForLineType(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int count = 0;
            int row;
            int col;
            BoardIdx boardIdx = new BoardIdx();

            for (int i = 0; i < moveOffset.Length; i += 2)
            {
                count = 0;
                row = curtRowIdx;
                col = curtColIdx;

                while (true)
                {
                    count++;
                    row += moveOffset[i];
                    col += moveOffset[i + 1];

                    if(moveLimitCount != -1 && 
                        count > moveLimitCount)
                    {
                        break;
                    }

                    if (row < 0 || col < 0 ||              
                        row >= chessBoard.RowCount ||             
                        col >= chessBoard.ColCount)
                        break;

                    if (chessBoard.IsHavPiece(row, col))
                        break;

                    boardIdx.row = row;
                    boardIdx.col = col;
                    boardIdxList.Add(boardIdx);
                   
                }
            }

            return boardIdxList.ToArray();
        }

        BoardIdx[] ComputeEatPosForPointType(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            BoardIdx boardIdx = new BoardIdx();
            for (int i = 0; i < eatOffset.Length; i += 2)
            {
                boardIdx.row = curtRowIdx + eatOffset[i];
                boardIdx.col = curtColIdx + eatOffset[i + 1];

                if (boardIdx.row < 0 || boardIdx.col < 0 ||
                   boardIdx.row >= chessBoard.RowCount ||
                   boardIdx.col >= chessBoard.ColCount)
                    continue;

                if (chessBoard.IsHavPiece(boardIdx.row, boardIdx.col))
                    boardIdxList.Add(boardIdx);
            }

            return boardIdxList.ToArray();
        }

        BoardIdx[] ComputeEatPosForLineType(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int count = 0;
            int row;
            int col;
            BoardIdx boardIdx = new BoardIdx();

            for (int i = 0; i < eatOffset.Length; i += 2)
            {
                count = 0;
                row = curtRowIdx;
                col = curtColIdx;

                while (true)
                {
                    count++;
                    row += eatOffset[i];
                    col += eatOffset[i + 1];

                    if (moveLimitCount != -1 &&
                      count > moveLimitCount)
                    {
                        break;
                    }

                    if (row < 0 || col < 0 ||
                    row >= chessBoard.RowCount ||
                    col >= chessBoard.ColCount)
                        break;

                    if (chessBoard.IsHavPiece(row, col))
                    {
                        boardIdx.row = row;
                        boardIdx.col = col;
                        boardIdxList.Add(boardIdx);
                        break;
                    }
                }
            }

            return boardIdxList.ToArray();
        }

    }
}
