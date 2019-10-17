using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    [Serializable]
    public class Knight : Piece
    {
        static int[] _offset = new int[]
      {
            -1,2, 1,2,
            2, 1, 2,-1,
            1,-2, -1,-2,
            -2,-1, -2,1
      };

        public override PieceType Type
        {
            get
            {
                return PieceType.Knight;
            }
        }

        public Knight()
        {
            moveType = PieceMoveType.Point;
            moveOffset = _offset;
            eatOffset = _offset;
        }
    }
}
