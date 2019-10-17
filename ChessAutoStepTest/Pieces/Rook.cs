using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    [Serializable]
    public class Rook : Piece
    {
        static int[] _offset = new int[]
     {
            0,1, -1, 0, 1,0, 0,-1,
     };



        public override PieceType Type
        {
            get
            {
                return PieceType.Rook;
            }
        }

        public Rook()
        {
            moveType = PieceMoveType.Line;
            moveOffset = _offset;
            eatOffset = _offset;
        }
    }
}
