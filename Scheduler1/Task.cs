using System;
using System.Globalization;

namespace Scheduler1
{
    public class Task
    {
        private SettingScheduler setting;

        public SettingScheduler Setting { get => setting; set => setting = value; }

        public Task(SettingScheduler setting)
        {
            this.Setting = setting;
        }

        private DateTime GetNextDateOnce(DateTime input)
        {
            DateTime nextDate;

            ValidateOnce(input);

            nextDate = this.setting.Date.Value;
        
            return nextDate;
        }

        private DateTime GetNextDateRecurring(DateTime input)
        {
            DateTime nextDate;

            ValidateRecurring(input);

            DaysOfWeek dayOfWeek = (DaysOfWeek)input.DayOfWeek;


            nextDate = this.setting.StartDate.Value;
            do
            {
                nextDate = nextDate.AddDays(this.setting.Every);

                if (this.setting.EndDate.HasValue == true &&
                    this.setting.EndDate.Value.CompareTo(nextDate) < 0)
                {
                    throw new ArgumentException($"The NextDate {nextDate} is greater than EndDate {this.setting.EndDate.Value}.");
                }
            } while (nextDate.CompareTo(input) < 1);

            return nextDate;
        }

        public DateTime? GetNextDate(DateTime input)
        {
            DateTime? nextDate = null;

            switch (Setting.Type)
            {
                case TypeSetting.Once:
                    nextDate = GetNextDateOnce(input);
                    break;
                case TypeSetting.Recurring:
                    nextDate = GetNextDateRecurring(input);
                    break;
            }
            return nextDate;
        }

        public string GetDescriptionNextDate(DateTime nextDate)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");

            string description = "Occurs ";

            switch (Setting.Type)
            {
                case TypeSetting.Once:
                    description += "once";
                    break;
                case TypeSetting.Recurring:
                    if (this.setting.Every == 1) description += "every day";
                    else description += "every " + this.setting.Every.ToString() + " days"; ;
                    break;
                default:
                    break;
            }

            description += ". Schedule will be used on ";
            description += nextDate.ToString("d", culture) + " at " + nextDate.ToString("t", culture);
            if (this.setting.StartDate.HasValue == true)
            {
                description += " starting on " + this.setting.StartDate.Value.ToString("d", culture) +
                    " at " + this.setting.StartDate.Value.ToString("t", culture);
            }
            if (this.setting.EndDate.HasValue == true)
            {
                description += " until " + this.setting.EndDate.Value.ToString("d", culture) +
                    " at " + this.setting.EndDate.Value.ToString("t", culture);
            }
            description += ".";

            return description;
        }

        private void Validate(DateTime input)
        {
            if (this.setting.Enable == false)
            {
                throw new ArgumentException("Setting is disabled.");
            }

            if (this.setting.StartDate.HasValue == true)
            {
                if (this.setting.StartDate.Value.CompareTo(input) > 0)
                {
                    throw new ArgumentException($"The Input {input} is less than StartDate {this.setting.StartDate.Value}.");
                }

                if (this.setting.Date.HasValue == true)
                {
                    if (this.setting.StartDate.Value.CompareTo(this.setting.Date.Value) > 0)
                    {
                        throw new ArgumentException($"The Date {this.setting.Date.Value} is less than StartDate {this.setting.StartDate.Value}."); 
                    }
                }
            }

            if (this.setting.EndDate.HasValue == true)
            {
                if (this.setting.EndDate.Value.CompareTo(input) < 0)
                {
                    throw new ArgumentException($"The Input {input} is greater than EndDate {this.setting.EndDate.Value}."); 
                }

                if (this.setting.Date.HasValue == true)
                {
                    if (this.setting.EndDate.Value.CompareTo(this.setting.Date.Value) < 0)
                    {
                        throw new ArgumentException($"The Date {this.setting.Date.Value} is greater than EndDate {this.setting.EndDate.Value}."); 
                    }
                }
            }
        }

        private void ValidateOnce(DateTime input)
        {
            Validate(input);

            if (this.setting.Type == TypeSetting.Once && this.setting.Date.HasValue == false)
            {
                throw new ArgumentNullException("Date","The value cannot be null.");
            }
        }

        private void ValidateRecurring(DateTime input)
        {
            Validate(input);

            if (this.setting.Type == TypeSetting.Recurring)
            {
                if (this.setting.Every <= 0)
                {
                    throw new ArgumentException("Every is invalid.");
                }

                if (this.setting.DaysOfWeek.HasValue == false)
                {
                    throw new ArgumentException("No day of the week has been specified.");
                }

            }
        }
        private DaysOfWeek GetDayOfWeek(DateTime day)
        {
            return (DaysOfWeek)Math.Pow(2, (double)day.DayOfWeek);
        }

        private TimeSpan GetDailyTime(TimeSpan StartTime, DateTime input, TimeSpan EndTime, int every, DailyFrecuencyType type)
        {
            TimeSpan time = StartTime;

            do
            {
                switch (type)
                {
                    case DailyFrecuencyType.Hours:
                        time.Add(new TimeSpan(every, 0, 0));
                        break;
                    case DailyFrecuencyType.Minutes:
                        time.Add(new TimeSpan(0, every, 0));
                        break;
                    case DailyFrecuencyType.Seconds:
                        time.Add(new TimeSpan(0, 0, every));
                        break;
                }

            } while (EndTime.CompareTo(time) > 0 || input.TimeOfDay.CompareTo(time) > 0);

            return time;
        }
    }
}
