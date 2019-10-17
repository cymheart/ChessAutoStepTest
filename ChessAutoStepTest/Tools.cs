﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Tools
    {
        private static Tools instance = null;
        public static Tools Instance
        {
            get
            {
                if (instance == null)
                    instance = new Tools();
                return instance;
            }
        }

        public T Create<T>()
        {
            Type type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);
            return obj;
        }

        public T DeepCopyByBinary<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
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

        public Random Rand()
        {
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            return ra;
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
