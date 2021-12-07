using System;
using System.Collections.Generic;

namespace Scheduler1
{
    public class SettingScheduler
    {
        #region Public Properties
        public TypeSetting Type { get; }
        public bool Enable { get; set; }
        public int Every { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Date { get; set; }

        public int DailyFrecuencyEvery { get; set; }
        public DailyFrecuencyType? DailyType { get; set; }
        public TimeSpan? DailyStartTime { get; set; }
        public TimeSpan? DailyEndTime { get; set; }
        public TimeSpan? DailyOnceTime { get; set; }

        public List<DayOfWeek> DaysOfWeek { get; set; }
        #endregion

        #region Constructor
        public SettingScheduler(TypeSetting type)
        {
            this.Type = type;
            this.Enable = true;
            this.DaysOfWeek = new List<DayOfWeek>();
        }
        #endregion
    }
}
