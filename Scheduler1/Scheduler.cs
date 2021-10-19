namespace Scheduler1
{
    public class Scheduler
    {
        public Scheduler()
        {
        }

        public static Task CreateTask(SettingScheduler setting)
        {
            Task task = new Task(setting);
            return task;
        }
    }
}
