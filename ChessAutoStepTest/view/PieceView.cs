//面试试题测试: by蔡业民 开始于 2019/10/17 


using Anim;
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
        BoardView boardView;
        public Piece piece;
        public Image img;
        public RectangleF rect;
        int animMS = 5000;
        Animation moveAnim;
        LinearAnimation linearPosX;
        LinearAnimation linearPosY;

        BoardIdx dstBoardIdx;
        public bool IsMoving = false;

        public Action<PieceView> MovedStopEvent = null;
    
        public PieceView(BoardView boardView, string imgName)
        {
            this.boardView = boardView;
            img = Resource.Load(imgName);
            moveAnim = new Animation(Chess.ChessView, animMS, true);
            moveAnim.AnimationEvent += MovedEvent;
        }

        public void StartMove(BoardIdx dstBoardIdx)
        {
            this.dstBoardIdx = dstBoardIdx;
            RectangleF dstRect  = boardView.GetTableCellRectByBoardIdx(dstBoardIdx);
            rect.Width = dstRect.Width;
            rect.Height = dstRect.Height;
            IsMoving = true;

            moveAnim.Stop();
            linearPosX = new LinearAnimation(rect.X, dstRect.X, moveAnim);
            linearPosY = new LinearAnimation(rect.Y, dstRect.Y, moveAnim);
            moveAnim.Start();
        }

        public void SuspendMove()
        {
            moveAnim.Stop();
        }

        private void MovedEvent(Animation animation)
        {
            rect.X = linearPosX.GetCurtValue();
            rect.Y = linearPosY.GetCurtValue();

            Chess.ChessView.Refresh();

            if (linearPosX.IsStop && linearPosY.IsStop)
            {
                animation.Stop();
                IsMoving = false;

                if (MovedStopEvent != null)
                    MovedStopEvent(this);
            }
        }


        public void Draw(Graphics g)
        {
            g.DrawImage(img, rect);
        }
    }
}
