using System;
using System.Collections.Generic;
using System.Text;
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
     
        private DateTime GetNextDate(DateTime input)
        {
            DateTime nextDate = this.setting.StartDate;

            switch (Setting.Type)
            {
                case TypeSetting.Once:
                    if (this.setting.Date.HasValue == true &&
                        this.setting.StartDate.CompareTo(this.setting.Date.Value) < 0)
                    {
                        nextDate = this.setting.Date.Value;

                    }
                    else
                    {
                        if (this.setting.StartDate.CompareTo(input) < 0)
                        {
                            nextDate = input;
                        }
                    }
                    break;
                case TypeSetting.Recurring:

                    do
                    {
                        nextDate = nextDate.AddDays(this.setting.Every);

                        if (this.setting.EndDate.HasValue == true &&
                            this.setting.EndDate.Value.CompareTo(nextDate) < 0)
                        {
                            throw new Exception("Next date is invalid.");
                        }

                    } while (nextDate.CompareTo(input) < 1);
                    break;
            }
            return nextDate;
        }

        private string GetDescriptionNextDate(DateTime nextDate)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
                        
            string description = "Occurs ";

            switch (Setting.Type)
            {
                case TypeSetting.Once:
                    description += "once";
                    break;
                case TypeSetting.Recurring:
                    description += "every day";
                    break;
                default:
                    break;
            }

            description += ". Schedule will be used on ";
            description += nextDate.ToString("d", culture) + " at " + nextDate.ToString("t", culture);
            description += " starting on " + this.setting.StartDate.ToString("d", culture) + 
                    " at " + this.setting.StartDate.ToString("t", culture);
            if (this.setting.EndDate.HasValue == true)
            {
                description += " until " + this.setting.EndDate.Value.ToString("d", culture) + 
                    " at " + this.setting.EndDate.Value.ToString("t", culture);
            }

            return description;
        }

        public Output NextDate(DateTime input)
        {
            Output output;

            try
            {
                this.setting.Validate(input);

                DateTime nextDate = GetNextDate(input);
                output = new Output(nextDate, this.GetDescriptionNextDate(nextDate).ToString());
            }
            catch (Exception)
            {
                throw;
            }
            return output;
        }
    }
}
