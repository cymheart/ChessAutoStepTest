using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Pawn : Piece
    {
        static int[] _offset = new int[]
        {
            -1, 1,0,1, 1,1,
            -1, 0,1,0,
            -1,-1,0,-1,1,-1
        };

        public Pawn()
        {
            moveType = PieceMoveType.Point;
            offset = _offset;
        }

    }
}
