using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    [Serializable]
    public class Player
    {
        LinkedList<int> chessBoardPiecePos = new LinkedList<int>();
        Chessboard chessBoard;

        public Player(Chessboard board)
        {
            chessBoard = board;
        }

        public void DelBoardPieceRef(int boardX, int boardY)
        {
            int key = GetPieceKeyForBoardIdx(boardX, boardY);

            if(chessBoardPiecePos.Contains(key))
                chessBoardPiecePos.Remove(key);
        }

        public void AddBoardPieceRef(int boardX, int boardY)
        {
            int key = GetPieceKeyForBoardIdx(boardX, boardY);
            chessBoardPiecePos.AddLast(key);
        }


        int GetPieceKeyForBoardIdx(int boardX, int boardY)
        {
            return (boardX << 12) | boardY;
        }

        BoardIdx GetPieceKeyBoardIdx(int pieceKey)
        {
            BoardIdx boardIdx = new BoardIdx();
            boardIdx.x = pieceKey >> 12;
            boardIdx.y = pieceKey & 0xfff;
            return boardIdx;
        }

        public BoardIdx[] GetAllPieceBoardIdx()
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();
            LinkedListNode<int> node = chessBoardPiecePos.First;
            for (;node!=null ; node = node.Next )
            {
                BoardIdx boardIdx = GetPieceKeyBoardIdx(node.Value);
                boardIdxList.Add(boardIdx);
            }

            return boardIdxList.ToArray();
        }

        public BoardIdx? GetCanEatBoardIdx(BoardIdx beEatBoardIdx)
        {
            BoardIdx[] boardIdxes = GetCanEatBoardIdxs();
            if (boardIdxes == null)
                return null;

            for(int i=0; i<boardIdxes.Length; i++)
            {
                Piece piece = chessBoard.GetPiece(boardIdxes[i]);
                BoardIdx[] canBeEatBoardIdxs = piece.ComputeEatPos(boardIdxes[i].x, boardIdxes[i].y, chessBoard);

                for (int j=0; j< canBeEatBoardIdxs.Length; j++)
                {
                    if(canBeEatBoardIdxs[j].x == beEatBoardIdx.x &&
                       canBeEatBoardIdxs[j].y == beEatBoardIdx.y)
                    {
                        return boardIdxes[i];
                    }
                }

            }

            return null;
        }


        public BoardIdx[] GetCanEatBoardIdxs()
        {
            BoardIdx[] boardIdxs = GetAllPieceBoardIdx();

            List<BoardIdx> boardIdxList = new List<BoardIdx>();
            for (int i = 0; i < boardIdxs.Length; i++)
            {
                Piece piece = chessBoard.GetPiece(boardIdxs[i]);
                BoardIdx[] canBeEatBoardIdxs = piece.ComputeEatPos(boardIdxs[i].x, boardIdxs[i].y, chessBoard);
                if (canBeEatBoardIdxs.Length == 0)
                    continue;

                boardIdxList.Add(boardIdxs[i]);
            }

            if (boardIdxList.Count == 0)
                return null;

            return boardIdxList.ToArray();
        }

        public BoardIdx? GetRandomCanEatBoardIdx()
        {
            BoardIdx[] boardIdxes = GetCanEatBoardIdxs();

            if (boardIdxes == null)
                return null;
       
            Random ra = Tools.Instance.Rand();
            int eatIdx = ra.Next(0, boardIdxes.Length - 1);

            return boardIdxes[eatIdx];
        }

        public BoardIdx? GetRandomCanMoveBoardIdx()
        {
            BoardIdx[] boardIdxs = GetAllPieceBoardIdx();

            List<BoardIdx> boardIdxList = new List<BoardIdx>();
            for (int i = 0; i < boardIdxs.Length; i++)
            {
                Piece piece = chessBoard.GetPiece(boardIdxs[i]);
                BoardIdx[] canMoveBoardIdxs = piece.ComputeMovePos(boardIdxs[i].x, boardIdxs[i].y, chessBoard);
                if (canMoveBoardIdxs.Length == 0)
                    continue;

                boardIdxList.Add(boardIdxs[i]);
            }

            if (boardIdxList.Count == 0)
                return null;

            Random ra = Tools.Instance.Rand();
            int moveIdx = ra.Next(0, boardIdxList.Count - 1);

            return boardIdxList[moveIdx];
        }

        public void EatOrMoveBoardPiece(BoardIdx playerBoardIdx, BoardIdx dstBoardIdx)
        {
            chessBoard.MovePiece(playerBoardIdx, dstBoardIdx);
            DelBoardPieceRef(playerBoardIdx.x, playerBoardIdx.y);
            AddBoardPieceRef(dstBoardIdx.x, dstBoardIdx.y);
        }


        public BoardIdx[] GetPieceBoardIdxsByType(PieceType type)
        {
            List<BoardIdx> boardIdxList = new List<BoardIdx>();
            LinkedListNode<int> node = chessBoardPiecePos.First;
            for (; node != null; node = node.Next)
            {
                BoardIdx boardIdx = GetPieceKeyBoardIdx(node.Value);
                Piece piece = chessBoard.GetPiece(boardIdx);

                if(piece.Type == type)
                {
                    boardIdxList.Add(boardIdx);
                }
            }

            if (boardIdxList.Count == 0)
                return null;

            return boardIdxList.ToArray();
        }

    }
}
