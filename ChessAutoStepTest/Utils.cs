using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Utils
    {
        private static Utils instance = null;
        public static Utils Instance
        {
            get
            {
                if (instance == null)
                    instance = new Utils();
                return instance;
            }
        }

        public T Create<T>()
        {
            Type type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);
            return obj;
        }

        public Piece CreatePiece(PieceType type)
        {
            switch(type)
            {
                case PieceType.King: return new King();
                case PieceType.Queen: return new Queen();
                case PieceType.Knight: return new Knight();
                case PieceType.Rook: return new Rook();
                case PieceType.Bishop: return new Bishop();
                case PieceType.Pawn: return new Pawn();
            }

            return null;
        }

            public int[] GetRandomNum(int[] existArrNum, int num, int minValue, int maxValue)
        {

            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));

            int[] arrNum = new int[num];

            int tmp = 0;

            for (int i = 0; i <= num - 1; i++)
            {
                tmp = ra.Next(minValue, maxValue); //随机取数
                arrNum[i] = GetNum(existArrNum, arrNum, tmp, minValue, maxValue, ra); //取出值赋到数组中
            }

            return arrNum;

        }

        public int GetNum(int[] existArrNum, int[] arrNum, int tmp, int minValue, int maxValue, Random ra)
        {
            int n = 0;
            while (n <= arrNum.Length - 1)
            {
                if (arrNum[n] == tmp) //利用循环判断是否有重复
                {
                    tmp = ra.Next(minValue, maxValue);
                    GetNum(existArrNum, arrNum, tmp, minValue, maxValue, ra);
                }
                n++;
            }


            if (existArrNum != null)
            {
                n = 0;
                while (n <= existArrNum.Length - 1)
                {
                    if (existArrNum[n] == tmp)
                    {
                        tmp = ra.Next(minValue, maxValue);
                        GetNum(existArrNum, arrNum, tmp, minValue, maxValue, ra);
                    }
                    n++;
                }
            }

            return tmp;
        }

    }
}
