using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scheduler1
{
    public class SchedulerTask
    {
        #region Constructor
        public SchedulerTask()
        {
        }
        #endregion

        #region Private methods
        private static DateTime GetNextDateOnce(SettingScheduler setting)
        {
            ValidateOnce(setting);

            return setting.Date.Value;
        }
        private static DateTime GetNextDateRecurring(SettingScheduler setting, DateTime input)
        {
            TimeSpan nextTime;

            ValidateRecurring(setting, input);

            DateTime nextDate = GetNextDateTime(setting, input);
            if (nextDate.Date.CompareTo(input.Date) == 0)
            {
                nextTime = GetNextDailyTime(setting, input.TimeOfDay);
            }
            else
            {
                if (setting.DailyType == DailyFrecuencyType.Once)
                {
                    nextTime = setting.DailyOnceTime.Value;
                }
                else
                {
                    nextTime = setting.DailyStartTime.Value;
                }
            }
            return nextDate.Add(nextTime);
        }
        private static DateTime GetNextDateTime(SettingScheduler setting, DateTime input)
        {
            DateTime nextDate = GetNextDayOfWeekValid(setting, input);

            if (setting.EndDate.HasValue == true &&
                setting.EndDate.Value.CompareTo(nextDate) < 0)
            {
                throw new SchedulerException(SchedulerException.NextDateIsGreaterThanEndDateMessage(nextDate,setting.EndDate.Value));
            }
            
            return nextDate;
        }
        private static TimeSpan GetNextDailyTime(SettingScheduler setting, TimeSpan input)
        {
            TimeSpan time;

            if (setting.DailyType == DailyFrecuencyType.Once)
            {
                return setting.DailyOnceTime.Value;
            }

            time = setting.DailyStartTime.Value;

            if (setting.DailyFrecuencyEvery == 0) { return time; }

            if (input.CompareTo(setting.DailyStartTime.Value) <= 0) { return time; }

            do
            {
                switch (setting.DailyType)
                {
                    case DailyFrecuencyType.Hours:
                        time = time.Add(new TimeSpan(setting.DailyFrecuencyEvery, 0, 0));
                        break;
                    case DailyFrecuencyType.Minutes:
                        time = time.Add(new TimeSpan(0, setting.DailyFrecuencyEvery, 0));
                        break;
                    case DailyFrecuencyType.Seconds:
                        time = time.Add(new TimeSpan(0, 0, setting.DailyFrecuencyEvery));
                        break;
                }

            } while (input.CompareTo(time) > 0 && setting.DailyEndTime.Value.CompareTo(time) > 0);

            return time;
        }
        private static DateTime GetNextDayOfWeekValid(SettingScheduler setting, DateTime input)
        {
            DateTime nextDateTimeByDayOfWeek;

            ValidateNextDayOfWeek(setting);

            nextDateTimeByDayOfWeek = input;

            if (IsWeekValid(setting, input) == true)
            {
                nextDateTimeByDayOfWeek = GetNextDateTimeByDayOfWeek(setting, input.AddDays(-1));

                if (setting.DailyType == DailyFrecuencyType.Once)
                {
                    if (nextDateTimeByDayOfWeek.Add(setting.DailyOnceTime.Value) < input)
                    {
                        nextDateTimeByDayOfWeek = GetNextDateTimeByDayOfWeek(setting, input);
                    }
                }
                else
                {
                    if (nextDateTimeByDayOfWeek.Add(setting.DailyEndTime.Value) < input)
                    {
                        nextDateTimeByDayOfWeek = GetNextDateTimeByDayOfWeek(setting, input);
                    }
                }

                if (IsWeekValid(setting, nextDateTimeByDayOfWeek) == true)
                {
                    return nextDateTimeByDayOfWeek;
                }
            }

            DateTime firstDayOfWeek = GetNextFirstDayOfWeek(nextDateTimeByDayOfWeek);
            do
            {
                firstDayOfWeek = firstDayOfWeek.AddDays(7);
            } while (IsWeekValid(setting, firstDayOfWeek) == false);
            
            return GetNextDateTimeByDayOfWeek(setting, firstDayOfWeek);
        }
        private static DateTime GetNextDateTimeByDayOfWeek(SettingScheduler setting, DateTime current)
        {
            return Enumerable.Range(1, 7)
                    .Select(n => current.Date.AddDays(n))
                    .First(date => setting.DaysOfWeek.Contains(date.DayOfWeek));
        }
        private static Boolean IsWeekValid(SettingScheduler setting, DateTime current)
        {
            CultureInfo info = CultureInfo.CurrentCulture;

            int weekInit = info.Calendar.GetWeekOfYear(setting.StartDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            int weekCurrent = info.Calendar.GetWeekOfYear(current, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            while (weekInit < weekCurrent)
            {
                weekInit += setting.Every;
            }

            return weekInit == weekCurrent ? true : false;
        }
        private static DateTime GetNextFirstDayOfWeek(DateTime current)
        {
            while (current.DayOfWeek != DayOfWeek.Monday)
            {
                current = current.AddDays(-1);
            }

            return current;
        }
        private static string DescriptionOnce(SettingScheduler setting)
        {
            string description = "once";

            description += TypeSettingOnceDescription(setting);
            description += StartEndDescription(setting);

            return description;
        }
        private static string TypeSettingOnceDescription(SettingScheduler setting)
        {
            string description = "";

            if (setting.Type == TypeSetting.Once)
            {
                description = ". Scheduler will be used on ";
                description += setting.Date.Value.ToString("d", CultureInfo.CurrentCulture) + " at " + setting.Date.Value.ToString("t", CultureInfo.CurrentCulture);
            }

            return description;
        }
        private static string DescriptionRecurring(SettingScheduler setting)
        {
            string description = "";

            description += EveryDescription(setting);
            description += DaysOfAWeekDescription(setting);
            description += DailyTimesDescripcion(setting);
            description += StartEndDescription(setting);

            return description;
        }
        private static string EveryDescription(SettingScheduler setting)
        {
            if (setting.Every == 1) return "every week on";
            else return "every " + setting.Every.ToString() + " weeks on"; ;
        }
        private static string DaysOfAWeekDescription(SettingScheduler setting)
        {
            string days = "";

            if (setting.DaysOfWeek.Count == 0) { return days; }

            if (setting.DaysOfWeek.Count == 1)
            {
                days += " " + setting.DaysOfWeek[0].ToString().ToLower() + " ";
            }
            else
            {
                days += " "; 
                for (int i = 0; i < setting.DaysOfWeek.Count; i++)
                {
                    if (i == setting.DaysOfWeek.Count - 1) { days += " and "; }
                    days += setting.DaysOfWeek[i].ToString().ToLower();
                    if (i < setting.DaysOfWeek.Count - 2) { days += ", "; }
                }
                days += " ";
            }   
             
            return days;
        }
        private static string DailyTimesDescripcion(SettingScheduler setting)
        {
            string description = "";

            if (setting.DailyType.HasValue == true)
            {
                if (setting.DailyType.Value != DailyFrecuencyType.Once)
                {
                    description += "every " + setting.DailyFrecuencyEvery.ToString();
                    switch (setting.DailyType)
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
                else
                {
                    description += "every " + setting.DailyOnceTime.ToString();
                }
                if (setting.DailyStartTime.HasValue == true)
                {
                    description += " between " + setting.DailyStartTime.Value.ToString();
                }
                if (setting.DailyEndTime.HasValue == true)
                {
                    description += " and " + setting.DailyEndTime.Value.ToString();
                }
            }

            return description;
        }
        private static string StartEndDescription(SettingScheduler setting)
        {
            string description = "";

            if (setting.StartDate.HasValue == true)
            {
                description += " starting on " + setting.StartDate.Value.ToString("d", CultureInfo.CurrentCulture) +
                    " at " + setting.StartDate.Value.ToString("t", CultureInfo.CurrentCulture);
            }
            if (setting.EndDate.HasValue == true)
            {
                description += " until " + setting.EndDate.Value.ToString("d", CultureInfo.CurrentCulture) +
                    " at " + setting.EndDate.Value.ToString("t", CultureInfo.CurrentCulture);
            }
            description += ".";

            return description;
        }
        #endregion

        #region Public methods
        public static DateTime? GetNextDate(SettingScheduler setting, DateTime input)
        {
            DateTime? nextDate = null;

            switch (setting.Type)
            {
                case TypeSetting.Once:
                    nextDate = GetNextDateOnce(setting);
                    break;
                case TypeSetting.Recurring:
                    nextDate = GetNextDateRecurring(setting, input);
                    break;
            }
            return nextDate;
        }
        public static string GetDescriptionNextDate(SettingScheduler setting)
        {
            string description = "Occurs ";

            switch (setting.Type)
            {
                case TypeSetting.Once:
                    description += DescriptionOnce(setting);
                    break;
                case TypeSetting.Recurring:
                    description += DescriptionRecurring(setting);
                    break;
                default:
                    break;
            }

            return description;
        }
        #endregion

        #region Validations
        private static void ValidateOnce(SettingScheduler setting)
        {
            if (setting.Enable == false)
            {
                throw new SchedulerException(SchedulerException.DisabledMessage);
            }

            if (setting.Type == TypeSetting.Once && setting.Date.HasValue == false)
            {
                throw new SchedulerException(SchedulerException.DateNullMessage);
            }
        }
        private static void ValidateRecurring(SettingScheduler setting, DateTime input)
        {
            if (setting.Enable == false)
            {
                throw new SchedulerException(SchedulerException.DisabledMessage);
            }

            if (setting.Type == TypeSetting.Recurring)
            {
                if (setting.Every <= 0)
                {
                    throw new SchedulerException(SchedulerException.EveryIsInvalidMessage);
                }
            }

            ValidateDates(setting, input);
            ValidateDailyTime(setting);
            ValidateDailyFrecuencyEvery(setting);
        }
        private static void ValidateDates(SettingScheduler setting, DateTime input)
        {
            if (setting.EndDate.HasValue == true)
            {
                if (setting.EndDate.Value.CompareTo(input) < 0)
                {
                    throw new SchedulerException(SchedulerException.InputIsGreaterThanEndDateMessage(input, setting.EndDate.Value));
                }

                if (setting.Date.HasValue == true)
                {
                    if (setting.EndDate.Value.CompareTo(setting.Date.Value) < 0)
                    {
                        throw new SchedulerException(SchedulerException.DateIsGreaterThanEndDateMessage(setting.Date.Value, setting.EndDate.Value));
                    }
                }
            }
        }
        private static void ValidateDailyTime(SettingScheduler setting)
        {
            if (setting.DailyType == DailyFrecuencyType.Once)
            {
                if (setting.DailyOnceTime.HasValue == false)
                {
                    throw new SchedulerException(SchedulerException.OnceTimeIsInvalidMessage);
                }
            }
            else
            {
                if (setting.DailyStartTime.HasValue == false)
                {
                    throw new SchedulerException(SchedulerException.StartTimeIsInvalidMessage);
                }

                if (setting.DailyEndTime.HasValue == false)
                {
                    throw new SchedulerException(SchedulerException.EndTimeIsInvalidMessage);
                }

                if (setting.DailyStartTime.Value.CompareTo(setting.DailyEndTime.Value) > 0)
                {
                    throw new SchedulerException(SchedulerException.StartTimeIsGreaterThanEndTimeMessage(setting.DailyStartTime.Value, setting.DailyEndTime.Value));
                }
            }
        }
        private static void ValidateDailyFrecuencyEvery(SettingScheduler setting)
        {
            switch (setting.DailyType)
            {
                case DailyFrecuencyType.Hours:
                    if (setting.DailyFrecuencyEvery < 0)
                    {
                        throw new SchedulerException(SchedulerException.EveryHoursIsInvalidMessage);
                    }
                    break;
                case DailyFrecuencyType.Minutes:
                    if (setting.DailyFrecuencyEvery < 0)
                    {
                        throw new SchedulerException(SchedulerException.EveryMinutesIsInvalidMessage);
                    }
                    break;
                case DailyFrecuencyType.Seconds:
                    if (setting.DailyFrecuencyEvery < 0)
                    {
                        throw new SchedulerException(SchedulerException.EverySecondsIsInvalidMessage);
                    }
                    break;
            }
        }
        private static void ValidateNextDayOfWeek(SettingScheduler setting)
        {
            if (setting.DaysOfWeek.Count == 0)
            {
                throw new SchedulerException(SchedulerException.NoDayOfTheWeekMessage);
            }
        }
        #endregion
    }
}
