using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler1
{
    public class SettingScheduler
    {
        private TypeSetting type;
        private OccurSetting occur;
        private int every; //days
        private DateTime startDate;
        private DateTime? endDate;
        private Boolean enable;
        private DateTime? date;

        public TypeSetting Type { get => type; set => type = value; }
        public OccurSetting Occur { get => occur; set => occur = value; }
        public int Every { get => every; set => every = value; }
        public bool Enable { get => enable; set => enable = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime? EndDate { get => endDate; set { endDate = value; } }
        public DateTime? Date { get => date; set { date = value; } }

        public SettingScheduler(TypeSetting type, OccurSetting occur, DateTime startDate, DateTime? date) : base()
        {
            this.type = type;
            this.occur = occur;
            this.startDate = startDate;
            this.date = date;
            this.every = 0;
            this.endDate = null;
            this.enable = true;
        }

        public SettingScheduler(TypeSetting type, OccurSetting occur, DateTime startDate, int every)
           : this(type, occur, startDate, null)
        {
            this.every = every;
        }

        public SettingScheduler(TypeSetting type, OccurSetting occur, DateTime startDate, int every, DateTime? date, DateTime? endDate, Boolean enable)
            :this(type, occur, startDate, date)
        {
            this.every = every;
            this.endDate = endDate;
            this.enable = enable;
        }

        public void Validate(DateTime input)
        {
            if (this.Enable == false)
            {
                throw new Exception("Setting is disabled.");
            }

            if (this.StartDate.CompareTo(input) > 0 || 
                    (this.EndDate.HasValue == true &&
                    input.CompareTo(this.EndDate.Value) > 0))
            {
                throw new Exception("Input is invalid.");
            }

            if (this.type == TypeSetting.Once &&
                    this.Date.HasValue == true &&
                    (this.StartDate.CompareTo(this.Date.Value) > 0 || 
                        (this.EndDate.HasValue == true &&
                        Date.Value.CompareTo(this.EndDate.Value) > 0)))
            {
                throw new Exception("Date is invalid.");
            }

            if (this.type == TypeSetting.Recurring && this.every <= 0)
            {
                throw new Exception("Every is invalid.");
            }
        }
        
    }
}
