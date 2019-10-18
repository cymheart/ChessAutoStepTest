//面试试题测试: by蔡业民 开始于 2019/10/17 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    [Serializable]
    public class Piece
    {
        /// <summary>
        /// 移动偏移方向
        /// </summary>
        protected int[] moveOffset;

        /// <summary>
        /// 吃偏移方向
        /// </summary>
        protected int[] eatOffset;

        protected PieceMoveType moveType = PieceMoveType.Point;
        protected int moveLimitCount = -1;

        public ChessColor Color = ChessColor.None;

        /// <summary>
        /// 是否首次移动
        /// </summary>
        public bool IsFirstMove = true;

        public virtual PieceType Type
        {
            get
            {
                return PieceType.None;
            }
        }

        public virtual string Desc
        {
            get
            {
                string color = "黑";
                if (Color == ChessColor.White)
                    color = "白";
                string msg = color + "棋";
                return msg;
            }
        }

        public virtual BoardIdx[] ComputeMovePos(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {
            switch (moveType)
            {
                case PieceMoveType.Point:
                    return ComputeMovePosForPointType(curtBoardX, curtBoardY, chessBoard);
                case PieceMoveType.Line:
                    return ComputeMovePosForLineType(curtBoardX, curtBoardY, chessBoard);
            }

            return null;
        }
        public virtual BoardIdx[] ComputeEatPos(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {
            switch (moveType)
            {
                case PieceMoveType.Point:
                    return ComputeEatPosForPointType(curtBoardX, curtBoardY, chessBoard);
                case PieceMoveType.Line:
                    return ComputeEatPosForLineType(curtBoardX, curtBoardY, chessBoard);
            }

            return null;
        }


        BoardIdx[] ComputeMovePosForPointType(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            BoardIdx boardIdx = new BoardIdx();
            for (int i = 0; i < moveOffset.Length; i += 2)
            {
                boardIdx.x = curtBoardX + moveOffset[i];
                boardIdx.y = curtBoardY + moveOffset[i + 1];

                if (boardIdx.x < 0 || boardIdx.y < 0 ||
                   boardIdx.x >= chessBoard.XCount ||
                   boardIdx.y >= chessBoard.YCount)
                    continue;

                if (chessBoard.IsHavPiece(boardIdx.x, boardIdx.y))
                    continue;

                boardIdxList.Add(boardIdx);
            }

            return boardIdxList.ToArray();
        }

        BoardIdx[] ComputeMovePosForLineType(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int count = 0;
            int x;
            int y;
            BoardIdx boardIdx = new BoardIdx();

            for (int i = 0; i < moveOffset.Length; i += 2)
            {
                count = 0;
                x = curtBoardX;
                y = curtBoardY;

                while (true)
                {
                    count++;
                    x += moveOffset[i];
                    y += moveOffset[i + 1];

                    if(moveLimitCount != -1 && 
                        count > moveLimitCount)
                    {
                        break;
                    }

                    if (x < 0 || y < 0 ||              
                        x >= chessBoard.XCount ||             
                        y >= chessBoard.YCount)
                        break;

                    if (chessBoard.IsHavPiece(x, y))
                        break;

                    boardIdx.x = x;
                    boardIdx.y = y;
                    boardIdxList.Add(boardIdx);
                   
                }
            }

            return boardIdxList.ToArray();
        }

        BoardIdx[] ComputeEatPosForPointType(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            BoardIdx boardIdx = new BoardIdx();
            for (int i = 0; i < eatOffset.Length; i += 2)
            {
                boardIdx.x = curtBoardX + eatOffset[i];
                boardIdx.y = curtBoardY + eatOffset[i + 1];

                if (boardIdx.x < 0 || boardIdx.y < 0 ||
                   boardIdx.x >= chessBoard.XCount ||
                   boardIdx.y >= chessBoard.YCount)
                    continue;

                if (chessBoard.IsHavPiece(boardIdx.x, boardIdx.y))
                {
                    Piece piece = chessBoard.GetPiece(boardIdx);
                    if(piece.Color != Color)
                        boardIdxList.Add(boardIdx);
                }
            }

            return boardIdxList.ToArray();
        }

        BoardIdx[] ComputeEatPosForLineType(int curtBoardX, int curtBoardY, Chessboard chessBoard)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();

            int count = 0;
            int x;
            int y;
            BoardIdx boardIdx = new BoardIdx();

            for (int i = 0; i < eatOffset.Length; i += 2)
            {
                count = 0;
                x = curtBoardX;
                y = curtBoardY;

                while (true)
                {
                    count++;
                    x += eatOffset[i];
                    y += eatOffset[i + 1];

                    if (moveLimitCount != -1 &&
                      count > moveLimitCount)
                    {
                        break;
                    }

                    if (x < 0 || y < 0 ||
                    x >= chessBoard.XCount ||
                    y >= chessBoard.YCount)
                        break;

                    if (chessBoard.IsHavPiece(x, y))
                    {
                        boardIdx.x = x;
                        boardIdx.y = y;

                        Piece piece = chessBoard.GetPiece(boardIdx);
                        if (piece.Color != Color)
                            boardIdxList.Add(boardIdx);

                        break;
                    }
                }
            }

            return boardIdxList.ToArray();
        }

    }
}
