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
        bool IsPiecesRandomPos = true;

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
        }

        int[] RandomPiecesCount()
        {
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            int[] piecesCount = { 0, 1, 1, 2, 2, 2, 8 };
            piecesCount[(int)PieceType.Queen] = ra.Next(0, 1);
            piecesCount[(int)PieceType.Knight] = ra.Next(0, 2);
            piecesCount[(int)PieceType.Rook] = ra.Next(0, 2);
            piecesCount[(int)PieceType.Bishop] = ra.Next(0, 2);
            piecesCount[(int)PieceType.Pawn] = ra.Next(0, 8);
            return piecesCount;
        }


        /// <summary>
        /// 生成标准的玩家棋盘棋子
        /// </summary>
        void CreateStandardPlayersBoardPieces()
        {

        }


        /// <summary>
        /// 生成随机位置的玩家棋盘棋子
        /// </summary>
        void CreateRandomPlayersBoardPieces()
        {
            int[] piecesCount0 = RandomPiecesCount();
            int[] piecesCount1 = RandomPiecesCount();

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
            int[] pawnAtboardXIdxs0 = Utils.Instance.GetRandomNum(null, pawnCount0, 0, 7);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pawnAtboardYIdxs0 = Utils.Instance.GetRandomNum(usedBoardIdxs.ToArray(), pawnCount0, 1, 7);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pawnAtboardXIdxs1 = Utils.Instance.GetRandomNum(usedBoardIdxs.ToArray(), pawnCount1, 0, 7);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pawnAtboardYIdxs1 = Utils.Instance.GetRandomNum(usedBoardIdxs.ToArray(), pawnCount1, 0, 6);
            usedBoardIdxs.AddRange(usedBoardIdxs);
            int[] pieceAtboardIdxs = Utils.Instance.GetRandomNum(usedBoardIdxs.ToArray(), totalPiecesCount * 2, 0, 7);

            //兵不能在出现在最后一行
            for (int i=0; i<pawnCount0; i++)
            {
                Pawn pawn = new Pawn();
                chessBoard.AppendPiece(pawn, pawnAtboardXIdxs0[0], pawnAtboardYIdxs0[1]);
                players[0].AddBoardPieceRef(pawnAtboardXIdxs0[0], pawnAtboardYIdxs0[1]);
            }

            for (int i = 0; i < pawnCount1; i++)
            {
                Pawn pawn = new Pawn();
                chessBoard.AppendPiece(pawn, pawnAtboardXIdxs1[0], pawnAtboardYIdxs1[1]);
                players[1].AddBoardPieceRef(pawnAtboardXIdxs1[0], pawnAtboardYIdxs1[1]);
            }


            //两个象不能在同一种色块格中
            List<int> pieceIdxList = new List<int>(pieceAtboardIdxs);
            if(piecesCount0[(int)PieceType.Bishop] == 2)
                CreateTowBishop(pieceIdxList, players[0]);

            if (piecesCount1[(int)PieceType.Bishop] == 2)
                CreateTowBishop(pieceIdxList, players[1]);


            //其它棋子加入到棋盘
            int count;
            for (PieceType i = PieceType.King; i < PieceType.Count; i++)
            {
                count = piecesCount0[(int)i];
                if (count == 2 && i == PieceType.Bishop)
                    continue;

                for(int j=0; j<count; j++)
                {
                    Pawn pawn = new Pawn();
                    chessBoard.AppendPiece(pawn, pawnAtboardXIdxs0[0], pawnAtboardYIdxs0[1]);
                    players[0].AddBoardPieceRef(pawnAtboardXIdxs0[0], pawnAtboardYIdxs0[1]);
                }
            }
        }


        //两个象不能在同一种色块格中
        void CreateTowBishop(List<int> pieceIdxList, Player player)
        {
            Bishop bishop = new Bishop();
            chessBoard.AppendPiece(bishop, pieceIdxList[0], pieceIdxList[1]);
            player.AddBoardPieceRef(pieceIdxList[0], pieceIdxList[1]);
            BoardCellColor color1 = chessBoard.GetCellColor(pieceIdxList[0], pieceIdxList[1]);
            pieceIdxList.RemoveAt(0);
            pieceIdxList.RemoveAt(1);

            BoardCellColor color2;
            for (int i = 0; i < pieceIdxList.Count; i += 2)
            {
                color2 = chessBoard.GetCellColor(pieceIdxList[i], pieceIdxList[i + 1]);
                if (color2 != color1)
                {
                    bishop = new Bishop();
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
