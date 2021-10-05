using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scheduler1;

namespace TestEjercicio1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Scheduler scheduler = new Scheduler();
          
            Output output = scheduler.CalculateNextDate(new DateTime(2021,10,4));

            this.label1.Text = output.NextExecutionTime.ToString();

            this.label2.Text = output.Description.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
