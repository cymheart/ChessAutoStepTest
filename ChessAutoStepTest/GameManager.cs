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
            int[] piecesCount = RandomPiecesCount();
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));

            for (PieceType i = PieceType.King; i < PieceType.Count; i++)
            {
                for(int j = 0; j < piecesCount[(int)i]; j++)
                {
                    ra.Next(0, 7);
                    ra.Next(0, 7);
                }
            }
        }

        

    }
}
