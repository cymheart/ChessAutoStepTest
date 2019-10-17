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

        void CreatePlayerPieces()
        {
            int[] piecesCount0 = RandomPiecesCount();
            int[] piecesCount1 = RandomPiecesCount();

            int totalPiecesCount = 0;

            for(int i=0; i< piecesCount0.Length; i++)
                totalPiecesCount += piecesCount0[i];

            for (int i = 0; i < piecesCount1.Length; i++)
                totalPiecesCount += piecesCount1[i];

            int[] pieceAtboardIdxs = Utils.Instance.GetRandomNum(totalPiecesCount * 2, 0, 7);


            //两个象不能在同一种色块格中
            List<int> pieceIdxList = new List<int>(pieceAtboardIdxs);
            if(piecesCount0[(int)PieceType.Bishop] == 2)
                CreateTowBishop(pieceIdxList, players[0]);

            if (piecesCount1[(int)PieceType.Bishop] == 2)
                CreateTowBishop(pieceIdxList, players[1]);


            //
            for (PieceType i = PieceType.King; i < PieceType.Count; i++)
            {
               
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
