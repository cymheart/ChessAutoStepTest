using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anim
{
    public class AnimationEffect
    {
        protected bool isStop = false;
        public virtual float GetCurtValue()
        {
            return 0;
        }

        public bool IsStop
        {
            get
            {
                return isStop;
            }
            set
            {
                isStop = value;
            }
        }
    }

    public class ScLinearAnimation:AnimationEffect
    {
        float n = 0;
        Animation scAnim = null;
        float startValue;
        float stopValue;

        public ScLinearAnimation(float startValue, float stopValue, Animation scAnimation)
        {
            scAnim = scAnimation;
            this.startValue = startValue;
            this.stopValue = stopValue;
            n = (stopValue - startValue) / scAnim.AnimMS * scAnim.DurationMS;
        }

        public override float GetCurtValue()
        {
            float val = startValue + scAnim.FrameIndex * n;

            if ((n >= 0 && val >= stopValue) || (n <= 0 && val <= stopValue))
            {
                val = stopValue;
                isStop = true;
            }

            return val;
        }
    }

    public class ScStepAnimation : AnimationEffect
    {
        float n = 0;
        Animation scAnim = null;
        float startValue;

        public ScStepAnimation(float startValue, float step, Animation scAnimation)
        {
            scAnim = scAnimation;
            this.startValue = startValue;
            n = step;
        }

        public override float GetCurtValue()
        {
            float val = startValue + scAnim.FrameIndex * n;
            return val;
        }
    }
}
