using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Piece
    {

        public virtual  BoardIdx[] ComputeMovePos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            return null;
        }

        public virtual BoardIdx[] ComputeEatPos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            return null;
        }
    }
}
