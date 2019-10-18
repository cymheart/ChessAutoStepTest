//面试试题测试: by蔡业民 开始于 2019/10/17 


using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Resource
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        static Dictionary<string, Image> imageDict = new Dictionary<string, Image>();

        static public void Create()
        {
            DirectoryInfo folder = new DirectoryInfo(path + "Resources");//目录信息
            foreach (FileInfo file in folder.GetFiles("*.png"))
            {
                string key = file.Name.Remove(file.Name.LastIndexOf("."));
                Image img = Image.FromFile(file.FullName);
                imageDict[key] = img;
            }
        }

        static public Image Load(string key)
        {
            return imageDict[key];
        }
    }
}
