using System;

namespace Scheduler1
{
    public class Scheduler
    {
        private SettingScheduler settingScheduler = new SettingScheduler();

        public Scheduler()
        {
        }

        public SettingScheduler SettingScheduler { get => settingScheduler; set => settingScheduler = value; }

        public Output CalculateNextDate(DateTime input)
        {
            Output output;

            //Validation parameters

            if (this.SettingScheduler is null)
            {
                throw new ArgumentNullException(nameof(this.SettingScheduler));
            }
                       

            try
            {
                Task task = new Task(input, this.SettingScheduler);
                output = task.NextDate();
            }
            catch (Exception)
            {
                throw;
            }

            return output;
        }
    }
}
