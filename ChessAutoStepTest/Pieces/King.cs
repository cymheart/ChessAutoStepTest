using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    [Serializable]
    public class King :Piece
    {
        static int[] _offset = new int[]    
        {
            -1, 1,0,1, 1,1,
            -1, 0,1,0,
            -1,-1,0,-1,1,-1
        };

        public override PieceType Type
        {
            get
            {
                return PieceType.King;
            }
        }

        public override string Desc
        {
            get
            {
                string color = "黑";
                if (Color == ChessColor.White)
                    color = "白";
                string msg = color + "王";
                return msg;
            }
        }


        public King()
        {
            moveType = PieceMoveType.Point;
            moveOffset = _offset;
            eatOffset = _offset;
        }


    }
}
