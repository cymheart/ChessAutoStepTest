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
using System.Threading;
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
        bool isPause = false;
        int recordIdx = 0;

        LinkedListNode<Record> recordNode;
        public Chess()
        {
            InitializeComponent();
            ChessView = board ;
            gameManager = new GameManager();

            boardView = new BoardView();
            boardView.PieceMovedStopEvent = PieceMovedStoped;
            boardView.ResetSize(board.Width, board.Height);
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        void StartGame()
        {
            boardView.Destory();
            recordIdx = 0;
            isPause = false;
            recordNode = null;

            gameManager.CreateGame();
            gameManager.Play();

            chessboard = gameManager.orgChessBoard;
            recordMgr = gameManager.recordMgr;

            boardView = new BoardView();
            boardView.PlaceEndEvent = PlaceEnd;
            boardView.PieceMovedStopEvent = PieceMovedStoped;
            boardView.CreateBoardPieces(chessboard);
            boardView.ResetSize(board.Width, board.Height);
            ChessView.Refresh();

            AddRecordToListBox();

           
        }

        void Play()
        {
            if(recordNode == null)
            {
                recordNode = recordMgr.recordList.First;
            }

            Record record = recordNode.Value;
            boardView.Move(record.orgBoardIdx, record.dstBoardIdx);

            listBoxRecord.SetSelected(recordIdx, true);
            recordIdx++;
        }

        void PlaceEnd()
        {
            Play();
        }

        void PieceMovedStoped(PieceView pieceView)
        {
            recordNode = recordNode.Next;
            if (recordNode == null)
                return;

            if (isPause)
                return;
         
            Record record = recordNode.Value;
            boardView.Move(record.orgBoardIdx, record.dstBoardIdx);
            listBoxRecord.SetSelected(recordIdx, true);
            recordIdx++;
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


        private void board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
           // g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (boardView == null)
                return;

            boardView.Draw(g);
        }

        private void board_SizeChanged(object sender, EventArgs e)
        {
            if (boardView == null)
                return;

            boardView.ResetSize(board.Width, board.Height);
            board.Refresh();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (isPause)
            {
                isPause = false;
                Play();
                btnPause.Text = "暂停游戏";
            }
            else
            {
                isPause = true;
                btnPause.Text = "继续游戏";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            gameManager.IsRandomPiecesPos = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            gameManager.IsRandomPiecesCount = checkBox2.Checked;
        }

    }
}
