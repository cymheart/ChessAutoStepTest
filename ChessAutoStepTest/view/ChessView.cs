using Anim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    class ChessView
    {
        PieceView[,] boardPieceViews;

        public void CreateBoardView(Chessboard chessBoard)
        {
            boardPieceViews = new PieceView[chessBoard.XCount, chessBoard.YCount];

            Piece piece;
            PieceView pieceView;
            for (int i = 0; i < chessBoard.XCount; i++)
            {
                for (int j = 0; j < chessBoard.YCount; j++)
                {
                    piece = chessBoard.GetPiece(i, j);
                    pieceView = CreatePieceView(piece.Type);
                    boardPieceViews[i, j] = pieceView;
                }
            }
        }

        PieceView CreatePieceView(PieceType type)
        {
            switch (type)
            {
                case PieceType.King: return new PieceView("King");
                case PieceType.Queen: return new PieceView("Queen");
                case PieceType.Knight: return new PieceView("Knight");
                case PieceType.Rook: return new PieceView("Rook");
                case PieceType.Bishop: return new PieceView("Bishop");
                case PieceType.Pawn: return new PieceView("Pawn");
            }
            return null;
        }

        public void ChessEat(BoardIdx orgBoardIdx, BoardIdx dstBoardIdx)
        {

        }



        public void StartAnim(Color stopColor)
        {
            //scAnim.Stop();

            //linearR = new ScLinearAnimation(color.R, stopColor.R, scAnim);
            //linearG = new ScLinearAnimation(color.G, stopColor.G, scAnim);
            //linearB = new ScLinearAnimation(color.B, stopColor.B, scAnim);
            //scAnim.Start();
        }
    }
}
