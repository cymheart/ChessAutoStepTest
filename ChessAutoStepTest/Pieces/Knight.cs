using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Knight : Piece
    {
        static int[] _offset = new int[]
      {
            -1,2, 1,2,
            2, 1, 2,-1,
            1,-2, -1,-2,
            -2,-1, -2,1
      };

        public Knight()
        {
            moveType = PieceMoveType.Point;
            moveOffset = _offset;
            eatOffset = _offset;
        }
    }
}
