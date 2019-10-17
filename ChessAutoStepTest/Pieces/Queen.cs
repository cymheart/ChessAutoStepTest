using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Queen : Piece
    {
       static int[] offset = new int[]
      {
            -1, 1,0,1, 1,1,
            -1, 0,1,0,
            -1,-1,0,-1,1,-1
      };

        public override BoardIdx[] ComputeMovePos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int row;
            int col;
            BoardIdx boardIdx = new BoardIdx();

            for (int i = 0; i < offset.Length; i += 2)
            {
                row = curtRowIdx;
                col = curtColIdx;

                while (true)
                {
                    row += offset[i];
                    col += offset[i + 1];

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

        public override BoardIdx[] ComputeEatPos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int row;
            int col;
            BoardIdx boardIdx = new BoardIdx();

            for (int i = 0; i < offset.Length; i += 2)
            {
                row = curtRowIdx;
                col = curtColIdx;

                while (true)
                {
                    row += offset[i];
                    col += offset[i + 1];

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
