using System;
using System.Windows.Forms;

namespace TestEjercicio1
{
    public partial class Scheluder : Form
    {
        public Scheluder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DateTime input = new DateTime(2020, 1, 4);

            //SettingScheduler setting = new SettingScheduler(
            //    TypeSetting.Once, OccurSetting.Daily,
            //    new DateTime(2020,1,1), 
            //    new DateTime(2020, 1, 8, 14, 0, 0));

            //Scheduler1.Task task = Scheduler.CreateTask(setting);
            //Output output = task.GetNextDate(input);

            //this.TxtNextExecutionTime.Text = output.NextExecutionTime.ToString();
            //this.TxtDescription.Text = output.Description.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.CbType.DataSource = Enum.GetValues(typeof(TypeSetting));
            //this.CbOccurs.DataSource = Enum.GetValues(typeof(OccurSetting));
            //this.NudEvery.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DateTime input = new DateTime(2020, 1, 4);

            //SettingScheduler setting = new SettingScheduler(
            //    TypeSetting.Recurring, OccurSetting.Daily,
            //    new DateTime(2020, 1, 1), 2);

            //Scheduler1.Task task = Scheduler.CreateTask(setting);
            //Output output = task.GetNextDate(input);

            //this.TxtNextExecutionTime.Text = output.NextExecutionTime.ToString();
            //this.TxtDescription.Text = output.Description.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.TxtNextExecutionTime.Text ="";
            //this.TxtDescription.Text = "";

            //DateTime input = DateTime.Parse(TxtInput.Text);
            //DateTime starDate = DateTime.Parse(TxtStarDate.Text);
            //DateTime? endData = null;
            //DateTime? date = null;
            //Boolean enabled = chkEnabled.Checked;

            //if (string.IsNullOrEmpty(TxtEndDate.Text) == false) { endData = DateTime.Parse(TxtEndDate.Text); };
            //if (string.IsNullOrEmpty(TxtDateTime.Text) == false) { date = DateTime.Parse(TxtDateTime.Text); };

            //SettingScheduler setting = new SettingScheduler(
            //    (TypeSetting)this.CbType.SelectedItem,
            //    (OccurSetting)this.CbOccurs.SelectedItem,
            //    starDate, (int)this.NudEvery.Value,
            //    date, endData, enabled);

            //Scheduler1.Task task = Scheduler.CreateTask(setting);

            //try
            //{
            //    Output output = task.NextDate(input);
            //    this.TxtNextExecutionTime.Text = output.NextExecutionTime.ToString();
            //    this.TxtDescription.Text = output.Description.ToString();
            //}
            //catch (Exception exc)
            //{

            //    MessageBox.Show(exc.Message.ToString());
            //}
        }
    }
}
