using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DummyFinger;
using System.Net.Http;

namespace WindowsFormsApplication1
{
    public partial class Form2 : FingerPrintEvent
    {
        public bool closed = false;

        public Form2()
        {
            InitializeComponent();
            this.FingerPrintDetect += new FingerPrintEventHandler(On_FingerDetect);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            toolStripStatusLabel1.Text = "Waiting for Finger Print input.";
        }

        private void On_FingerDetect(object sender, FingerPrintArgs e)
        {
            if (!button1.Enabled)
            {
                byte[] newPrint = e.getPrint();
                string print = Convert.ToBase64String(newPrint);

                // Post URL here
                PostAsync("", print);

                toolStripStatusLabel1.Text = "Processing Finger Print";
            }
        }
        public async Task PostAsync(string uri, string data)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(uri, new StringContent(data));

            var content = response.Content;

            await content.ReadAsStringAsync();

            button1.Enabled = true;
            toolStripStatusLabel1.Text = "Success";
        }
    }
}
