using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Pawn : Piece
    {
        static int[] _moveOffsetForward = new int[]
        {
            0,1
        };

        static int[] _moveOffsetReverse = new int[]
        {
            0, -1
        };

        public override PieceType Type
        {
            get
            {
                return PieceType.Pawn;
            }
        }

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
                        moveOffset = _moveOffsetForward;
                        break;

                    case BoardDirection.Reverse:
                        moveOffset = _moveOffsetReverse;
                        break;
                }
            }
        }

        /// <summary>
        /// 是否首次移动
        /// </summary>
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
            }
            else
            {
                moveLimitCount = 2;
                moveType = PieceMoveType.Line;
            }

            return base.ComputeMovePos(curtRowIdx, curtColIdx, chessBoard);
        }

        public override BoardIdx[] ComputeEatPos(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            if (IsFirstMove)
            {
                moveType = PieceMoveType.Point;
            }
            else
            {
                moveLimitCount = 2;
                moveType = PieceMoveType.Line;
            }

            //判断是否可以吃过路兵
            bool canEatPawn = CanEatGuoLuPawn(curtRowIdx, curtColIdx, chessBoard);



            return base.ComputeEatPos(curtRowIdx, curtColIdx, chessBoard);
        }


        /// <summary>
        /// 判断是否可以吃过路兵
        /// </summary>
        /// <param name="curtRowIdx"></param>
        /// <param name="curtColIdx"></param>
        /// <param name="chessBoard"></param>
        /// <returns></returns>
        bool CanEatGuoLuPawn(int curtRowIdx, int curtColIdx, Chessboard chessBoard)
        {
            Piece piece = chessBoard.GetLastActionPiece();

            if (piece.Type == PieceType.Pawn)
            {

                //1.要吃子的兵需在其第五行
                if (PieceAtBoardDir == BoardDirection.Forward)
                {
                    if (curtColIdx != 4)
                        return false;
                }
                else if(PieceAtBoardDir == BoardDirection.Reverse)
                {
                    if (curtColIdx != 3)
                        return false;
                }

                //2.被吃子的兵需在相邻的列，而且一次就移动二格。
                BoardIdx boardIdx = chessBoard.LastActionPieceAtBoardIdx;
                if(boardIdx.col = )


            }

            return false;
        }

    }
}
