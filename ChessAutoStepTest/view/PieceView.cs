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

        public PieceView(string imgName)
        {
            img = Resource.Load(imgName);
        }

        public void Draw(Graphics g, Point pos, int width, int height)
        {
            g.DrawImage(img, pos.X, pos.Y, width, height);
        }
    }
}
