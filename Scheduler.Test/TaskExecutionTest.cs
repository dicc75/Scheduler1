using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler1;
using System;

namespace Scheduler.Test
{
    [TestClass]
    public class TaskExecutionTest
    {
        //[TestMethod]
        //public void Task_Once_Disabled_Error()
        //{
        //    DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Once,
        //        OccurSetting.Daily)
        //    {
        //        Enable = false
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    try
        //    {
        //        DateTime? next = task.GetNextDate(input);
        //    }
        //    catch (Exception Error)
        //    {
        //        StringAssert.Contains(Error.Message, "Setting is disabled.");
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(WrongEveryException))]
        //public void Task_Recurring_Every_Error()
        //{
        //    DateTime input = new DateTime(2020, 1, 4);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        Every = 0
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    DateTime? next = task.GetNextDate(input);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(WrongInputStartDateException))]
        //public void Task_StartDate_Greater_Than_Input_Error()
        //{
        //    DateTime input = new DateTime(2020, 1, 4);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 5)
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    DateTime? next = task.GetNextDate(input);
        //}

        //[TestMethod]
        //public void Task_StartDate_Greater_Than_Date_Error()
        //{
        //    DateTime input = new DateTime(2020, 1, 6);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 5),
        //        Date = new DateTime(2020, 1, 4)
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    try
        //    {
        //        DateTime? next = task.GetNextDate(input);
        //    }
        //    catch (Exception Error)
        //    {
        //        StringAssert.Contains(Error.Message, "StartDate is greater than Date.");
        //    }
        //}

        //[TestMethod]
        //public void Task_Input_Greater_Than_EndDate_Error()
        //{
        //    DateTime input = new DateTime(2020, 1, 6);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        EndDate = new DateTime(2020, 1, 5),
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    try
        //    {
        //        DateTime? next = task.GetNextDate(input);
        //    }
        //    catch (Exception Error)
        //    {
        //        StringAssert.Contains(Error.Message, "Input is greater than EndDate.");
        //    }
        //}

        //[TestMethod]
        //public void Task_Date_Greater_Than_EndDate_Error()
        //{
        //    DateTime input = new DateTime(2020, 1, 3);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        Date = new DateTime(2020, 1, 7),
        //        EndDate = new DateTime(2020, 1, 4),
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    try
        //    {
        //        DateTime? next = task.GetNextDate(input);
        //    }
        //    catch (Exception Error)
        //    {
        //        StringAssert.Contains(Error.Message, "Date is greater than EndDate.");
        //    }
        //}

        //[TestMethod]
        //public void Task_NextDate_Greater_Than_EndDate_Error()
        //{
        //    DateTime input = new DateTime(2020, 1, 5);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 4),
        //        EndDate = new DateTime(2020, 1, 6),
        //        Every = 3
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    try
        //    {
        //        DateTime? next = task.GetNextDate(input);
        //    }
        //    catch (Exception Error)
        //    {
        //        StringAssert.Contains(Error.Message, "NextDate is greater than EndDate.");
        //    }
        //}

        //[TestMethod]
        //public void Task_Once_Date_NextDate()
        //{
        //    DateTime? expected = new DateTime(2020, 1, 8, 14, 0, 0);
        //    DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Once,
        //        OccurSetting.Daily)
        //    {
        //        Date = new DateTime(2020, 1, 8, 14, 0, 0)
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    Assert.AreEqual(expected, task.GetNextDate(input));
        //}

        //[TestMethod]
        //public void Task_Once_Input_NextDate()
        //{
        //    DateTime? expected = new DateTime(2020, 1, 4, 0, 0, 0);
        //    DateTime input = new DateTime(2020, 1, 4, 0, 0, 0);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Once,
        //        OccurSetting.Daily)
        //    {
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    Assert.AreEqual(expected, task.GetNextDate(input));
        //}

        //[TestMethod]
        //public void Task_Recurring_NextDate_Every_1()
        //{
        //    DateTime? expected = new DateTime(2020, 1, 5);
        //    DateTime input = new DateTime(2020, 1, 4);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 1),
        //        Every = 1
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    Assert.AreEqual(expected, task.GetNextDate(input));
        //}

        //[TestMethod]
        //public void Task_Recurring_NextDate_Every_2()
        //{
        //    DateTime? expected = new DateTime(2020, 1, 5);
        //    DateTime input = new DateTime(2020, 1, 4);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 1),
        //        Every = 2
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    Assert.AreEqual(expected, task.GetNextDate(input));
        //}

        //[TestMethod]
        //public void Task_Recurring_NextDate_Every_3()
        //{
        //    DateTime? expected = new DateTime(2020, 1, 7);
        //    DateTime input = new DateTime(2020, 1, 5);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 1),
        //        Every = 3
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    Assert.AreEqual(expected, task.GetNextDate(input));
        //}

        //[TestMethod]
        //public void Task_Once_Description()
        //{
        //    DateTime nextDate = new DateTime(2020, 1, 5);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Once,
        //        OccurSetting.Daily)
        //    {
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    string description = task.GetDescriptionNextDate(nextDate);

        //    StringAssert.Contains(description, "Occurs once. Schedule will be used on 05/01/2020 at 0:00.");
        //}

        //[TestMethod]
        //public void Task_Once_StartDate_Description()
        //{
        //    DateTime nextDate = new DateTime(2020, 1, 5);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Once,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 1)
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    string description = task.GetDescriptionNextDate(nextDate);

        //    StringAssert.Contains(description, "Occurs once. Schedule will be used on 05/01/2020 at 0:00 starting on 01/01/2020 at 0:00.");
        //}

        //[TestMethod]
        //public void Task_Once_StartDate_EndDate_Description()
        //{
        //    DateTime nextDate = new DateTime(2020, 1, 10, 14, 0, 0);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Once,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 1),
        //        EndDate = new DateTime(2020, 1, 20)
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    string description = task.GetDescriptionNextDate(nextDate);

        //    StringAssert.Contains(description, "Occurs once. Schedule will be used on 10/01/2020 at 14:00 starting on 01/01/2020 at 0:00 until 20/01/2020 at 0:00.");
        //}
        //[TestMethod]
        //public void Task_Recurring_Description()
        //{
        //    DateTime nextDate = new DateTime(2020, 1, 5);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        Every = 1
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    string description = task.GetDescriptionNextDate(nextDate);

        //    StringAssert.Contains(description, "Occurs every day. Schedule will be used on 05/01/2020 at 0:00.");
        //}

        //[TestMethod]
        //public void Task_Recurring_StartDate_Description()
        //{
        //    DateTime nextDate = new DateTime(2020, 1, 5);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 1),
        //        Every = 1
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    string description = task.GetDescriptionNextDate(nextDate);

        //    StringAssert.Contains(description, "Occurs every day. Schedule will be used on 05/01/2020 at 0:00 starting on 01/01/2020 at 0:00.");
        //}

        //[TestMethod]
        //public void Task_Recurring_EndDate_Description()
        //{
        //    DateTime nextDate = new DateTime(2020, 1, 5);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        EndDate = new DateTime(2020, 1, 20),
        //        Every = 1
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    string description = task.GetDescriptionNextDate(nextDate);

        //    StringAssert.Contains(description, "Occurs every day. Schedule will be used on 05/01/2020 at 0:00 until 20/01/2020 at 0:00.");
        //}

        //[TestMethod]
        //public void Task_Recurring_StartDate_EndDate_Description()
        //{
        //    DateTime input = new DateTime(2020, 1, 10, 14, 0, 0);

        //    SettingScheduler setting = new SettingScheduler(
        //        TypeSetting.Recurring,
        //        OccurSetting.Daily)
        //    {
        //        StartDate = new DateTime(2020, 1, 1),
        //        EndDate = new DateTime(2020, 1, 20),
        //        Every = 2
        //    };

        //    Task task = Scheduler1.Scheduler.CreateTask(setting);

        //    string description = task.GetDescriptionNextDate(input);

        //    StringAssert.Contains(description, "Occurs every 2 days. Schedule will be used on 10/01/2020 at 14:00 starting on 01/01/2020 at 0:00 until 20/01/2020 at 0:00.");
        //}
    }
}
