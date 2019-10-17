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
    public class CmdList
    {
        public LinkedList<Cmd> cmdList = new LinkedList<Cmd>();

        public void Add(Cmd cmd)
        {
            cmdList.AddLast(cmd);
        }

    }

    public class Cmd
    {
        public ChessCmdType type;
        public BoardIdx orgBoardIdx;
        public BoardIdx dstBoardIdx;

        public Cmd(BoardIdx orgBoardIdx, BoardIdx dstBoardIdx, ChessCmdType type)
        {
            this.type = type;
            this.orgBoardIdx = orgBoardIdx;
            this.dstBoardIdx = dstBoardIdx;
        }
    }

}
