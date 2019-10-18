﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anim
{
    public class Animation:IDisposable
    {
        Control control;
        private System.Timers.Timer refreshTimer = null;
        bool autoRest = false;
        int durationMS = 20;
        public int animMS = 100;
        int frameIndex = 0;
        bool isDisposed = false;

        public delegate void AnimationEventHandler(Animation scAnimation);
        public event AnimationEventHandler AnimationEvent;

        delegate void UpdateCallback(object obj);
        UpdateCallback updateDet;

        public Animation(Control control, bool autoRest)
        {
            this.control = control;
            this.autoRest = autoRest;
            updateDet = new UpdateCallback(Update);
        }

        public Animation(Control control, int animMS, bool autoRest)
        {
            this.control = control;
            this.autoRest = autoRest;
            this.animMS = animMS;   
            updateDet = new UpdateCallback(Update);

        }

        public int AnimMS
        {
            get
            {
                return animMS;
            }
        }

        public int DurationMS
        {
            get
            {
                return durationMS;
            }
            set
            {
                durationMS = value;
            }
        }

        public int FrameIndex
        {
            get
            {
                return frameIndex;
            }
        }

        public void Start()
        {
            frameIndex = 0;
            StartTimer(durationMS);
        }

        public void Stop()
        {
            StopTimer();  
        }

        void StartTimer(int period)
        {
           if (isDisposed)
                return;

            refreshTimer = new System.Timers.Timer(period);   //实例化Timer类，设置间隔时间为period毫秒   
            refreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(theout); //到达时间的时候执行事件   
            refreshTimer.AutoReset = autoRest;   //设置是执行一次（false）还是一直执行(true)   
            refreshTimer.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件  
        }

        void StopTimer()
        {
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
                refreshTimer = null;
            }
        }

        public void theout(object source, EventArgs e)
        {
            if (control == null )
                return;

            frameIndex++;
            control.Invoke(updateDet, this);
        }

        void Update(object obj)
        {
            AnimationEvent?.Invoke(this);
        }

        public void Dispose()
        {
            Stop();
            control = null;
            isDisposed = true;            
        }
    }
   
}