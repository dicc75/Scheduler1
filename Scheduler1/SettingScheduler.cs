using System;
using System.Collections.Generic;

namespace Scheduler1
{
    public class SettingScheduler
    {
        #region Private Properties
        private TypeSetting type;
        private Boolean enable;
        private int every = 0;
        private DateTime? startDate;
        private DateTime? endDate;
        private DateTime? date;

        private DailyFrecuencyType? dailyType;
        private TimeSpan? dailyStartTime;
        private TimeSpan? dailyEndTime;
        
        private List<DayOfWeek> daysOfWeek;
        private int dailyFrecuencyEvery = 0;
        #endregion

        #region Public Properties
        public TypeSetting Type { get => type; set => type = value; }
        public bool Enable { get => enable; set => enable = value; }
        public int Every { get => every; set => every = value; }
        public DateTime? StartDate { get => startDate; set { startDate = value; } }
        public DateTime? EndDate { get => endDate; set { endDate = value; } }
        public DateTime? Date { get => date; set { date = value; } }

        public int DailyFrecuencyEvery { get => dailyFrecuencyEvery; set => dailyFrecuencyEvery = value; }
        public DailyFrecuencyType? DailyType { get => dailyType; set => dailyType = value; }
        public TimeSpan? DailyStartTime { get => dailyStartTime; set => dailyStartTime = value; }
        public TimeSpan? DailyEndTime { get => dailyEndTime; set => dailyEndTime = value; }
        
        public List<DayOfWeek> DaysOfWeek { get => daysOfWeek; set => daysOfWeek = value; }
        #endregion

        #region Constructor
        public SettingScheduler(TypeSetting type)
        {
            this.type = type;
            this.enable = true;
            this.daysOfWeek = new List<DayOfWeek>();
        }
        #endregion
    }
}
