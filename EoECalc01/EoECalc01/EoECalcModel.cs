using System;
using System.ComponentModel;

namespace EoECalc01
{
    class EoECalcModel : INotifyPropertyChanged
    {
        double nowCounts, nowSec, nowCps, goalCounts=160000, goalTime;
        int goalDay, goalHour, goalMinute,goalSec;
        const double halfTime = 2.8 * 24 * 3600;

        public event PropertyChangedEventHandler PropertyChanged;

        public double NowCounts
        {
            set
            {
                if (nowCounts != value)
                {
                    nowCounts = value;
                    OnPropertyChanged("NowCounts");
                    CpsCalculate();
                }
                
            }
            get
            {
                return nowCounts;
            }
        }
        public double NowSec
        {
            set
            {
                if (nowSec != value)
                {
                    nowSec = value;
                    OnPropertyChanged("NowSec");
                    CpsCalculate();
                }   
            }
            get
            {
                return nowSec;
            }
        }
        public double NowCps
        {
            set
            {
                nowCps = value;
                OnPropertyChanged("NowCps");
                GoalCalculate();
            }
            get
            {
                return nowCps;
            }
        }
        public double GoalTime
        {
            set
            {
                goalTime = value;
                OnPropertyChanged("GoalTime");
            }
            get
            {
                return goalTime;
            }
        }
        void CpsCalculate()
        {
            NowCps = NowCounts / NowSec;
        }

        public double GoalCounts
        {
            set
            {
                goalCounts = value;
                OnPropertyChanged("GoalCounts");
                GoalCalculate();
            }
            get
            {
                return goalCounts;
            }
        }
        
        public int GoalDay
        {
            get => goalDay;
            set
            {
                    goalDay = value;
                OnPropertyChanged("GoalDay");   
            }
        }
        public int GoalHour {
            get => goalHour;
            set
            {
                goalHour = value;
                OnPropertyChanged("GoalHour");
            }
        }
        public int GoalMinute
        {
            get => goalMinute;
            set
            {
                goalMinute = value;
                OnPropertyChanged("GoalMinute");
            }
        }
        public int GoalSec
        {
            get => goalSec;
            set
            {
                goalSec = value;
                OnPropertyChanged("GoalSec");
            }
        }
        
        void GoalCalculate()
        {
            GoalTime = halfTime / Math.Log(2.0) * (-1) * Math.Log(1 - (Math.Log(2.0) / halfTime) * (GoalCounts / NowCps));
            GoalDay = (int)Math.Truncate(GoalTime / (24 * 60 * 60));
            GoalHour = (int)(GoalTime%(24*60*60)) / (60 * 60);
            GoalMinute = (int)((GoalTime % (24 * 60 * 60)) % (60 * 60)) / 60;
            GoalSec =(int) ((GoalTime % (24 * 60 * 60)) % (60 * 60)) % 60;
            //doubleからintへの型変換時には値の切り捨てが行われる
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}