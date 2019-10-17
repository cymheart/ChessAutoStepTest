﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Queen : Piece
    {
       static int[] _offset = new int[]
      {
            -1, 1,0,1, 1,1,
            -1, 0,1,0,
            -1,-1,0,-1,1,-1
      };

        public Queen()
        {
            moveType = PieceMoveType.Line;
            moveOffset = _offset;
            eatOffset = _offset;
        }

    }
}
