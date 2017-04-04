using System;
using System.Collections.Generic;
using DummyFinger;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : FingerPrintEvent
    {
        private List<byte[]> fingerList;

        public Form1()
        {
            InitializeComponent();
            fingerList = new List<byte[]>();
            label2.Text = "Loading Database Please wait";
            checkBox1.Enabled = false;
            groupBox1.Enabled = false;

            // Get URL here
            GetAsync("http://localhost:4686/Service1.svc/GetData");
        }

        public async Task GetAsync(string uri)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            var content = response.Content;
            string result = await content.ReadAsStringAsync();

            KeyValuePair<string, string>[] prints = JsonConvert.DeserializeObject<KeyValuePair<string, string>[]>(result.Replace(@"\", ""));

            //foreach (string print in prints)
            //{
            //    fingerList.Add(Convert.FromBase64String(print));
            //}

            //checkBox1.Enabled = true;
            //this.FingerPrintDetect += new FingerPrintEventHandler(On_FingerDetect);
            label2.Text = prints[0].Value;
            

            //label2.Text = "Ready";

            List<byte[]> temp = new List<byte[]>();
            fingerList.Add(BitConverter.GetBytes(65));
        }

        //private List<byte[]> GetFinger()
        //{
        //    //List<byte[]> temp = new List<byte[]>();
        //    //temp.Add(BitConverter.GetBytes(65));

        //    return temp;
        //}

        private void On_FingerDetect(object sender, FingerPrintArgs e)
        {
            if (!groupBox1.Enabled)
            {
                bool flag = false;
                foreach (byte[] temp in fingerList)
                {
                    if (temp.SequenceEqual(e.getPrint()))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    login();
                }
                else
                {
                    label2.Text = "Invalid Finger Print, Please try again.";                    
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;          
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin" && textBox2.Text == "123")
            {
                login();
            }
        }

        private void login()
        {
            label2.Text = "Login success";
            this.Hide();
            Form2 frm = new Form2();
            frm.ShowDialog();

            if (frm.closed)
            {
                frm.Dispose();
                this.Close();
            }
        }
    }
}
