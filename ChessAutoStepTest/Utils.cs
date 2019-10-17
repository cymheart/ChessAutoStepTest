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

        public int[] GetRandomNum(int num, int minValue, int maxValue)
        {

            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));

            int[] arrNum = new int[num];

            int tmp = 0;

            for (int i = 0; i <= num - 1; i++)
            {
                tmp = ra.Next(minValue, maxValue); //随机取数
                arrNum[i] = GetNum(arrNum, tmp, minValue, maxValue, ra); //取出值赋到数组中
            }

            return arrNum;

        }

        public int GetNum(int[] arrNum, int tmp, int minValue, int maxValue, Random ra)
        {
            int n = 0;
            while (n <= arrNum.Length - 1)
            {
                if (arrNum[n] == tmp) //利用循环判断是否有重复
                {
                    tmp = ra.Next(minValue, maxValue);
                    GetNum(arrNum, tmp, minValue, maxValue, ra);
                }
                n++;
            }

            return tmp;
        }

    }
}
