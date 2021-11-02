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
        public void Task_Recurring_NextDate_Every_1()
        {
            DateTime? expected = new DateTime(2020, 1, 5);
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1,
                DaysOfWeek = DaysOfWeek.Weekdays
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Every_2()
        {
            DateTime? expected = new DateTime(2020, 1, 5);
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 2,
                DaysOfWeek = DaysOfWeek.Weekdays
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_NextDate_Every_3()
        {
            DateTime? expected = new DateTime(2020, 1, 7);
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 3,
                DaysOfWeek = DaysOfWeek.Weekdays
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);
            expected.Should().Be(task.GetNextDate(input));
        }
        [Fact]
        public void Task_Recurring_Description()
        {
            DateTime nextDate = new DateTime(2020, 1, 5);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                Every = 1
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            task.GetDescriptionNextDate(nextDate).Should().Be("Occurs every day. Schedule will be used on 05/01/2020 at 0:00.");
        }
        [Fact]
        public void Task_Recurring_StartDate_EndDate_Description()
        {
            DateTime input = new DateTime(2020, 1, 10, 14, 0, 0);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 1, 20),
                Every = 2
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetDescriptionNextDate(input);


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
            DateTime input = new DateTime(2020, 1, 5);
            DateTime nextDate = new DateTime(2020, 1, 7);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 4),
                EndDate = new DateTime(2020, 1, 6),
                Every = 3,
                DaysOfWeek = DaysOfWeek.Weekdays
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage($"The NextDate {nextDate} is greater than EndDate {setting.EndDate.Value}.");
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
        public void Task_Recurring_DayOfWeek_NULL_ShouldReturnsAnException()
        {
            DateTime input = new DateTime(2020, 1, 4);

            SettingScheduler setting = new SettingScheduler(
                TypeSetting.Recurring)
            {
                StartDate = new DateTime(2020, 1, 1),
                Every = 1
            };

            Task task = Scheduler1.Scheduler.CreateTask(setting);

            Action action = () => task.GetNextDate(input);
            FluentAssertions.Specialized.ExceptionAssertions<ArgumentException> exceptionAssertions = action.Should().Throw<ArgumentException>().WithMessage("No day of the week has been specified.");
        }
    }
}
