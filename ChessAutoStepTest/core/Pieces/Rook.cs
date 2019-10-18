//面试试题测试: by蔡业民 开始于 2019/10/17 


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

        public override string Desc
        {
            get
            {
                string color = "黑";
                if (Color == ChessColor.White)
                    color = "白";
                string msg = color + "车";
                return msg;
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
