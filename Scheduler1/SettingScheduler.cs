using System;

namespace Scheduler1
{
    public class SettingScheduler
    {
        private TypeSetting type;
        private OccurSetting occur;
        private Boolean enable;
        private int every = 0;
        private DateTime? startDate;
        private DateTime? endDate;
        private DateTime? date;

        public TypeSetting Type { get => type; set => type = value; }
        public OccurSetting Occur { get => occur; set => occur = value; }
        public bool Enable { get => enable; set => enable = value; }
        public int Every { get => every; set => every = value; }
        public DateTime? StartDate { get => startDate; set { startDate = value; } }
        public DateTime? EndDate { get => endDate; set { endDate = value; } }
        public DateTime? Date { get => date; set { date = value; } }

        public SettingScheduler(TypeSetting type, OccurSetting occur)
        {
            this.type = type;
            this.occur = occur;
            this.enable = true;
        }
    }
}
