using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Player
    {
        public LinkedList<int> chessBoardPiecePos = new LinkedList<int>();


        void Remove(int rowIdx, int colIdx)
        {
            int key = GetPieceKey(rowIdx, colIdx);

        }

        int GetPieceKey(int rowIdx, int colIdx)
        {
            return (rowIdx << 12) | colIdx;
        }
    }
}
