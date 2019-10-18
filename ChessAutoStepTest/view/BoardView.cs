//面试试题测试: by蔡业民 开始于 2019/10/17 


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
        Table boardTable;
        Table leftLanTable;
        Table bottomLanTable;
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

            table = new Table(rect, 3, 3);
            TableLine tableLine = new TableLine(LineDir.HORIZONTAL);
            tableLine.lineComputeMode = LineComputeMode.PERCENTAGE;
            tableLine.computeParam = 1 / 17f;
            table.SetLineArea(2, tableLine);
            tableLine.computeParam = 1 / 17f;
            table.SetLineArea(0, tableLine);
            tableLine.computeParam = 15 / 17f;
            table.SetLineArea(1, tableLine);

            tableLine = new TableLine(LineDir.VERTICAL);
            tableLine.lineComputeMode = LineComputeMode.PERCENTAGE;
            tableLine.computeParam = 1 / 17f;
            table.SetLineArea(0, tableLine);
            tableLine.computeParam = 1 / 17f;
            table.SetLineArea(2, tableLine);
            tableLine.computeParam = 15 / 17f;
            table.SetLineArea(1, tableLine);

             boardTable = new Table(xCount, yCount);
             table.AddCellChildTable(1, 1, boardTable);


            //leftLanTable = new Table(8, 1);
            //table.AddCellChildTable(1, 0, boardTable);

            //bottomLanTable = new Table(1, 8);
            //table.AddCellChildTable(2, 1, boardTable);

            table.ReLayout();
        }

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);

            for (int i = 0; i < table.colAmount; i++)
            {
                LinePos line = table.GetColLinePos(i);
                g.DrawLine(p, table.TransToGlobalPoint(line.start),
                   table.TransToGlobalPoint(line.end));
            }

            for (int i = 0; i < table.rowAmount; i++)
            {
                LinePos line = table.GetRowLinePos(i);
                g.DrawLine(p, table.TransToGlobalPoint(line.start),
                   table.TransToGlobalPoint(line.end));
            }



            //Brush brush;
            //for(int i = 1; i< table.rowAmount; i++)
            //{
            //    if (i % 2 == 0)
            //        brush = Brushes.Black;
            //    else
            //        brush = Brushes.White;

            //    for(int j = 0; j<table.colAmount - 1; j++)
            //    {
            //       RectangleF rect = table.GetCellContentGlobalRect(j,i);
            //        g.FillRectangle(brush, rect);
            //    }
            //}




            LinePos[] linePos = table.GetRectLinePos(0, 0, 2, 2);
            for (int i = 0; i < linePos.Length; i++)
            {
                g.DrawLine(p, table.TransToGlobalPoint(linePos[i].start),
                    table.TransToGlobalPoint( linePos[i].end));
            }


            Table table2 = table.GetCellChildTable(1, 1);
            RectangleF rect = table2.GetTableRect();
            rect = table2.TransToGlobalRect(rect);
          //  g.FillRectangle(Brushes.Black, rect);


            Brush brush = Brushes.Black ;
            for (int i = 0; i < table2.rowAmount; i++)
            {
                if (i % 2 == 0)
                    brush = Brushes.White;

                for (int j = 0; j < table2.colAmount; j++)
                {
                    rect = table2.GetCellContentGlobalRect(j, i);
                    Rectangle r = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
                    g.DrawRectangle(Pens.Black, r);
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
