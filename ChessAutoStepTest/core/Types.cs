using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{

    /// <summary>
    /// 棋子类型
    /// </summary>
    public enum PieceType : int
    {
        /// <summary>
        /// 未知
        /// </summary>
        None,

        /// <summary>
        /// 王
        /// </summary>
        King,

        /// <summary>
        /// 后
        /// </summary>
        Queen,

        /// <summary>
        /// 马
        /// </summary>
        Knight,

        /// <summary>
        /// 车
        /// </summary>
        Rook,

        /// <summary>
        /// 象
        /// </summary>
        Bishop,

        /// <summary>
        /// 兵
        /// </summary>
        Pawn,

        Count 
    }

    public enum ChessColor:int
    {
        None,
        White,
        Black  
    }


    [Serializable]
    /// <summary>
    /// 棋盘标号
    /// </summary>
    public struct BoardIdx
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// 棋子的移动方式
    /// </summary>
    public enum PieceMoveType
    {
        /// <summary>
        /// 线方式移动
        /// </summary>
        Line,

        /// <summary>
        /// 点方式移动
        /// </summary>
        Point
    }


    public enum ChessRecordType
    {
        /// <summary>
        /// 吃命令
        /// </summary>
        Eat,

        /// <summary>
        /// 移动命令
        /// </summary>
        Move
    }


    public enum BoardDirection
    {
        Forward,
        Reverse
    }
}
