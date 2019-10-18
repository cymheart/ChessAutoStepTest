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
    public class RecordList
    {
        public LinkedList<Record> recordList = new LinkedList<Record>();

        public void Push(Record record)
        {
            recordList.AddLast(record);
        }

        public Record Pop()
        {
            Record record = recordList.Last.Value;
            recordList.RemoveLast();
            return record;
        }
    }

    public class Record
    {
        public ChessCmdType type;
        public BoardIdx orgBoardIdx;
        public BoardIdx dstBoardIdx;
        public Piece orgPiece;
        public Piece dstPiece;
        public bool orgPieceIsFirstMove;

        public Record(
            BoardIdx orgBoardIdx, BoardIdx dstBoardIdx,
            Piece orgPiece,Piece dstPiece,ChessCmdType type)
        {
            this.type = type;
            this.orgBoardIdx = orgBoardIdx;
            this.dstBoardIdx = dstBoardIdx;
            this.orgPiece = orgPiece;
            this.dstPiece = dstPiece;
            orgPieceIsFirstMove = orgPiece.IsFirstMove;
        }
    }

}
