using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler1
{
    [Serializable]
    public class SchedulerException : Exception
    {
        public const string DisabledMessage = "Setting is disabled.";
        public const string DateNullMessage = "The value of Date cannot be null.";
        public const string EveryIsInvalidMessage = "Every is invalid.";
        public const string StartTimeIsInvalidMessage = "The StartTime is invalid.";
        public const string OnceTimeIsInvalidMessage = "The OnceTime is invalid.";
        public const string EndTimeIsInvalidMessage = "The EndTime is invalid.";
        public const string EveryHoursIsInvalidMessage = "The Every hours is invalid.";
        public const string EveryMinutesIsInvalidMessage = "The Every minutes is invalid.";
        public const string EverySecondsIsInvalidMessage = "The Every seconds is invalid.";
        public const string NoDayOfTheWeekMessage = "No day of the week has been specified.";
        
        public SchedulerException() { }

        public SchedulerException(string message)
            : base(message) { }

        public static string DateIsGreaterThanEndDateMessage(DateTime date, DateTime endDate) { return $"The Date {date} is greater than StartDate {endDate}."; }
        public static string InputIsGreaterThanEndDateMessage(DateTime input, DateTime endDate) { return ($"The Input {input} is greater than EndDate {endDate}."); }
        public static string NextDateIsGreaterThanEndDateMessage(DateTime nextDate, DateTime endDate) { return ($"The NextDate {nextDate} is greater than EndDate {endDate}."); }
        public static string StartTimeIsGreaterThanEndTimeMessage(TimeSpan startTime, TimeSpan endTime) { return ($"The StartTime {startTime} is greater than EndTime {endTime}.");  }
    }
}
