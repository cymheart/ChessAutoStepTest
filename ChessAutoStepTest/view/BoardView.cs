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
        Table table;
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
                  //  pieceView = CreatePieceView(piece.Type);
                   // boardPieceViews[i, j] = pieceView;
                }
            }
        }

        public void ResetSize(int width, int height)
        {
            int w = height - 10;
            if (width < height)
                w = width - 10;

            int xpos = width / 2 - w/2;
            int ypos = height / 2 - w/2;

            rect.X = xpos;
            rect.Y = ypos;
            rect.Width = w;
            rect.Height = w;

            int xCount = boardPieceViews.GetLength(0);
            int yCount = boardPieceViews.GetLength(1);

            table = new Table(rect, xCount + 1, yCount + 1);

            TableLine tableLine = new TableLine(LineDir.HORIZONTAL);
            tableLine.lineComputeMode = LineComputeMode.ABSOLUTE;
            tableLine.computeParam = 30;
            table.SetLineArea(xCount, tableLine);

            tableLine = new TableLine(LineDir.VERTICAL);
            tableLine.lineComputeMode = LineComputeMode.ABSOLUTE;
            tableLine.computeParam = 30;
            table.SetLineArea(0, tableLine);

            table.ReLayout();
        }

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);

            for (int i = 0; i < table.colAmount - 1; i++)
            {
                LinePos line = table.GetColLinePos(i);
                g.DrawLine(p, table.TransToGlobalPoint(line.start),
                   table.TransToGlobalPoint(line.end));
            }


            LinePos[] linePos = table.GetRectLinePos(0, 0, 8, 8);
            for (int i = 0; i < linePos.Length; i++)
            {
                g.DrawLine(p, table.TransToGlobalPoint(linePos[i].start),
                    table.TransToGlobalPoint( linePos[i].end));
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
