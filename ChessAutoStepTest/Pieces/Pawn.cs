using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Pawn : Piece
    {
        static int[] _moveOffsetForward1 = new int[]
        {
            0,1
        };

        static int[] _moveOffsetForward2 = new int[]
        {
            0,1, 0,2
        };

        static int[] _moveOffsetReverse1 = new int[]
        {
            0, -1
        };

        static int[] _moveOffsetReverse2 = new int[]
        {
            0, -1, 0, -2
        };

        int[][] moveOffsetGroup = new int[2][];

        BoardDirection pieceAtBoardDir;
        public BoardDirection PieceAtBoardDir
        {
            get { return pieceAtBoardDir; }
            set
            {
                pieceAtBoardDir = value;

                switch(pieceAtBoardDir)
                {
                    case BoardDirection.Forward:
                        moveOffsetGroup[0] = _moveOffsetForward1;
                        moveOffsetGroup[1] = _moveOffsetForward2;
                        break;

                    case BoardDirection.Reverse:
                        moveOffsetGroup[0] = _moveOffsetReverse1;
                        moveOffsetGroup[1] = _moveOffsetReverse2;
                        break;
                }
            }
        }

        public bool IsFirstMove = true;


        public Pawn()
        {
            moveType = PieceMoveType.Point;
        }

        public override BoardIdx[] ComputeMovePos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            if(IsFirstMove)
            {
                moveType = PieceMoveType.Point;
                moveOffset = moveOffsetGroup[1];
            }
            else
            {
                moveType = PieceMoveType.Line;
                moveOffset = moveOffsetGroup[0];
            }

            return base.ComputeMovePos(curtRowIdx, curtColIdx, chessBoard);
        }

    }
}
