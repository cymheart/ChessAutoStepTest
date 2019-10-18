using Anim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessAutoStepTest
{
    public class BoardView
    {
        PieceView[,] boardPieceViews;
        Animation anim;
        Rectangle rect;
        int cellWidth;
        int cellHeight;

        Table table;
        public void CreateBoardView(Control ctrl, Chessboard chessBoard)
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

        public void ResetSize(int width, int height)
        {
            int w = height;
            if (width < height)
                w = width;

            int xpos = width / 2 - w;
            int ypos = height / 2 - w;

            rect.X = xpos;
            rect.Y = ypos;
            rect.Width = w;
            rect.Height = w;

            int xCount = boardPieceViews.GetLength(0);
            int yCount = boardPieceViews.GetLength(1);

            cellWidth = rect.Width / xCount;
            cellHeight = rect.Height / yCount;
        }

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            g.DrawLine(p, 0, 0, 200, 300);


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

        public void Eat(BoardIdx orgBoardIdx, BoardIdx dstBoardIdx)
        {

        }



        public void StartAnim(BoardIdx dstBoardIdx)
        {
            anim.Stop();

         //   linearR = new LinearAnimation(color.R, stopColor.R, scAnim);
         //   linearG = new LinearAnimation(color.G, stopColor.G, scAnim);

            anim.Start();
        }
    }
}
