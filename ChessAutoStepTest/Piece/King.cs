using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class King :Piece
    {

        public override BoardIdx[] ComputeMovePos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int[] offset = new int[]
            {
                -1, 1,0,1, 1,1,
                -1, 0,1,0,
                -1,-1,0,-1,1,-1
            };

            BoardIdx boardIdx = new BoardIdx();
            for (int i = 0; i < offset.Length; i+=2)
            {
                boardIdx.row = curtRowIdx + offset[i];
                boardIdx.col = curtColIdx + offset[i+1];

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

        public override BoardIdx[] ComputeEatPos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int[] offset = new int[]
            {
                -1, 1,0,1, 1,1,
                -1, 0,1,0,
                -1,-1,0,-1,1,-1
            };

            BoardIdx boardIdx = new BoardIdx();
            for (int i = 0; i < offset.Length; i += 2)
            {
                boardIdx.row = curtRowIdx + offset[i];
                boardIdx.col = curtColIdx + offset[i + 1];

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


    }
}
