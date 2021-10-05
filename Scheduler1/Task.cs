using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Scheduler1
{
    class Task
    {
        private DateTime input;
        private SettingScheduler setting;
       
        
        public Task(DateTime input,
            SettingScheduler setting)
            
        {
            this.input = input;
            this.setting = setting;
        }

        private string GetDescription(DateTime dateValue)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
                        
            string description = "Occurs ";

            switch (this.setting.Type)
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

            if (dateValue != null)
            {
                description += dateValue.ToString("d", culture) + 
                        " at " + dateValue.ToString("t", culture);
            }

            //if (this.startDate != null)
            //{
            //    description += " starting on " + DateTime.Parse(this.startDate.ToString()).ToString("d", culture) + 
            //        " at " + DateTime.Parse(this.startDate.ToString()).ToString("t", culture);
            //}

            return description;
        }

        public Output NextDate()
        {
            //Calculate the next date
            DateTime nextDate = new DateTime(2022, 1, 1);


            Output output = new Output(nextDate, this.GetDescription(nextDate).ToString());
            return output;
        }
    }
}
