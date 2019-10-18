using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessAutoStepTest
{
    public partial class Chess : Form
    {
        Chessboard chessboard;
        RecordManager recordMgr;
        public Chess()
        {
            InitializeComponent();

            GameManager gameManager = new GameManager();
            gameManager.CreateGame();
            gameManager.Play();

            chessboard = gameManager.orgChessBoard;
            recordMgr = gameManager.recordMgr;

            AddRecordToListBox();
        }

        void AddRecordToListBox()
        {
            Record record;
            LinkedListNode<Record> node = recordMgr.recordList.First;
            for (; node != null; node = node.Next)
            {
                record = node.Value;
                Piece orgPiece = chessboard.GetPiece(record.orgBoardIdx);
                Piece dstPiece = chessboard.GetPiece(record.dstBoardIdx);
                chessboard.MovePiece(record.orgBoardIdx, record.dstBoardIdx);

                string orgIdxMsg = "(" + record.orgBoardIdx.x + "," + record.orgBoardIdx.y + ")";
                string dstIdxMsg = "(" + record.dstBoardIdx.x + "," + record.dstBoardIdx.y + ")";

                switch (record.type)
                {
                    case ChessCmdType.Eat:
                        {
                            listBoxRecord.Items.Add(orgPiece.Desc + orgIdxMsg + "吃" + dstPiece.Desc + dstIdxMsg);
                        }
                        break;

                    case ChessCmdType.Move:
                        {
                            listBoxRecord.Items.Add(orgPiece.Desc + orgIdxMsg + "走到" + dstIdxMsg);
                        }
                        break;
                }
            }
        }
    }
}
