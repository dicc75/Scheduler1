using System;
using Xunit;
using Scheduler1;
using FluentAssertions;

namespace XUnitTestScheduler
{
    public class TestScheduler1
    {
        [Fact]
        public void Task_Once_Disabled_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
                Enable = false
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage("Setting is disabled.");
        }
        [Fact]
        public void Task_Once_Date_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentNullException> exceptionAssertions = action.Should().Throw<ArgumentNullException>().WithMessage("The value cannot be null. (Parameter 'Date')");
        }
        [Fact]
        public void Task_Once_Date_NextDate()
        {
            DateTime? expected = new DateTime(2020, 1, 8, 14, 0, 0);
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
                Date = new DateTime(2020, 1, 8, 14, 0, 0)
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Once_Description()
        {
            DateTime nextDate = new DateTime(2020, 1, 5);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            task.GetDescriptionNextDate(nextDate).Should().Be("Occurs once. Schedule will be used on 05/01/2020 at 0:00.");
        }
        [Fact]
        public void Task_Once_StartDate_EndDate_Description()
        {
            DateTime nextDate = new DateTime(2020, 1, 10, 14, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Once)
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 1, 20)
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            task.GetDescriptionNextDate(nextDate).Should().Be("Occurs once. Schedule will be used on 10/01/2020 at 14:00 starting on 01/01/2020 at 0:00 until 20/01/2020 at 0:00.");
        }
        [Fact]
        public void Task_Recurring_Disabled_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                Enable = false
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage("Setting is disabled.");
        }
        [Fact]
        public void Task_Recurring_Every_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                Every = 0
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage("Every is invalid.");
        }
        [Fact]
        public void Task_Input_Less_Than_StartDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 5)
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Input {input} is less than StartDate {setting.StartDate.Value}.");
        }
        [Fact]
        public void Task_Input_Greater_Than_EndDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 6);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                EndDate = new DateTime(2020, 1, 5),
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Input {input} is greater than EndDate {setting.EndDate.Value}.");
        }
        [Fact]
        public void Task_Date_Greater_Than_EndDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 3);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                Date = new DateTime(2020, 1, 7),
                EndDate = new DateTime(2020, 1, 4),
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Date {setting.Date.Value} is greater than EndDate {setting.EndDate.Value}.");
        }
        [Fact]
        public void Task_NextDate_Greater_Than_EndDate_ShouldReturnsAnException()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The NextDate {expected} is greater than EndDate {setting.EndDate.Value}.");
        }
        [Fact]
        public void Task_Input_less_than_StartDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 5)
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Input {input} is less than StartDate {setting.StartDate.Value}.");
        }
        [Fact]
        public void Task_Date_less_than_StartDate_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 6);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 5),
                Date = new DateTime(2020, 1, 4)
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Date {setting.Date.Value} is less than StartDate {setting.StartDate.Value}.");
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_Same_Day_In_Minutes()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_Same_Day_In_Seconds()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_Same_Day_In_Hours()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_Same_Day_Out_After()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_Same_Day_Out_Early()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_0()
        {
            DateTime input = new DateTime(2021, 11, 2);
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_1()
        {
            DateTime input = new DateTime(2021, 11, 4);
            DateTime? expected = new DateTime(2021, 11, 17, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 60,
                DailyType = DailyFrecuencyType.Seconds 
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_2()
        {
            DateTime input = new DateTime(2021, 11, 4);
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_3()
        {
            DateTime input = new DateTime(2021, 11, 7);
            DateTime? expected = new DateTime(2021, 11, 17, 16, 0, 0);

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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_4()
        {
            DateTime input = new DateTime(2021, 11, 9);
            DateTime? expected = new DateTime(2021, 11, 17, 16, 0, 0);

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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            DateTime? nextDate = task.GetNextDate(input);
            expected.Should().Be(nextDate);
            task.GetDescriptionNextDate(nextDate.Value).Should().Be(
                $"Occurs every 2 weeks on wednesday every 2 hours between 16:00:00 and 20:00:00 starting on 01/11/2021 at 0:00.");
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_5()
        {
            DateTime input = new DateTime(2021, 11, 20);
            DateTime? expected = new DateTime(2021, 11, 20, 16, 0, 0);

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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            DateTime? nextDate = task.GetNextDate(input);
            expected.Should().Be(nextDate);
            task.GetDescriptionNextDate(nextDate.Value).Should().Be(
                $"Occurs every 2 weeks on wednesday and saturday every 30 minutes between 16:00:00 and 18:00:00 starting on 01/11/2021 at 0:00.");
        }
        [Fact]
        public void Task_Recurring_NextDate_Test_6()
        {
            DateTime input = new DateTime(2021, 11, 21);
            DateTime? expected = new DateTime(2021, 12, 1, 16, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2021, 11, 1),
                EndDate = new DateTime(2021, 12, 31),
                Every = 2,
                DailyStartTime = new TimeSpan(16, 0, 0),
                DailyEndTime = new TimeSpan(18, 0, 0),
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Seconds
            };
            setting.DaysOfWeek.Add(DayOfWeek.Wednesday);
            setting.DaysOfWeek.Add(DayOfWeek.Friday);
            setting.DaysOfWeek.Add(DayOfWeek.Saturday);

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            DateTime? nextDate = task.GetNextDate(input);
            expected.Should().Be(nextDate);
            task.GetDescriptionNextDate(nextDate.Value).Should().Be(
                $"Occurs every 2 weeks on wednesday, friday and saturday every 30 seconds between 16:00:00 and 18:00:00 starting on 01/11/2021 at 0:00 until 31/12/2021 at 0:00.");
        }
        [Fact]
        public void Task_Recurring_NextDate_0__ShouldReturnsAnException()
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
                DailyFrecuencyEvery = 30,
                DailyType = DailyFrecuencyType.Minutes
            };
           
            Task task = Scheduler1.Scheduler.CreateTask(setting);
            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"No day of the week has been specified.");
        }
        [Fact]
        public void Task_Recurring_DailyFrecuencyEvery_Hours_ShouldReturnsAnException()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Every hours is invalid.");
        }
        [Fact]
        public void Task_Recurring_DailyFrecuencyEvery_Minutes_ShouldReturnsAnException()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Every minutes is invalid.");
        }
        [Fact]
        public void Task_Recurring_DailyFrecuencyEvery_Seconds_ShouldReturnsAnException()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The Every seconds is invalid.");
        }
        [Fact]
        public void Task_Recurring_DailyStartTime_ShouldReturnsAnException()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The StartTime is invalid.");
        }
        [Fact]
        public void Task_Recurring_DailyEndTime_ShouldReturnsAnException()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The EndTime is invalid.");
        }
        [Fact]
        public void Task_Recurring_DailyEndTime_Greater_than_DailyEndTime_ShouldReturnsAnException()
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

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The StartTime {setting.DailyStartTime.Value} is greater than EndTime {setting.DailyEndTime.Value}.");
        }

    }
}
