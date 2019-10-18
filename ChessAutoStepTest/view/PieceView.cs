using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class PieceView
    {
        public Piece piece;
        public Image img;
        int width;
        int height;

        public PieceView(string imgName)
        {
            img = Resource.Load(imgName);
        }

        public void Draw(Graphics g, Point pos)
        {
            g.DrawImage(img, pos.X, pos.Y, width, height);
        }
    }
}
