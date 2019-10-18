using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessAutoStepTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameManager gameManager = new GameManager();

            for (int i = 0; i < 5; i++)
            {             
                gameManager.CreateGame();
                gameManager.Play();
                Thread.Sleep(200);
            }


            // Application.Run(new Chess());
        }
    }
}
