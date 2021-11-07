using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scheduler1
{
    public class Task
    {
        #region Private Properties
        private SettingScheduler setting;
        #endregion

        #region Public Properties
        public SettingScheduler Setting { get => setting; set => setting = value; }
        #endregion

        #region Constructor
        public Task(SettingScheduler setting)
        {
            this.Setting = setting;
        }
        #endregion

        #region Private methods
        private DateTime GetNextDateOnce(DateTime input)
        {
            DateTime nextDate;

            ValidateOnce(input);

            nextDate = this.setting.Date.Value;

            return nextDate;
        }
        private DateTime GetNextDateRecurring(DateTime input)
        {
            TimeSpan nextTime;

            ValidateRecurring(input);

            DateTime nextDate = GetNextDateTime(input);
            if (nextDate.Date.CompareTo(input.Date) == 0)
            {
                nextTime = GetNextDailyTime(input.TimeOfDay);
            }
            else
            {
                nextTime = this.setting.DailyStartTime.Value; 
            }
            return nextDate.Add(nextTime);
        }
        private DateTime GetNextDateTime(DateTime input)
        {
            DateTime nextDate = GetNextDayOfWeekValid(input);

            if (this.setting.EndDate.HasValue == true &&
                this.setting.EndDate.Value.CompareTo(nextDate) < 0)
            {
                throw new ArgumentException($"The NextDate {nextDate} is greater than EndDate {this.setting.EndDate.Value}.");
            }
            
            return nextDate;
        }
        private TimeSpan GetNextDailyTime(TimeSpan input)
        {
            TimeSpan time = this.setting.DailyStartTime.Value;

            if (this.setting.DailyFrecuencyEvery == 0) { return time; }

            if (input.CompareTo(this.setting.DailyStartTime.Value) <= 0) { return time; }

            do
            {
                switch (this.setting.DailyType)
                {
                    case DailyFrecuencyType.Hours:
                        time = time.Add(new TimeSpan(this.setting.DailyFrecuencyEvery, 0, 0));
                        break;
                    case DailyFrecuencyType.Minutes:
                        time = time.Add(new TimeSpan(0, this.setting.DailyFrecuencyEvery, 0));
                        break;
                    case DailyFrecuencyType.Seconds:
                        time = time.Add(new TimeSpan(0, 0, this.setting.DailyFrecuencyEvery));
                        break;
                }

            } while (input.CompareTo(time) > 0 && this.setting.DailyEndTime.Value.CompareTo(time) > 0);

            return time;
        }
        private DateTime GetNextDayOfWeekValid(DateTime input)
        {
            DateTime nextDateTimeByDayOfWeek;

            this.ValidateNextDayOfWeek();

            nextDateTimeByDayOfWeek = input;

            if (IsWeekValid(input) == true)
            {
                nextDateTimeByDayOfWeek = GetNextDateTimeByDayOfWeek(input.AddDays(-1));

                if (nextDateTimeByDayOfWeek.Add(this.setting.DailyEndTime.Value) < input)
                {
                    nextDateTimeByDayOfWeek = GetNextDateTimeByDayOfWeek(input);
                }

                if (IsWeekValid(nextDateTimeByDayOfWeek) == true)
                {
                    return nextDateTimeByDayOfWeek;
                }
            }

            DateTime firstDayOfWeek = GetNextFirstDayOfWeek(nextDateTimeByDayOfWeek);
            do
            {
                firstDayOfWeek = firstDayOfWeek.AddDays(7);
            } while (IsWeekValid(firstDayOfWeek) == false);
            
            return GetNextDateTimeByDayOfWeek(firstDayOfWeek);
        }

        private DateTime GetNextDateTimeByDayOfWeek(DateTime current)
        {
            return Enumerable.Range(1, 7)
                    .Select(n => current.Date.AddDays(n))
                    .First(date => this.setting.DaysOfWeek.Contains(date.DayOfWeek));
        }

        private Boolean IsWeekValid(DateTime current)
        {
            CultureInfo info = CultureInfo.CurrentCulture;

            int weekInit = info.Calendar.GetWeekOfYear(this.setting.StartDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            int weekCurrent = info.Calendar.GetWeekOfYear(current, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            while (weekInit < weekCurrent)
            {
                weekInit += this.setting.Every;
            }

            if (weekInit == weekCurrent)
            {
                return true;
            }
            return false;
        }

        private DateTime GetNextFirstDayOfWeek(DateTime current)
        {
            while (current.DayOfWeek != DayOfWeek.Monday)
            {
                current = current.AddDays(-1);
            }

            return current;
        }
        #endregion

        #region Public methods
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
            string days = "";

            switch (Setting.Type)
            {
                case TypeSetting.Once:
                    description += "once";
                    break;
                case TypeSetting.Recurring:
                    if (this.setting.Every == 1) description += "every week on";
                    else description += "every " + this.setting.Every.ToString() + " weeks on"; ;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < this.setting.DaysOfWeek.Count; i++)
            {
                if (this.setting.DaysOfWeek.Count == 1)
                {
                    days += this.setting.DaysOfWeek[i].ToString().ToLower();
                }
                else
                { 
                    if (i == this.setting.DaysOfWeek.Count - 1) { days += " and "; }
                    days += this.setting.DaysOfWeek[i].ToString().ToLower();
                    if (i < this.setting.DaysOfWeek.Count - 2) { days += ", "; }
                }
            }

            if (Setting.Type == TypeSetting.Once)
            {
                description += ". Schedule will be used on ";
                description += nextDate.ToString("d", culture) + " at " + nextDate.ToString("t", culture);
            }

            if (string.IsNullOrEmpty(days) == false)
            {
                description += " " + days + " ";
            }
            if (Setting.DailyType.HasValue == true)
            {
                if (Setting.DailyType.Value != DailyFrecuencyType.Once)
                {
                    description += "every " + this.setting.DailyFrecuencyEvery.ToString();
                    switch (Setting.DailyType)
                    {
                        case DailyFrecuencyType.Hours:
                            description += " hours";
                            break;
                        case DailyFrecuencyType.Minutes:
                            description += " minutes";
                            break;
                        case DailyFrecuencyType.Seconds:
                            description += " seconds";
                            break;
                    }
                }
                if (this.setting.DailyStartTime.HasValue == true)
                {
                    description += " between " + this.setting.DailyStartTime.Value.ToString();
                }
                if (this.setting.DailyEndTime.HasValue == true)
                {
                    description += " and " + this.setting.DailyEndTime.Value.ToString();
                }
            }
            
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
        #endregion

        #region Validations
        private void ValidateDates(DateTime input)
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
            ValidateDates(input);

            if (this.setting.Type == TypeSetting.Once && this.setting.Date.HasValue == false)
            {
                throw new ArgumentNullException("Date", "The value cannot be null.");
            }
        }
        private void ValidateRecurring(DateTime input)
        {
            ValidateDates(input);

            if (this.setting.Type == TypeSetting.Recurring)
            {
                if (this.setting.Every <= 0)
                {
                    throw new ArgumentException("Every is invalid.");
                }
            }

            ValidateDailyTime();
        }
        private void ValidateDailyTime()
        {
            if (this.setting.DailyStartTime.HasValue == false)
            {
                throw new ArgumentException($"The StartTime is invalid.");
            }

            if (this.setting.DailyEndTime.HasValue == false)
            {
                throw new ArgumentException($"The EndTime is invalid.");
            }

            if (this.setting.DailyStartTime.Value.CompareTo(this.setting.DailyEndTime.Value) > 0)
            {
                throw new ArgumentException($"The StartTime {this.setting.DailyStartTime.Value} is greater than EndTime {this.setting.DailyEndTime.Value}.");
            }

            switch (this.setting.DailyType)
            {
                case DailyFrecuencyType.Hours:
                    if (this.setting.DailyFrecuencyEvery < 0)
                    {
                        throw new ArgumentException($"The Every hours is invalid.");
                    }
                    break;
                case DailyFrecuencyType.Minutes:
                    if (this.setting.DailyFrecuencyEvery < 0)
                    {
                        throw new ArgumentException($"The Every minutes is invalid.");
                    }
                    break;
                case DailyFrecuencyType.Seconds:
                    if (this.setting.DailyFrecuencyEvery < 0)
                    {
                        throw new ArgumentException($"The Every seconds is invalid.");
                    }
                    break;
            }
        }
        private void ValidateNextDayOfWeek()
        {
            if (this.setting.DaysOfWeek.Count == 0)
            {
                throw new ArgumentException("No day of the week has been specified.");
            }
        }
        #endregion
    }
}
