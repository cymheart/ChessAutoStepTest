using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class GameManager
    {
        Player[] players;
        Chessboard chessBoard;

        int chessBoardRowCount;
        int chessBoardColCount;
        bool IsRandomPiecesPos = true;
        bool IsRandomPiecesCount = false;

        int firstPlayPlayerIdx = 0;
        ChessColor[] playerPieceColor = { ChessColor.White, ChessColor.Black};
        int playerCount = 2;
        int curtPlayPlayerIdx;

        public GameManager()
        {
            players = new Player[2]
            {
                new Player(),
                new Player()
            };
        }

        public void Create()
        {
            chessBoard.XCount = chessBoardRowCount;
            chessBoard.YCount = chessBoardColCount;
            chessBoard.Create();

            players = new Player[2]
           {
                new Player(),
                new Player()
           };

            curtPlayPlayerIdx = -1;

            CreatePlayersBoardPieces();
        }


        /// <summary>
        /// 
        /// </summary>
        public void CompputeGameResult()
        {
            for (int i = 0; i < 100; i++)
            {
                TurnToNextPlayer();




            }

        }


        void TurnToNextPlayer()
        {
            if (curtPlayPlayerIdx == -1)
                curtPlayPlayerIdx = firstPlayPlayerIdx;
            else
                curtPlayPlayerIdx = (firstPlayPlayerIdx + 1) % playerCount;
        }


        void dd(Player player)
        {
            BoardIdx boardIdx = player.GetRandomPieceBoardIdx();
            Piece piece = chessBoard.GetPiece(boardIdx);

            //优先吃棋子
            BoardIdx[] canEatBoardIdxs = piece.ComputeEatPos(boardIdx.x, boardIdx.y, chessBoard);
            if(canEatBoardIdxs.Length != 0)
            {
                Random ra = Tools.Instance.Rand();
                int eatIdx = ra.Next(0, canEatBoardIdxs.Length - 1);
                BoardIdx eatBoardIdx = canEatBoardIdxs[eatIdx];




            }


            //没有吃的情况下，随机移动棋子
            BoardIdx[] canMoveBoardIdxs = piece.ComputeMovePos(boardIdx.x, boardIdx.y, chessBoard);
        }


        /// <summary>
        /// 生成玩家棋盘棋子
        /// </summary>
        void CreatePlayersBoardPieces()
        {
            if(IsRandomPiecesPos)
                CreateRandomPlayersBoardPieces();
            else
                CreateStandardPlayersBoardPieces();
        }

        /// <summary>
        /// 生成标准的玩家棋盘棋子
        /// </summary>
        void CreateStandardPlayersBoardPieces()
        {
            CreateStandardPlayerBoardPieces(players[0], playerPieceColor[0], BoardDirection.Forward);
            CreateStandardPlayerBoardPieces(players[1], playerPieceColor[1], BoardDirection.Reverse);
        }

        void CreateStandardPlayerBoardPieces(Player player, ChessColor color, BoardDirection dir)
        {
            int pawnY = 1;
            int kingY = 0;

            if (dir == BoardDirection.Reverse)
            {
                kingY = 7;
                pawnY = 6;
            }

            //pawn
            for (int i = 0; i < 8; i++)
            {
                Pawn pawn = new Pawn();
                pawn.PieceAtBoardDir = dir;
                pawn.IsFirstMove = true;
                chessBoard.AppendPiece(pawn, i, pawnY);
                player.AddBoardPieceRef(i, pawnY);
                pawn.Color = color;     
            }

            //Rook
            Piece piece = Tools.Instance.CreatePiece(PieceType.Rook);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 0, kingY);
            player.AddBoardPieceRef(0, kingY);

            piece = Tools.Instance.CreatePiece(PieceType.Rook);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 7, kingY);
            player.AddBoardPieceRef(7, kingY);

            //Knight
            piece = Tools.Instance.CreatePiece(PieceType.Knight);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 1, kingY);
            player.AddBoardPieceRef(1, kingY);

            piece = Tools.Instance.CreatePiece(PieceType.Knight);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 6, kingY);
            player.AddBoardPieceRef(6, kingY);

            //Bishop
            piece = Tools.Instance.CreatePiece(PieceType.Bishop);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 2, kingY);
            player.AddBoardPieceRef(2, kingY);

            piece = Tools.Instance.CreatePiece(PieceType.Bishop);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 5, kingY);
            player.AddBoardPieceRef(5, kingY);

            //King
            piece = Tools.Instance.CreatePiece(PieceType.King);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 3, kingY);
            player.AddBoardPieceRef(3, kingY);

            //Queen
            piece = Tools.Instance.CreatePiece(PieceType.Queen);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 4, kingY);
            player.AddBoardPieceRef(4, kingY);
        }


        /// <summary>
        /// 生成随机位置的玩家棋盘棋子
        /// </summary>
        void CreateRandomPlayersBoardPieces()
        {
            int[] piecesCount0 = CreatePiecesCount();
            int[] piecesCount1 = CreatePiecesCount();

            int pawnCount0 = piecesCount0[(int)PieceType.Pawn];
            int pawnCount1 = piecesCount1[(int)PieceType.Pawn];
            int totalPiecesCount = 0;

            for (int i = 0; i < piecesCount0.Length; i++)
            {
                if(i != (int)PieceType.Pawn)
                    totalPiecesCount += piecesCount0[i];
            }

            for (int i = 0; i < piecesCount1.Length; i++)
            {
                if (i != (int)PieceType.Pawn)
                    totalPiecesCount += piecesCount1[i];
            }
          
            List<int> usedBoardIdxs = new List<int>();
            int[] pawnAtboardXIdxs0 = Tools.Instance.GetRandomNum(null, pawnCount0, 0, 7);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pawnAtboardYIdxs0 = Tools.Instance.GetRandomNum(usedBoardIdxs.ToArray(), pawnCount0, 1, 7);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pawnAtboardXIdxs1 = Tools.Instance.GetRandomNum(usedBoardIdxs.ToArray(), pawnCount1, 0, 7);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pawnAtboardYIdxs1 = Tools.Instance.GetRandomNum(usedBoardIdxs.ToArray(), pawnCount1, 0, 6);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pieceAtboardIdxs = Tools.Instance.GetRandomNum(usedBoardIdxs.ToArray(), totalPiecesCount * 2, 0, 7);

            //兵不能在出现在第一行
            for (int i=0; i<pawnCount0; i++)
            {
                Pawn pawn = new Pawn();
                pawn.Color = playerPieceColor[0];
                chessBoard.AppendPiece(pawn, pawnAtboardXIdxs0[0], pawnAtboardYIdxs0[1]);
                players[0].AddBoardPieceRef(pawnAtboardXIdxs0[0], pawnAtboardYIdxs0[1]);
            }

            for (int i = 0; i < pawnCount1; i++)
            {
                Pawn pawn = new Pawn();
                pawn.Color = playerPieceColor[1];
                chessBoard.AppendPiece(pawn, pawnAtboardXIdxs1[0], pawnAtboardYIdxs1[1]);
                players[1].AddBoardPieceRef(pawnAtboardXIdxs1[0], pawnAtboardYIdxs1[1]);
            }


            //两个象不能在同一种色块格中
            List<int> pieceIdxList = new List<int>(pieceAtboardIdxs);
            if(piecesCount0[(int)PieceType.Bishop] == 2)
                CreateTowBishop(pieceIdxList, players[0], playerPieceColor[0]);

            if (piecesCount1[(int)PieceType.Bishop] == 2)
                CreateTowBishop(pieceIdxList, players[1], playerPieceColor[1]);


            //其它玩家的剩余棋子加入到棋盘
            int count;
            int idx = 0;
            for (PieceType i = PieceType.King; i < PieceType.Count; i++)
            {
                count = piecesCount0[(int)i];
                if (count == 2 && i == PieceType.Bishop)
                    continue;

                for(int j = 0; j < count; j++)
                {
                    Piece piece = Tools.Instance.CreatePiece(i);
                    piece.Color = playerPieceColor[0];
                    chessBoard.AppendPiece(piece, pieceIdxList[idx], pieceIdxList[idx + 1]);
                    players[0].AddBoardPieceRef(pieceIdxList[idx], pieceIdxList[idx + 1]);
                    idx += 2;
                }
            }


            for (PieceType i = PieceType.King; i < PieceType.Count; i++)
            {
                count = piecesCount1[(int)i];
                if (count == 2 && i == PieceType.Bishop)
                    continue;

                for (int j = 0; j < count; j++)
                {
                    Piece piece = Tools.Instance.CreatePiece(i);
                    piece.Color = playerPieceColor[1];
                    chessBoard.AppendPiece(piece, pieceIdxList[idx], pieceIdxList[idx + 1]);
                    players[1].AddBoardPieceRef(pieceIdxList[idx], pieceIdxList[idx + 1]);
                    idx += 2;
                }
            }
        }
        int[] CreatePiecesCount()
        {    
            int[] piecesCount = {0, 1 ,1, 2, 2, 2, 8 };

            if (IsRandomPiecesCount)
            {
                Random ra = Tools.Instance.Rand();
                piecesCount[(int)PieceType.Queen] = ra.Next(0, 1);
                piecesCount[(int)PieceType.Knight] = ra.Next(0, 2);
                piecesCount[(int)PieceType.Rook] = ra.Next(0, 2);
                piecesCount[(int)PieceType.Bishop] = ra.Next(0, 2);
                piecesCount[(int)PieceType.Pawn] = ra.Next(0, 8);
            }

            return piecesCount;
        }

        //两个象不能在同一种色块格中
        void CreateTowBishop(List<int> pieceIdxList, Player player, ChessColor chessColor)
        {
            Bishop bishop = new Bishop();
            bishop.Color = chessColor;
            chessBoard.AppendPiece(bishop, pieceIdxList[0], pieceIdxList[1]);
            player.AddBoardPieceRef(pieceIdxList[0], pieceIdxList[1]);
            ChessColor color1 = chessBoard.GetCellColor(pieceIdxList[0], pieceIdxList[1]);
            pieceIdxList.RemoveAt(0);
            pieceIdxList.RemoveAt(1);

            ChessColor color2;
            for (int i = 0; i < pieceIdxList.Count; i += 2)
            {
                color2 = chessBoard.GetCellColor(pieceIdxList[i], pieceIdxList[i + 1]);
                if (color2 != color1)
                {
                    bishop = new Bishop();
                    bishop.Color = chessColor;
                    chessBoard.AppendPiece(bishop, pieceIdxList[i], pieceIdxList[i + 1]);
                    player.AddBoardPieceRef(pieceIdxList[i], pieceIdxList[i+1]);
                    pieceIdxList.RemoveAt(i);
                    pieceIdxList.RemoveAt(i + 1);
                    break;
                }
            }
        }

    }
}
