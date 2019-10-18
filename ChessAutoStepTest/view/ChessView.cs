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
            Piece piece;
            for (int i = 0; i < chessBoard.XCount; i++)
            {
                for (int j = 0; j < chessBoard.YCount; j++)
                {
                    piece = chessBoard.GetPiece(i, j);
                }
            }


        }

        public void ChessEat(BoardIdx orgBoardIdx, BoardIdx dstBoardIdx)
        {

        }



        public void StartAnim(Color stopColor)
        {
            scAnim.Stop();

            linearR = new ScLinearAnimation(color.R, stopColor.R, scAnim);
            linearG = new ScLinearAnimation(color.G, stopColor.G, scAnim);
            linearB = new ScLinearAnimation(color.B, stopColor.B, scAnim);
            scAnim.Start();
        }
    }
}
