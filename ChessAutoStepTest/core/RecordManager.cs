using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    /// <summary>
    /// 这个类主要用来存储走棋的记录
    /// 扩展了下试题
    /// </summary>
    public class RecordManager
    {
        GameManager gameMgr;
        public LinkedList<Record> recordList = new LinkedList<Record>();

        public RecordManager(GameManager gameMgr)
        {
            this.gameMgr = gameMgr;
        }

        /// <summary>
        /// 添加走子记录
        /// </summary>
        public void AppendRecord( 
            int orgPlayerIdx, int dstPlayerIdx,
            BoardIdx orgBoardIdx, BoardIdx dstBoardIdx,
            string tips,
            ChessRecordType type)
        {
            Record record = new Record( 
                gameMgr.chessBoard, orgPlayerIdx, dstPlayerIdx,
                orgBoardIdx, dstBoardIdx, tips, type);

            Push(record);
        }

        /// <summary>
        /// 撤销走子记录
        /// </summary>
        public void CancelRecord()
        {
            Player[] players = gameMgr.players;
            Chessboard chessBoard = gameMgr.chessBoard;

            Record record = Pop();
            record.orgPiece.IsFirstMove = record.orgPieceIsFirstMove;

            chessBoard.AppendPiece(record.orgPiece, record.orgBoardIdx);
            chessBoard.AppendPiece(record.dstPiece, record.dstBoardIdx);
            chessBoard.LastActionPieceAtBoardIdx = record.lastActionPieceAtBoardIdx;
            chessBoard.LastActionPieceAtPrevBoardIdx = record.lastActionPieceAtPrevBoardIdx;

            players[record.orgPlayerIdx].DelBoardPieceRef(record.dstBoardIdx.x, record.dstBoardIdx.y);
            players[record.orgPlayerIdx].AddBoardPieceRef(record.orgBoardIdx.x, record.orgBoardIdx.y);

            if(record.type == ChessRecordType.Eat)
            {
                players[record.dstPlayerIdx].AddBoardPieceRef(record.dstBoardIdx.x, record.dstBoardIdx.y);
            }
        }



        void Push(Record record)
        {
            recordList.AddLast(record);
        }

        Record Pop()
        {
            Record record = recordList.Last.Value;
            recordList.RemoveLast();
            return record;
        }
    }

    public class Record
    {
        public ChessRecordType type;
        public BoardIdx orgBoardIdx;
        public BoardIdx dstBoardIdx;
        public Piece orgPiece;
        public Piece dstPiece;
        public bool orgPieceIsFirstMove;
        public BoardIdx lastActionPieceAtBoardIdx;
        public BoardIdx lastActionPieceAtPrevBoardIdx;
        public int orgPlayerIdx;
        public int dstPlayerIdx;
        public string tips;

        public Record(
            Chessboard chessBoard, int orgPlayerIdx, int dstPlayerIdx,
            BoardIdx orgBoardIdx, BoardIdx dstBoardIdx, string tips, ChessRecordType type)
        {
            this.tips = tips;
            this.type = type;
            this.orgBoardIdx = orgBoardIdx;
            this.dstBoardIdx = dstBoardIdx;
            this.orgPlayerIdx = orgPlayerIdx;
            this.dstPlayerIdx = dstPlayerIdx;

            orgPiece = chessBoard.GetPiece(orgBoardIdx);
            dstPiece = chessBoard.GetPiece(dstBoardIdx);

            orgPieceIsFirstMove = orgPiece.IsFirstMove;

            lastActionPieceAtBoardIdx = chessBoard.LastActionPieceAtBoardIdx;
            lastActionPieceAtPrevBoardIdx = chessBoard.LastActionPieceAtPrevBoardIdx;



        }
    }

}
