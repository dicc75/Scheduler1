namespace Scheduler1
{
    public class Scheduler
    {
        #region Constructor
        public Scheduler()
        {
        }
        #endregion

        #region Public Static Methods
        public static Task CreateTask(SettingScheduler setting)
        {
            Task task = new Task(setting);
            return task;
        }
        #endregion
    }
}
