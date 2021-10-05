using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler1
{
    public class Output
    {
        private DateTime nextExecutionTime;
        private string description = "";

        public DateTime NextExecutionTime
        {
            get { return this.nextExecutionTime; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public Output(DateTime nextExecutionTime, string description)
        {
            this.nextExecutionTime = nextExecutionTime;
            this.description = description;
        }

        public override string ToString()
        {
            return this.description;
        }
    }
}
