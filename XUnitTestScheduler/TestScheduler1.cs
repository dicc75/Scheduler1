using System;
using Xunit;
using Scheduler1;
using FluentAssertions;
using System.Collections.Generic;

namespace XUnitTestScheduler
{
    public class TestScheduler1
    {
        [Fact]
        public void Scheduler_Once_Disabled_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
                Enable = false
            };

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.DisabledMessage);
        }
        [Fact]
        public void Scheduler_Once_Date_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
            };

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.DateNullMessage);
        }
        [Fact]
        public void Scheduler_Recurring_Disabled_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                Enable = false
            };

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.DisabledMessage);
        }
        [Fact]
        public void Scheduler_Recurring_Every_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                Every = 0
            };

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.EveryIsInvalidMessage);
        }
        [Fact]
        public void Scheduler_Recurring_Input_Greater_Than_EndDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 6);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                EndDate = new DateTime(2020, 1, 5),
                Every = 1
            };

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.InputIsGreaterThanEndDateMessage(input,setting.EndDate.Value));
        }
        [Fact]
        public void Scheduler_Recurring_Date_Greater_Than_EndDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 3);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                Date = new DateTime(2020, 1, 7),
                EndDate = new DateTime(2020, 1, 4),
                Every = 1
            };

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.DateIsGreaterThanEndDateMessage(setting.Date.Value,setting.EndDate.Value));
        }
        [Fact]
        public void Scheduler_Recurring_NextDate_Greater_Than_EndDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 6);
            DateTime expected = new DateTime(2021, 11, 11);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                EndDate = new DateTime(2021, 11, 7),
                Every = 1,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 1, 
                DailyType = DailyFrecuencyType.Hours 
            };
            setting.DaysOfWeek.Add(DayOfWeek.Thursday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.NextDateIsGreaterThanEndDateMessage(expected, setting.EndDate.Value));
        }
        [Fact]
        public void Scheduler_Recurring_No_DayOfTheWeek__ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Minutes
            };

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.NoDayOfTheWeekMessage);
        }
        [Fact]
        public void Scheduler_Recurring_DailyFrecuency_Once_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = -1,
                DailyType = DailyFrecuencyType.Once 
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.OnceTimeIsInvalidMessage);
        }
        [Fact]
        public void Scheduler_Recurring_DailyFrecuency_Every_Hours_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = -1,
                DailyType = DailyFrecuencyType.Hours
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.EveryHoursIsInvalidMessage);
        }
        [Fact]
        public void Scheduler_Recurring_DailyFrecuency_Every_Minutes_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = -1,
                DailyType = DailyFrecuencyType.Minutes
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.EveryMinutesIsInvalidMessage);
        }
        [Fact]
        public void Scheduler_Recurring_DailyFrecuency_Every_Seconds_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = -1,
                DailyType = DailyFrecuencyType.Seconds
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.EverySecondsIsInvalidMessage);
        }
        [Fact]
        public void Scheduler_Recurring_DailyStartTime_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyFrecuencyEvery = -1,
                DailyType = DailyFrecuencyType.Seconds
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.StartTimeIsInvalidMessage);
        }
        [Fact]
        public void Scheduler_Recurring_DailyEndTime_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyFrecuencyEvery = -1,
                DailyType = DailyFrecuencyType.Seconds
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.EndTimeIsInvalidMessage);
        }
        [Fact]
        public void Scheduler_Recurring_DailyStartTime_Greater_than_DailyEndTime_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(19, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = -1,
                DailyType = DailyFrecuencyType.Seconds
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Action action = () => SchedulerTask.GetNextDate(setting, input);
            action.Should().Throw<SchedulerException>().WithMessage(SchedulerException.StartTimeIsGreaterThanEndTimeMessage(setting.DailyStartTime.Value, setting.DailyEndTime.Value));
        }
        [Fact]
        public void Scheduler_Once_Date_NextDate_And_Description()
        {
            DateTime? expected = new DateTime(2020, 1, 8, 14, 0, 0);
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
                Date = new DateTime(2020, 1, 8, 14, 0, 0)
            };

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                "Occurs once. Scheduler will be used on 08/01/2020 at 14:00.");
        }
        [Fact]
        public void Scheduler_Once_StartDate_EndDate_Description()
        {
            DateTime expected = new DateTime(2020, 1, 8, 14, 0, 0);
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
                Date = new DateTime(2020, 1, 8, 14, 0, 0),
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 1, 20)
            };

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                "Occurs once. Scheduler will be used on 08/01/2020 at 14:00 starting on 01/01/2020 at 0:00 until 20/01/2020 at 0:00.");
        }
        [Fact]
        public void Scheduler_Recurring_NextDate_Test_Same_Day_In_Minutes()
        {
            DateTime input = new DateTime(2021, 11, 3, 17, 0 , 1);
            DateTime? expected = new DateTime(2021, 11, 3, 17, 30, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Minutes
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);
            setting.DaysOfWeek.Add(DayOfWeek.Saturday);

            expected.Should().Be(SchedulerTask.GetNextDate(setting, input));
        }
        [Fact]
        public void Scheduler_Recurring_NextDate_Test_Same_Day_In_Seconds()
        {
            DateTime input = new DateTime(2021, 11, 3, 17, 0, 1);
            DateTime? expected = new DateTime(2021, 11, 3, 17, 0, 30);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Seconds 
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);
            setting.DaysOfWeek.Add(DayOfWeek.Saturday);

            expected.Should().Be(SchedulerTask.GetNextDate(setting, input));
        }
        [Fact]
        public void Scheduler_Recurring_NextDate_Test_Same_Day_In_Hours()
        {
            DateTime input = new DateTime(2021, 11, 3, 17, 0, 1);
            DateTime? expected = new DateTime(2021, 11, 3, 18, 00, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyType = DailyFrecuencyType.Hours 
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);
            setting.DaysOfWeek.Add(DayOfWeek.Saturday);

            expected.Should().Be(SchedulerTask.GetNextDate(setting, input));
        }
        [Fact]
        public void Scheduler_Recurring_NextDate_Test_Same_Day_Out_After()
        {
            DateTime input = new DateTime(2021, 11, 3, 18, 0, 1);
            DateTime? expected = new DateTime(2021, 11, 6, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Minutes
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);
            setting.DaysOfWeek.Add(DayOfWeek.Saturday);

            expected.Should().Be(SchedulerTask.GetNextDate(setting, input));
        }
        [Fact]
        public void Scheduler_Recurring_NextDate_Test_Same_Day_Out_Early()
        {
            DateTime input = new DateTime(2021, 11, 3, 12, 0, 0);
            DateTime? expected = new DateTime(2021, 11, 3, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Minutes
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);
            setting.DaysOfWeek.Add(DayOfWeek.Saturday);

            expected.Should().Be(SchedulerTask.GetNextDate(setting, input));
        }

        public static IEnumerable<object[]> DailyTypeSecondsData()
        {
            yield return new object[] { new DateTime(2021, 10, 30), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 2), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 15, 59, 59), new DateTime(2021, 11, 3, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 0, 0), new DateTime(2021, 11, 3, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 0, 1), new DateTime(2021, 11, 3, 16, 00, 30) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 0, 31), new DateTime(2021, 11, 3, 16, 01, 00) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 1, 1), new DateTime(2021, 11, 3, 16, 01, 30) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 24, 59), new DateTime(2021, 11, 3, 17, 25, 00) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 25, 0), new DateTime(2021, 11, 3, 17, 25, 00) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 25,1), new DateTime(2021, 11, 3, 17, 25, 30) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 30, 0), new DateTime(2021, 11, 3, 17, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 35, 0), new DateTime(2021, 11, 3, 17, 35, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 35, 5), new DateTime(2021, 11, 3, 17, 35, 30) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 59, 10), new DateTime(2021, 11, 3, 17, 59, 30) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 59, 35), new DateTime(2021, 11, 3, 18, 00, 00) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 00, 0), new DateTime(2021, 11, 3, 18, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 00, 1), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 4), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 9), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 11), new DateTime(2021, 11, 17, 16, 00, 0) };
        }
        [Theory]
        [MemberData(nameof(DailyTypeSecondsData))]
        public void Scheduler_Recurring_NextDate_Seconds_DailyType(DateTime input, DateTime expected)
        {
            SettingScheduler setting = new SettingScheduler(
               TypeSetting.Recurring)
            {
                Enable = true,
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Seconds 
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                $"Occurs every 2 weeks on wednesday every 30 seconds between 16:00:00 and 18:00:00 starting on 01/11/2021 at 0:00.");
        }

        public static IEnumerable<object[]> DailyTypeMinutesData()
        {
            yield return new object[] { new DateTime(2021, 10, 30), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 2), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 15, 59, 59), new DateTime(2021, 11, 3, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 0, 0), new DateTime(2021, 11, 3, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 0, 1), new DateTime(2021, 11, 3, 16, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 25, 0), new DateTime(2021, 11, 3, 17, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 30, 0), new DateTime(2021, 11, 3, 17, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 35, 0), new DateTime(2021, 11, 3, 18, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 00, 0), new DateTime(2021, 11, 3, 18, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 00, 1), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 4), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 9), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 11), new DateTime(2021, 11, 17, 16, 00, 0) };
        }
        [Theory]
        [MemberData(nameof(DailyTypeMinutesData))]
        public void Scheduler_Recurring_NextDate_Minutes_DailyType(DateTime input, DateTime expected)
        {
            SettingScheduler setting = new SettingScheduler(
               TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Minutes
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                $"Occurs every 2 weeks on wednesday every 30 minutes between 16:00:00 and 18:00:00 starting on 01/11/2021 at 0:00.");
        }

        public static IEnumerable<object[]> DailyTypeHoursData()
        {
            yield return new object[] { new DateTime(2021, 10, 30), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 2), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 15, 59, 59), new DateTime(2021, 11, 3, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 0, 0), new DateTime(2021, 11, 3, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 16, 0, 1), new DateTime(2021, 11, 3, 17, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 25, 0), new DateTime(2021, 11, 3, 18, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 30, 0), new DateTime(2021, 11, 3, 18, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 35, 0), new DateTime(2021, 11, 3, 18, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 00, 0), new DateTime(2021, 11, 3, 18, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 00, 1), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 4), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 9), new DateTime(2021, 11, 17, 16, 00, 0) };
            yield return new object[] { new DateTime(2021, 11, 11), new DateTime(2021, 11, 17, 16, 00, 0) };
        }
        [Theory]
        [MemberData(nameof(DailyTypeHoursData))]
        public void Scheduler_Recurring_NextDate_Hours_DailyType(DateTime input, DateTime expected)
        {
            SettingScheduler setting = new SettingScheduler(
               TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 1,
                DailyType = DailyFrecuencyType.Hours
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                $"Occurs every 2 weeks on wednesday every 1 hours between 16:00:00 and 18:00:00 starting on 01/11/2021 at 0:00.");
        }

        public static IEnumerable<object[]> DailyTypeOnceData()
        {
            yield return new object[] { new DateTime(2021, 10, 30), new DateTime(2021, 11, 3, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 2), new DateTime(2021, 11, 3, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 10, 0, 0), new DateTime(2021, 11, 3, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 12, 29, 59), new DateTime(2021, 11, 3, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 12, 30, 0), new DateTime(2021, 11, 3, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 12, 30, 1), new DateTime(2021, 11, 17, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 13, 0, 0), new DateTime(2021, 11, 17, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 4), new DateTime(2021, 11, 17, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 9), new DateTime(2021, 11, 17, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 11), new DateTime(2021, 11, 17, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 11, 30), new DateTime(2021, 12, 1, 12, 30, 0) };
            yield return new object[] { new DateTime(2021, 12, 2), new DateTime(2021, 12, 15, 12, 30, 0) };
        }
        [Theory]
        [MemberData(nameof(DailyTypeOnceData))]
        public void Scheduler_Recurring_NextDate_Once_DailyType(DateTime input, DateTime expected)
        {
            SettingScheduler setting = new SettingScheduler(
               TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyOnceTime = new TimeSpan(12, 30, 0),
                DailyType = DailyFrecuencyType.Once 
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                $"Occurs every 2 weeks on wednesday every 12:30:00 starting on 01/11/2021 at 0:00.");
        }

        public static IEnumerable<object[]> PeriodData_1_DayOfWeek()
        {
            yield return new object[] { new DateTime(2021, 11, 2), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 15, 0, 0), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 0, 0), new DateTime(2021, 11, 3, 18, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 0, 1), new DateTime(2021, 11, 3, 20, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 19, 0, 1), new DateTime(2021, 11, 3, 20, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 4), new DateTime(2021, 11, 17, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 5), new DateTime(2021, 11, 17, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 6), new DateTime(2021, 11, 17, 16, 0, 0) };
        }
        [Theory]
        [MemberData(nameof(PeriodData_1_DayOfWeek))]
        public void Scheduler_Recurring_NextDate_1_DayOfWeek(DateTime input, DateTime expected)
        {
            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(20, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyType = DailyFrecuencyType.Hours   
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                $"Occurs every 2 weeks on wednesday every 2 hours between 16:00:00 and 20:00:00 starting on 01/11/2021 at 0:00.");
        }

        public static IEnumerable<object[]> PeriodData_2_DayOfWeek()
        {
            yield return new object[] { new DateTime(2021, 11, 2), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 15, 0, 0), new DateTime(2021, 11, 3, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 17, 0, 0), new DateTime(2021, 11, 3, 18, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 18, 0, 1), new DateTime(2021, 11, 3, 20, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 3, 19, 0, 1), new DateTime(2021, 11, 3, 20, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 4, 18, 15, 0), new DateTime(2021, 11, 7, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 5, 20, 10, 0), new DateTime(2021, 11, 7, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 6, 13, 13 ,13), new DateTime(2021, 11, 7, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 7, 11, 20, 10), new DateTime(2021, 11, 7, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 7, 17, 18, 0), new DateTime(2021, 11, 7, 18, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 7, 19, 15, 0), new DateTime(2021, 11, 7, 20, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 7, 20, 15, 0) ,new DateTime(2021, 11, 17, 16, 0, 0) };
            yield return new object[] { new DateTime(2021, 11, 12), new DateTime(2021, 11, 17, 16, 0, 0) };
        }
        [Theory]
        [MemberData(nameof(PeriodData_2_DayOfWeek))]
        public void Scheduler_Recurring_NextDate_2_DayOfWeek(DateTime input, DateTime expected)
        {
            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(20, 0, 0),
                DailyFrecuencyEvery = 2,
                DailyType = DailyFrecuencyType.Hours
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);
            setting.DaysOfWeek.Add(DayOfWeek.Sunday);

            DateTime? nextDate = SchedulerTask.GetNextDate(setting, input);
            expected.Should().Be(nextDate);
            SchedulerTask.GetDescriptionNextDate(setting).Should().Be(
                $"Occurs every 2 weeks on wednesday and sunday every 2 hours between 16:00:00 and 20:00:00 starting on 01/11/2021 at 0:00.");
        }
    }
}
