//面试试题测试: by蔡业民 开始于 2019/10/17 


using Anim;
using DrawNS;
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
        Chessboard chessBoard;
        PieceView[,] boardPieceViews;
        Size size;
        Font textFont = new Font("微软雅黑", 13);
        Font textFont2 = new Font("微软雅黑", 12);
        Brush bgBrush = new SolidBrush(Color.FromArgb(38, 37, 35));
        Brush[] boardBrush = new Brush[] { new SolidBrush(Color.FromArgb(69, 68, 66)), Brushes.White };
        Table table;
        Table boardTable;
        Table leftLanTable;
        Table rightLanTable;
        Table topLanTable;
        Table bottomLanTable;

        public Action<PieceView> PieceMovedStopEvent = null;
        public Action PlaceEndEvent = null;

        Animation placeAnim;
        int animMS = 0;

        public BoardView()
        {
            boardPieceViews = new PieceView[8, 8];
        }

        public void CreateBoardPieces(Chessboard chessBoard, int placePieceTimeMS = 1000)
        {
            this.chessBoard = chessBoard;

            Piece piece;
            PieceView pieceView;
            for (int i = 0; i < chessBoard.XCount; i++)
            {
                for (int j = 0; j < chessBoard.YCount; j++)
                {
                    piece = chessBoard.GetPiece(i, j);
                    if (piece == null)
                        continue;
                    pieceView = CreatePieceView(piece.Type, piece.Color);
                    pieceView.MovedStopEvent = PieceMovedStoped;

                    boardPieceViews[i, j] = pieceView;
                }
            }

            placeAnim = new Animation(Chess.ChessView, animMS, false);
            placeAnim.DurationMS = placePieceTimeMS;
            placeAnim.AnimationEvent += PlaceEvent;
            placeAnim.Start();
        }

        private void PlaceEvent(Animation animation)
        {
            placeAnim.Stop();

            if (PlaceEndEvent != null)
                PlaceEndEvent();
        }

    
        public void Destory()
        {
            if(placeAnim != null)
                placeAnim.Stop();

            int xCount = boardPieceViews.GetLength(0);
            int yCount = boardPieceViews.GetLength(1);
            PieceView pieceView;

            List<PieceView> movedPieceView = new List<PieceView>();

            for (int i = 0; i < xCount; i++)
            {
                for (int j = 0; j < yCount; j++)
                {
                    pieceView = boardPieceViews[i, j];
                    if (pieceView == null)
                        continue;

                    pieceView.StopMove();
                }
            }
        }


        public void PieceMovedStoped(PieceView pieceView)
        {
            if (PieceMovedStopEvent != null)
                PieceMovedStopEvent(pieceView);
        }

        public void ResetSize(int width, int height)
        {
            RectangleF oldTableRect = new RectangleF();
            RectangleF newTableRect = new RectangleF();
            float scale = 1f;

            if (boardTable != null)
            {
                oldTableRect = boardTable.GetTableRect();
                oldTableRect = boardTable.TransToGlobalRect(oldTableRect);
            }

            CreateTables(width, height);

            if (boardTable != null)
            {
                newTableRect = boardTable.GetTableRect();
                newTableRect = boardTable.TransToGlobalRect(newTableRect);

                scale = newTableRect.Width / oldTableRect.Width;
            }


            int xCount = boardPieceViews.GetLength(0);
            int yCount = boardPieceViews.GetLength(1);
            PieceView pieceView;
            for (int i = 0; i < xCount; i++)
            {
                for (int j = 0; j < yCount; j++)
                {
                    pieceView = boardPieceViews[i, j];
                    if (pieceView == null)
                        continue;

                    if (pieceView.IsMoving == false)
                    {
                        pieceView.rect = GetTableCellRectByBoardIdx(i, j);
                        pieceView.SetBoardIdx(i, j);
                    }
                    else
                    {
                        pieceView.SuspendMove();
                        float x = (pieceView.rect.X - oldTableRect.X) * scale;
                        float y = (pieceView.rect.Y - oldTableRect.Y) * scale;

                        x += newTableRect.X;
                        y += newTableRect.Y;

                        pieceView.rect.X = x;
                        pieceView.rect.Y = y;

                        RectangleF newRect = GetTableCellRectByBoardIdx(i, j);
                        pieceView.rect.Width = newRect.Width;
                        pieceView.rect.Height = newRect.Height;
                        pieceView.ResumeMove();

                    }
                }
            }

        }


        public void CreateTables(int width, int height)
        {
            size.Width = width;
            size.Height = height;

            int w = height - 10;
            if (width < height)
                w = width - 10;

            int xpos = width / 2 - w / 2;
            int ypos = height / 2 - w / 2;

            RectangleF rect = new RectangleF();

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

            leftLanTable = new Table(8, 1);
            table.AddCellChildTable(1, 0, leftLanTable);

            rightLanTable = new Table(8, 1);
            table.AddCellChildTable(1, 2, rightLanTable);

            topLanTable = new Table(1, 8);
            table.AddCellChildTable(0, 1, topLanTable);

            bottomLanTable = new Table(1, 8);
            table.AddCellChildTable(2, 1, bottomLanTable);

            table.ReLayout();

        }

        public RectangleF GetTableCellRectByBoardIdx(BoardIdx boardIdx)
        {
            return GetTableCellRectByBoardIdx(boardIdx.x, boardIdx.y);
        }

        public RectangleF GetTableCellRectByBoardIdx(int x, int y)
        {  
            int row = boardTable.rowAmount - y - 1;
            int col = x;
            return boardTable.GetCellGlobalRect(row, col);
        }

        public void Draw(Graphics g)
        {
            DrawBG(g);

            //左标号
            DrawLeftSideText(g);

            //右标号
            DrawRightSideText(g);

            //上标号
            DrawTopSideText(g);

            //下标号
            DrawBottomSideText(g);

            //棋盘
            DrawBoard(g);

            //棋子
            DrawPieces(g);
        }

        void DrawBG(Graphics g)
        {
            RectangleF rect = table.GetTableRect();
            rect = table.TransToGlobalRect(rect);
            DrawUtils.FillRoundRectangle(g, bgBrush, rect, 0.01f);
        }


        /// <summary>
        /// 棋盘
        /// </summary>
        void DrawBoard(Graphics g)
        {      
            Color color = Color.White;
            RectangleF rect;
            int n = 0;

            for (int i = 0; i < boardTable.rowAmount; i++)
            {
                if (i % 2 == 0)
                    n = 1;
                else
                    n = 0;

                for (int j = 0; j < boardTable.colAmount; j++)
                {
                    rect = boardTable.GetCellGlobalRect(i, j);
                    DrawUtils.FillRoundRectangle(g, boardBrush[n], rect, 0.01f);
                    n = (n+1)%2;
                }
            }
        }

    
        /// <summary>
        /// 左标号
        /// </summary>
        /// <param name="g"></param>
        void DrawLeftSideText(Graphics g)
        {

            RectangleF rect;
            for (int i = 0; i < leftLanTable.rowAmount; i++)
            {
                rect = leftLanTable.GetCellGlobalRect(leftLanTable.rowAmount - i - 1, 0);
                LimitBoxDrawUtils.LimitBoxDraw(g, i.ToString(), textFont, Brushes.White, rect, true, 0);
            }
        }


        /// <summary>
        /// 右标号
        /// </summary>
        /// <param name="g"></param>
        void DrawRightSideText(Graphics g)
        {

            RectangleF rect;
            for (int i = 0; i < rightLanTable.rowAmount; i++)
            {
                rect = rightLanTable.GetCellGlobalRect(rightLanTable.rowAmount - i - 1, 0);
                LimitBoxDrawUtils.LimitBoxDraw(g, i.ToString(), textFont, Brushes.White, rect, true, 0);
            }
        }


        /// <summary>
        /// 上标号
        /// </summary>
        /// <param name="g"></param>
        void DrawTopSideText(Graphics g)
        {

            RectangleF rect;
            for (int i = 0; i < topLanTable.colAmount; i++)
            {
                rect = topLanTable.GetCellGlobalRect(0, i);
                LimitBoxDrawUtils.LimitBoxDraw(g, i.ToString(), textFont2, Brushes.White, rect, true, 0);
            }
        }

        /// <summary>
        /// 下标号
        /// </summary>
        /// <param name="g"></param>
        void DrawBottomSideText(Graphics g)
        {

            RectangleF rect;
            for (int i = 0; i < bottomLanTable.colAmount; i++)
            {
                rect = bottomLanTable.GetCellGlobalRect(0, i);
                LimitBoxDrawUtils.LimitBoxDraw(g, i.ToString(), textFont2, Brushes.White, rect, true, 0);
            }
        }


        void DrawPieces(Graphics g)
        {
            int xCount = boardPieceViews.GetLength(0);
            int yCount = boardPieceViews.GetLength(1);
            PieceView pieceView;

            List<PieceView> movedPieceView = new List<PieceView>();

            for (int i = 0; i < xCount; i++)
            {
                for (int j = 0; j < yCount; j++)
                {
                    pieceView = boardPieceViews[i, j];
                    if (pieceView == null)
                        continue;

                    if (pieceView.IsMoving)
                        movedPieceView.Add(pieceView);
                    else
                        pieceView.Draw(g);
                }
            }

            for(int i=0; i< movedPieceView.Count; i++)
            {
                movedPieceView[i].Draw(g);
            }
        }



        PieceView CreatePieceView(PieceType type, ChessColor color)
        {
            string name;
            if (color == ChessColor.Black)
                name = "Black";
            else
                name = "White";

            switch (type)
            {
                case PieceType.King: name += "King"; break;
                case PieceType.Queen: name += "Queen"; break;
                case PieceType.Knight: name += "Knight"; break;
                case PieceType.Rook: name += "Rook"; break;
                case PieceType.Bishop: name += "Bishop"; break;
                case PieceType.Pawn: name += "Pawn"; break;
            }

            return new PieceView(this, name);
        }

        public void SetPiece(PieceView piece, int x, int y)
        {
            boardPieceViews[x, y] = piece;
        }

        public void Move(BoardIdx orgBoardIdx, BoardIdx dstBoardIdx)
        {
            PieceView orgPieceView = boardPieceViews[orgBoardIdx.x, orgBoardIdx.y];
            if (orgPieceView == null)
                return;

            orgPieceView.StartMove(dstBoardIdx);
        }

    }
}
