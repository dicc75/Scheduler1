using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler1
{
    public class SettingScheduler
    {
        public TypeSetting Type = TypeSetting.Once;
        public OccurSetting Occur = OccurSetting.Daily;
        public int Every = 0; //days
        public Boolean Enable = true;
        private DateTime? startDate;
        private DateTime? endDate;

        public SettingScheduler()
        { 

        }
    }
}
