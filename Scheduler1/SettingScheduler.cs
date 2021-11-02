using System;

namespace Scheduler1
{
    public class SettingScheduler
    {
        private TypeSetting type;
        private Boolean enable;
        private int every = 0;
        private DateTime? startDate;
        private DateTime? endDate;
        private DateTime? date;

        private int weeklyFrecuencyEvery;
        private DailyFrecuencyType? dailyType;
        private TimeSpan? dailyStartTime;
        private TimeSpan? dailyEndTime;
        private TimeSpan? dailyTime;

        private DaysOfWeek? daysOfWeek;
        private int dailyFrecuencyEvery;

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
        public TimeSpan? DailyTime { get => dailyTime; set => dailyTime = value; }

        public DaysOfWeek? DaysOfWeek { get => daysOfWeek; set => daysOfWeek = value; }
        public int WeeklyFrecuencyEvery { get => weeklyFrecuencyEvery; set => weeklyFrecuencyEvery = value; }
       

        public SettingScheduler(TypeSetting type)
        {
            this.type = type;
            this.enable = true;
        }
    }
}
