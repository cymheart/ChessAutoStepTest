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
        CmdList cmdList;
        public Chess()
        {
            InitializeComponent();

            GameManager gameManager = new GameManager();
            gameManager.CreateGame();
            gameManager.Play();

            chessboard = gameManager.orgChessBoard;
            cmdList = gameManager.cmdList;

            AddRecordToListBox();
        }

        void AddRecordToListBox()
        {
            Cmd cmd;
            LinkedListNode<Cmd> node = cmdList.cmdList.First;
            for (; node != null; node = node.Next)
            {
                cmd = node.Value;
                Piece orgPiece = chessboard.GetPiece(cmd.orgBoardIdx);
                Piece dstPiece = chessboard.GetPiece(cmd.dstBoardIdx);
                chessboard.MovePiece(cmd.orgBoardIdx, cmd.dstBoardIdx);

                string orgIdxMsg = "(" + cmd.orgBoardIdx.x + "," + cmd.orgBoardIdx.y + ")";
                string dstIdxMsg = "(" + cmd.dstBoardIdx.x + "," + cmd.dstBoardIdx.y + ")";

                switch (cmd.type)
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
