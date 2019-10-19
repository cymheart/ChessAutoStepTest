//面试试题测试: by蔡业民 开始于 2019/10/17 


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessAutoStepTest
{
    public partial class Chess : Form
    {
        GameManager gameManager;
        Chessboard chessboard;
        RecordManager recordMgr;
        BoardView boardView;
        public static Control ChessView;
        public Chess()
        {
            InitializeComponent();
            ChessView = chessView ;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲


            gameManager = new GameManager();
            gameManager.CreateGame();
            gameManager.Play();

            chessboard = gameManager.orgChessBoard;
            recordMgr = gameManager.recordMgr;

            boardView = new BoardView();
            boardView.CreateBoardView(chessboard);
            boardView.ResetSize(chessView.Width, chessView.Height);

            BoardIdx orgBoardIdx = new BoardIdx(3, 1);
            BoardIdx dstBoardIdx = new BoardIdx(2, 7);

            boardView.Eat(orgBoardIdx, dstBoardIdx);

            AddRecordToListBox();
        }


        void AddRecordToListBox()
        {
            listBoxRecord.Items.Clear();
            int i = 0;
            Record record;
            LinkedListNode<Record> node = recordMgr.recordList.First;
            for (; node != null; node = node.Next)
            {
                i++;
                record = node.Value;
                Piece orgPiece = chessboard.GetPiece(record.orgBoardIdx);
                Piece dstPiece = chessboard.GetPiece(record.dstBoardIdx);
                chessboard.MovePiece(record.orgBoardIdx, record.dstBoardIdx);

                string orgIdxMsg = "(" + record.orgBoardIdx.x + "," + record.orgBoardIdx.y + ")";
                string dstIdxMsg = "(" + record.dstBoardIdx.x + "," + record.dstBoardIdx.y + ")";

                switch (record.type)
                {
                    case ChessRecordType.Eat:
                        {
                            listBoxRecord.Items.Add( i + ". " + orgPiece.Desc + orgIdxMsg + "吃" + dstPiece.Desc + dstIdxMsg + "   " + record.tips );
                        }
                        break;

                    case ChessRecordType.Move:
                        {
                            listBoxRecord.Items.Add(i + ". " + orgPiece.Desc + orgIdxMsg + "走到" + dstIdxMsg + "   " + record.tips);
                        }
                        break;
                }
            }

            string resultMsg = "平局";

            if (gameManager.winPlayerIdx >= 0) {
                ChessColor winColor = gameManager.playerPieceColor[gameManager.winPlayerIdx];
                if (winColor == ChessColor.White)
                    resultMsg = "白子胜";
                else
                    resultMsg = "黑子胜";
            }

            listBoxRecord.Items.Add(resultMsg);
        }

        private void chessView_SizeChanged(object sender, EventArgs e)
        {
            if (boardView == null)
                return;

            boardView.ResetSize(chessView.Width, chessView.Height);
            chessView.Refresh();
        }

        private void chessView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            boardView.Draw(g);
        }

       
    }
}
