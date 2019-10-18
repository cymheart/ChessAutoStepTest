//面试试题测试: by蔡业民 开始于 2019/10/17 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    [Serializable]
    public class Bishop : Piece
    {
        static int[] _offset = new int[]
        {
            -1, 1, 1,1,
            -1,-1,1,-1
         };


        public override PieceType Type
        {
            get
            {
                return PieceType.Bishop;
            }
        }

        public override string Desc
        {
            get
            {
                string color = "黑";
                if (Color == ChessColor.White)
                    color = "白";
                string msg = color + "象";
                return msg;
            }
        }

        public Bishop()
        {
            moveType = PieceMoveType.Line;
            moveOffset = _offset;
            eatOffset = _offset;
        }

    }
}
