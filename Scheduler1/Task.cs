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

            if (this.setting.Date.HasValue == true)
            {
                nextDate = this.setting.Date.Value;
            }
            else
            {
                nextDate = input;
            }

            return nextDate;
        }

        private DateTime GetNextDateRecurring(DateTime input)
        {
            DateTime nextDate;

            nextDate = this.setting.StartDate.Value;
            do
            {
                nextDate = nextDate.AddDays(this.setting.Every);

                if (this.setting.EndDate.HasValue == true &&
                    this.setting.EndDate.Value.CompareTo(nextDate) < 0)
                {
                    throw new Exception("NextDate is greater than EndDate.");
                }

            } while (nextDate.CompareTo(input) < 1);

            return nextDate;
        }

        public DateTime? GetNextDate(DateTime input)
        {
            DateTime? nextDate;

            Validate(input);

            switch (Setting.Type)
            {
                case TypeSetting.Once:
                    nextDate = GetNextDateOnce(input);
                    break;
                case TypeSetting.Recurring:
                    nextDate = GetNextDateRecurring(input);
                    break;
                default:
                    nextDate = null;
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
                throw new Exception("Setting is disabled.");
            }

            if (this.setting.StartDate.HasValue == true)
            {
                if (this.setting.StartDate.Value.CompareTo(input) > 0)
                {
                    throw new Exception("StartDate is greater than Input.");
                }

                if (this.setting.Date.HasValue == true)
                {
                    if (this.setting.StartDate.Value.CompareTo(this.setting.Date.Value) > 0)
                    {
                        throw new Exception("StartDate is greater than Date.");
                    }
                }
            }

            if (this.setting.EndDate.HasValue == true)
            {
                if (this.setting.EndDate.Value.CompareTo(input) < 0)
                {
                    throw new Exception("Input is greater than EndDate.");
                }

                if (this.setting.Date.HasValue == true)
                {
                    if (this.setting.EndDate.Value.CompareTo(this.setting.Date.Value) < 0)
                    {
                        throw new Exception("Date is greater than EndDate.");
                    }
                }
            }

            if (this.setting.Type == TypeSetting.Recurring && this.setting.Every <= 0)
            {
                throw new Exception("Every is invalid.");
            }
        }
    }
}
