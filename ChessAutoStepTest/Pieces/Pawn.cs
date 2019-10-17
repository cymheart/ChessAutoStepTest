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

        static int[] _eatOffsetForward = new int[]
      {
            -1,1, 1,1
      };

        static int[] _eatOffsetReverse = new int[]
        {
            -1,-1, 1,-1
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
                        eatOffset = _eatOffsetForward;
                        break;

                    case BoardDirection.Reverse:
                        moveOffset = _moveOffsetReverse;
                        eatOffset = _eatOffsetReverse;
                        break;
                }
            }
        }

        /// <summary>
        /// 是否首次移动
        /// </summary>
        public bool IsFirstMove = true;

        /// <summary>
        /// 是否可以吃过路兵
        /// </summary>
        public bool EatGuoLuPawn = true;


        public Pawn()
        {
            moveType = PieceMoveType.Point;
        }

        public override BoardIdx[] ComputeMovePos(int curtBoardX, int curtBoardY, Chessboard chessBoard)
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

            return base.ComputeMovePos(curtBoardX, curtBoardY, chessBoard);
        }

        public override BoardIdx[] ComputeEatPos(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {
            moveType = PieceMoveType.Point;
            BoardIdx[] eatBoardIdx = base.ComputeEatPos(curtBoardX, curtBoardY, chessBoard);

            //判断是否可以吃过路兵
            if (EatGuoLuPawn)
            {
                bool canEatPawn = CanEatGuoLuPawn(curtBoardX, curtBoardY, chessBoard);
                if (canEatPawn)
                {
                    BoardIdx boardIdx = chessBoard.LastActionPieceAtBoardIdx;
                    List<BoardIdx> boardIdxList = new List<BoardIdx>();
                    boardIdxList.AddRange(eatBoardIdx);
                    boardIdxList.Add(boardIdx);
                    eatBoardIdx = boardIdxList.ToArray();
                }
            }

            return eatBoardIdx;
        }


        /// <summary>
        /// 判断是否可以吃过路兵
        /// </summary>
        /// <param name="curtRowIdx"></param>
        /// <param name="curtColIdx"></param>
        /// <param name="chessBoard"></param>
        /// <returns></returns>
        bool CanEatGuoLuPawn(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {

            Piece piece = chessBoard.GetLastActionPiece();
            if (piece == null)
                return false;

            //1.吃过路兵是选择性的，若要进行，就要在对方走棋后的下一步马上进行，否则就失去机会
            if (piece.Type != PieceType.Pawn)
                return false;

            //2.要吃子的兵需在其第五行
            if (PieceAtBoardDir == BoardDirection.Forward)
            {
                if (curtBoardY != 4)
                    return false;
            }
            else if (PieceAtBoardDir == BoardDirection.Reverse)
            {
                if (curtBoardY != 3)
                    return false;
            }

            //3.被吃子的兵需在相邻的列，而且一次就移动二格。
            BoardIdx boardIdx = chessBoard.LastActionPieceAtBoardIdx;
            BoardIdx prevBoardIdx = chessBoard.LastActionPieceAtPrevBoardIdx;

            if (prevBoardIdx.y + 2 != boardIdx.y)  //没有一次移动两格
                return false;

            //不在相邻位置上
            if (!((boardIdx.x != curtBoardX - 1 && boardIdx.y != curtBoardY) ||                  
                (boardIdx.x != curtBoardX + 1 && boardIdx.y != curtBoardY)))
                    return false;

            return true;
        }

    }
}
